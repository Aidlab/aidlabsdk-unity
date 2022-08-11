using System.Runtime.InteropServices;

namespace Aidlab
{
    public class BLEApi
    {
        #region ScanningDevices
        [DllImport("BLE_DLL_x64.dll", EntryPoint = "StartDeviceScan")]
        public static extern void StartDeviceScan();

        [DllImport("BLE_DLL_x64.dll", EntryPoint = "StopDeviceScan")]
        public static extern void StopDeviceScan();

        public enum ScanStatus { PROCESSING, AVAILABLE, FINISHED };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DeviceUpdate
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string id;
            [MarshalAs(UnmanagedType.I1)]
            public bool isConnectable;
            [MarshalAs(UnmanagedType.I1)]
            public bool isConnectableUpdated;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string name;
            [MarshalAs(UnmanagedType.I1)]
            public bool nameUpdated;
        }

        [DllImport("BLE_DLL_x64.dll", EntryPoint = "PollDevice")]
        public static extern ScanStatus PollDevice(ref DeviceUpdate device, bool block);

        public static DeviceUpdate DeviceUpdateCopy(DeviceUpdate device)
        {
            DeviceUpdate newDevice = new DeviceUpdate();
            newDevice.id = device.id;
            newDevice.isConnectable = device.isConnectable;
            newDevice.isConnectableUpdated = device.isConnectableUpdated;
            newDevice.name = device.name;
            newDevice.nameUpdated = device.nameUpdated;

            return newDevice;
        }
        #endregion ScanningDevices

        #region Characteristics
        [DllImport("BLE_DLL_x64.dll", EntryPoint = "SubscribeCharacteristic", CharSet = CharSet.Unicode)]
        public static extern bool SubscribeCharacteristic(string deviceId, string serviceId, string characteristicId, bool block);

        [DllImport("BLE_DLL_x64.dll", EntryPoint = "ReadData")]
        public static extern void ReadData(in BLEData data, out ReadDataResult result);

        [DllImport("BLE_DLL_x64.dll", EntryPoint = "SendData")]
        public static extern bool SendData(in BLEData data, bool block);
        #endregion Characteristics

        #region Data
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct BLEData
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] buf;
            [MarshalAs(UnmanagedType.I2)]
            public short size;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string deviceId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string serviceUuid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string characteristicUuid;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct ReadDataResult
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] buf;
            [MarshalAs(UnmanagedType.I2)]
            public short size;
            [MarshalAs(UnmanagedType.I1)]
            public bool result;
        };

        [DllImport("BLE_DLL_x64.dll", EntryPoint = "PollData")]
        public static extern bool PollData(out BLEData data, bool block);
        #endregion Data

        #region Other
        [DllImport("BLE_DLL_x64.dll", EntryPoint = "Quit")]
        public static extern void Quit();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct ErrorMessage
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string msg;
        };

        [DllImport("BLE_DLL_x64.dll", EntryPoint = "GetError")]
        public static extern void GetError(out ErrorMessage buf);
        #endregion Other
    }
}
