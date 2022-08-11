using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

namespace Aidlab.BLE
{
    public class BLEConnector
    {
        private Dictionary<string, BLEApi.DeviceUpdate> devices;
        private string foundDevice;
        public Thread bleConnectorThread;
        private BLEStatus currentBLEStatus = BLEStatus.None;
        private AidlabSDK aidlabSDK;
        private string deviceNameToConnect;      
        private bool isConnected = false;  
        private string lastError = "";
        private System.IntPtr aidlabAddress;

        #region MainMethods
        public BLEConnector(AidlabSDK aidlabSDK, string deviceNameToConnect)
        {
                      
            this.aidlabSDK = aidlabSDK;
            this.deviceNameToConnect = deviceNameToConnect;
            devices = new Dictionary<string, BLEApi.DeviceUpdate>();

            bleConnectorThread = new Thread(StartDevicesScan);
            bleConnectorThread.Start();
            currentBLEStatus = BLEStatus.ScanningDevices;
        }

        public void ConnectionProcess()
        {
            switch (currentBLEStatus)
            {
                case BLEStatus.None:
                    Debug.Log("Something went wrong...");
                    break;
                case BLEStatus.ScanningDevices:
                    if (!bleConnectorThread.IsAlive && !string.IsNullOrEmpty(foundDevice))
                    {
                        currentBLEStatus = BLEStatus.TryingToConnect;
                        bleConnectorThread = new Thread(Connect);
                        bleConnectorThread.Start();
                    }
                    break;
                case BLEStatus.TryingToConnect:
                    if (!bleConnectorThread.IsAlive && isConnected)
                    {
                        currentBLEStatus = BLEStatus.Connected;
                        aidlabAddress = AidlabAPI.Initial();
                        
                        aidlabSDK.OnAidlabConnected(aidlabAddress); 

                        Thread.Sleep(128);

                        Write(aidlabSDK.GetCollectCommand(aidlabAddress));

                        bleConnectorThread = new Thread(ReceiveData);
                        bleConnectorThread.Start();
                    }
                    break;
            }

            BLEApi.ErrorMessage res = new BLEApi.ErrorMessage();
            BLEApi.GetError(out res);
            if (lastError != res.msg) 
            {
                lastError = res.msg;
                Debug.Log("BleDll: " + res.msg);
            }
        }

        public byte[] AddByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 0);
            newArray[bArray.Length] = newByte;
            return newArray;
        }

        public void Disconnect()
        {
            isConnected = false;
            if (bleConnectorThread != null)
                bleConnectorThread.Abort();
            BLEApi.Quit();
            aidlabSDK.OnAidlabDisconnected();
            
            bleConnectorThread = new Thread(StartDevicesScan);
            bleConnectorThread.Start();
            currentBLEStatus = BLEStatus.ScanningDevices;

        }
        #endregion MainMethods


        #region ScanningDevices
        private void StartDevicesScan()
        {
            Debug.Log("Start scanning devices");

            while (true)
            {
                BLEApi.StartDeviceScan();
                BLEApi.DeviceUpdate device = new BLEApi.DeviceUpdate();

                while (BLEApi.PollDevice(ref device, true) != BLEApi.ScanStatus.FINISHED)
                {
                    UpdateDevices(device);
                    foundDevice = CheckDevices();
                    if (!string.IsNullOrEmpty(foundDevice))
                    {
                        Debug.Log("Device found: " + foundDevice);

                        BLEApi.StopDeviceScan();
                        Thread.Sleep(256);
                        return;
                    }
                }

                BLEApi.StopDeviceScan();
                Debug.Log("Devices scanning complete");
                Thread.Sleep(500);
                Debug.Log("Start scanning devices again");
            }
        }


        private void UpdateDevices(BLEApi.DeviceUpdate device)
        {
            if (!devices.ContainsKey(device.id))
            {
                var newDevice = BLEApi.DeviceUpdateCopy(device);
                devices.Add(device.id, newDevice);
            }
            else
            {
                var savedDevice = devices[device.id];
                devices.Remove(device.id);

                var newDevice = BLEApi.DeviceUpdateCopy(savedDevice);
                if (device.nameUpdated)
                {
                    newDevice.name = device.name;
                    newDevice.nameUpdated = true;
                }
                if (device.isConnectableUpdated)
                {
                    newDevice.isConnectable = device.isConnectable;
                    newDevice.isConnectableUpdated = true;
                }
                devices.Add(device.id, newDevice);
            }
        }

        private string CheckDevices()
        {
            foreach (var device in devices.Values)
            {
                if (device.name.ToLower().Equals(deviceNameToConnect.ToLower()) && device.isConnectable)
                {
                    return device.id;
                }
            }

            return null;
        }
        #endregion ScanningDevices


        #region Connecting
        private void Connect()
        {
            Thread[] characteristicsThreads = new Thread[AidlabCharacteristicsUUID.Characteristics.Length];

            int i = 0;
            foreach (var characteristic in AidlabCharacteristicsUUID.Characteristics)
            {
                characteristicsThreads[i] = new Thread(() => ConnectToCharacteristic(characteristic));
                characteristicsThreads[i].Start();
                characteristicsThreads[i].Join();
                i++;
                Thread.Sleep(128);
            }

            string fw = Read(AidlabCharacteristicsUUID.FirmwareUUID);
            string hw = Read(AidlabCharacteristicsUUID.HardwareUUID);

            aidlabSDK.firmwareRevisionStr = fw;
            aidlabSDK.hardwareRevisionStr = hw;

            Debug.Log("Connection completed");
            Debug.Log("FW: " + fw);
            Debug.Log("HW: " + hw);
            
            isConnected = true;
        }
  
        public void Write(byte[] payload)
        {            
            byte[] toSend = {};
            // bluetooth allows to send 20 bytes per packet, so we need to divide 
            // the entire buffer into 20 packets and send separately, dll ble does not 
            // have a queue so we wait 128 ms
            for (int i = 0; i < payload.Length; i++) 
            {
                toSend = AddByteToArray(toSend,  payload[i]);

                if (toSend.Length >= 20) 
                {
                    RawWrite(toSend);
                    Thread.Sleep(128);
                    Array.Resize(ref toSend, 0);
                }
            }

            if (toSend.Length > 0) 
            {
                RawWrite(toSend);
            }
        }

        public void RawWrite(byte[] payload) 
        {
            BLEApi.BLEData data = new BLEApi.BLEData();
            data.buf = new byte[512];
            data.size = (short)payload.Length;
            data.deviceId = foundDevice;
            data.serviceUuid = AidlabCharacteristicsUUID.CmdUUID.serviceUuid;
            data.characteristicUuid = AidlabCharacteristicsUUID.CmdUUID.characteristicUuid;
            for (int i = 0; i < payload.Length; i++)
                data.buf[i] = payload[i];

            BLEApi.SendData(in data, false);          
        }

        public String Read(FullCharacteristic characteristic)
        {

            BLEApi.ReadDataResult result;
            BLEApi.BLEData data = new BLEApi.BLEData();
            data.deviceId = foundDevice;
            data.serviceUuid = characteristic.serviceUuid;
            data.characteristicUuid = characteristic.characteristicUuid;

            BLEApi.ReadData(in data, out result);

            String value = "";

            if (result.result) {
                byte[] buf = result.buf.Reverse().SkipWhile(x => x == 0).Reverse().ToArray();
                value = System.Text.Encoding.UTF8.GetString(buf);
            }

            return String.Concat(value.Where(c => !Char.IsWhiteSpace(c)));
        }
  
        private void ConnectToCharacteristic(FullCharacteristic fullCharacteristic)
        {
            bool result = BLEApi.SubscribeCharacteristic(foundDevice,
                    fullCharacteristic.serviceUuid,
                    fullCharacteristic.characteristicUuid,
                    true);
        }
        #endregion Connecting

        #region ReceiveData
        private void ReceiveData()
        {

            while (true)
            {
                RawReceiveData();
            }
        }

        private void RawReceiveData()
        {

            BLEApi.BLEData BLEData;

            bool result = BLEApi.PollData(out BLEData, true);

            if (result)
            {
                aidlabSDK.OnAidlabDataReceived(BLEData, aidlabAddress);
            }
        }
        #endregion ReceiveData
    }
}
