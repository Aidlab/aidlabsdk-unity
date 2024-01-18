using System;
using UnityEngine;
using Aidlab.BLE;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aidlab
{
    /// <summary>
    /// The `AidlabSDK` class is the main interface for interacting with the Aidlab or Aidmed One device.
    /// </summary>
    public class AidlabSDK : MonoBehaviour
    {
        /// <summary>
        /// Initializes the Aidlab SDK and creates an instance of the SDK object on the scene.
        /// </summary>
        public static void init()
        {
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("SDK"), Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        /// Enum representing different types of signals supported by the Aidlab device.
        /// </summary>
        enum Signal
        {
            Ecg = 0,
            Respiration = 1,
            Temperature = 2,
            Motion = 3,
            Battery = 4,
            Activity = 5,
            Orientation = 6,
            Steps = 7,
            HeartRate = 8,
            HealthThermometer = 9,
            SoundVolume = 10,
            Rr = 11,
            Pressure = 12,
            SoundFeatures = 13,
            RespirationRate = 14,
            BodyPosition = 15
        }

        #region Variables
        [SerializeField] private string deviceNameToConnect = "Aidlab";
        // private MainThreadWorker mainThreadWorker = new MainThreadWorker();
        private MainThreadWorker mainThreadWorker;
        public static AidlabDelegate aidlabDelegate = new AidlabDelegate();
        private BLEConnector bleConnector;
        private IAidlabDevice aidlabDevice;
        public string firmwareRevisionStr = "";
        public string hardwareRevisionStr = "";
        private float lastDataReceivedTime = -1.0f;
        private bool receivedData = false;
        static AidlabSDK instance;

        #endregion Variables
        #region UnityMethods

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            mainThreadWorker = MainThreadWorker.Instance;
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                aidlabDevice = null;
                lastDataReceivedTime = -1.0f;
                receivedData = false;
                bleConnector = new BLEConnector(this, deviceNameToConnect);
                GetComponent<MeshRenderer>().enabled = false;

            }
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            // Unfortunately, the dll from bluetooth does not have an event about 
            // the disconnection of the device, so we have to check that it is 
            // connected all the time. This is checked based on the time since 
            // the last bluetooth packet transmitted.
            TestConnection();
            // dll from bluetooth doesn't have an event about new 
            // data so we have to constantly check if there is any data and download and parse it
            bleConnector.ConnectionProcess();
        }

        /// <summary>
        /// Called when the script becomes disabled or inactive.
        /// </summary>
        private void OnDisable()
        {
            bleConnector.Disconnect();
        }
        #endregion UnityMethods


        #region Methods

        /// <summary>
        /// Checks the connection status and disconnects if no data is received for a certain period.
        /// </summary>
        private void TestConnection()
        {
            if (receivedData)
            {
                lastDataReceivedTime = Time.time;
                receivedData = false;
            }
            if (lastDataReceivedTime > 0.0f && Time.time - 8.0f > lastDataReceivedTime && aidlabDevice != null)
            {
                bleConnector.Disconnect();
            }
        }

        /// <summary>
        /// Called when Aidlab is connected, sets firmware and hardware revision.
        /// </summary>
        /// <param name="aidlabAddress">Address of the connected Aidlab device.</param>
        public void OnAidlabConnected(IntPtr aidlabAddress)
        {
            aidlabDevice = new AidlabDevice(firmwareRevisionStr, hardwareRevisionStr, aidlabAddress);

            byte[] firmwareRevision = System.Text.Encoding.UTF8.GetBytes(firmwareRevisionStr);
            AidlabAPI.SetFirmwareRevision(firmwareRevision, firmwareRevision.Length, aidlabAddress);

            byte[] hardwareRevision = System.Text.Encoding.UTF8.GetBytes(hardwareRevisionStr);
            AidlabAPI.SetHardwareRevision(hardwareRevision, hardwareRevision.Length, aidlabAddress);
        }


        /// <summary>
        /// The method allows you to specify which signals to collect from the Aidlab device.
        /// You can use the <see cref="Signal"/> enum to choose from various signal types.
        /// For example, to collect ECG and Respiration signals, you can use:
        /// <code>
        /// byte[] signals = {(byte)Signal.Ecg, (byte)Signal.Respiration};
        /// </code>
        /// </summary>
        /// <remarks>
        /// Please note that due to Aidlab's limited computing power, simultaneous data acquisition
        /// from the microphone and the Motion sensor is not possible. Therefore, if you subscribe
        /// to both Sound Volume and Motion data types, only one type of data will be returned.
        /// </remarks>
        public byte[] GetCollectCommand(IntPtr aidlabAddress)
        {
            byte[] signals = { (byte)Signal.Respiration, (byte)Signal.Ecg, (byte)Signal.HeartRate, (byte)Signal.Temperature };

            IntPtr ptr = AidlabAPI.GetCollectCommand(signals, signals.Length, signals, signals.Length, aidlabAddress);

            // ugly, we take the size from the header to find out the size of the whole package
            byte[] sizeBytes = new byte[5];
            Marshal.Copy(ptr, sizeBytes, 0, 5);
            int size = (int)sizeBytes[3] | (((int)sizeBytes[4]) << 8);

            byte[] result = new byte[size];
            Marshal.Copy(ptr, result, 0, size);

            return result;
        }

        public void OnAidlabDataReceived(BLEApi.BLEData BLEData, IntPtr aidlabAddress)
        {
            receivedData = true;

            if (AidlabCharacteristicsUUID.CmdUUID.Equals(BLEData.serviceUuid, BLEData.characteristicUuid))
            {
                AidlabAPI.InternalProcessCMD(BLEData.buf, BLEData.size, ecg_c_callback, respiration_c_callback, temperature_c_callback,
                                                accelerometer_c_callback, gyroscope_c_callback, magnetometer_c_callback,
                                                battery_c_callback, activity_c_callback, steps_c_callback,
                                                orientation_c_callback, quaternion_c_callback, respiration_rate_c_callback,
                                                wear_state_c_callback, heart_rate_c_callback, rr_c_callback,
                                                sound_volume_c_callback, exercise_c_callback, receive_command_c_callback,
                                                received_message_c_callback, user_event_c_callback, pressure_c_callback,
                                                pressure_wear_state_c_callback, body_position_c_callback, error_c_callback,
                                                signal_quality_c_callback, aidlabDevice.Address);
            }
        }

        /// <summary>
        /// Called when Aidlab is disconnected.
        /// </summary>
        public void OnAidlabDisconnected()
        {
            Debug.Log("On aidlab disconnected");
            aidlabDevice = null;
            lastDataReceivedTime = -1.0f;
            receivedData = false;
        }
        #endregion Methods


        #region Callbacks
        private void ecg_c_callback(System.IntPtr context, System.UInt64 timestamp, float[] values, int size)
        {
            aidlabDelegate.DidReceiveECG(timestamp, AverageCalc.GetAverage(values, size));
        }

        private void respiration_c_callback(System.IntPtr context, System.UInt64 timestamp, float[] values, int size)
        {
            aidlabDelegate.DidReceiveRespiration(timestamp, AverageCalc.GetAverage(values, size));
        }

        private void temperature_c_callback(System.IntPtr context, System.UInt64 timestamp, float value)
        {
            aidlabDelegate.DidReceiveSkinTemperature(timestamp, value);
        }

        private void activity_c_callback(System.IntPtr context, System.UInt64 timestamp, byte activity)
        {
            aidlabDelegate.DidReceiveActivity(timestamp, (ActivityType)activity);
        }

        private void steps_c_callback(System.IntPtr context, System.UInt64 timestamp, System.UInt64 steps)
        {
            aidlabDelegate.DidReceiveSteps(timestamp, steps);
        }

        private void accelerometer_c_callback(System.IntPtr context, System.UInt64 timestamp, float ax, float ay, float az)
        {
            aidlabDelegate.DidReceiveAccelerometer(timestamp, ax, ay, az);
        }

        private void gyroscope_c_callback(System.IntPtr context, System.UInt64 timestamp, float gx, float gy, float gz)
        {
            aidlabDelegate.DidReceiveGyroscope(timestamp, gx, gy, gz);
        }

        private void magnetometer_c_callback(System.IntPtr context, System.UInt64 timestamp, float mx, float my, float mz)
        {
            aidlabDelegate.DidReceiveMagnetometer(timestamp, mx, my, mz);
        }

        private void quaternion_c_callback(System.IntPtr context, System.UInt64 timestamp, float qw, float qx, float qy, float qz)
        {
            aidlabDelegate.DidReceiveQuaternion(timestamp, qx, qy, qz, qw);
        }

        private void orientation_c_callback(System.IntPtr context, System.UInt64 timestamp, float roll, float pitch, float yaw)
        {
            aidlabDelegate.DidReceiveOrientation(timestamp, roll, pitch, yaw);
        }

        private void body_position_c_callback(System.IntPtr context, System.UInt64 timestamp, byte bodyPosition)
        {
            aidlabDelegate.DidReceiveBodyPosition(timestamp, (BodyPosition)bodyPosition);
        }

        private void heart_rate_c_callback(System.IntPtr context, System.UInt64 timestamp, int heartRate)
        {
            aidlabDelegate.DidReceiveHeartRate(timestamp, heartRate);
        }

        private void rr_c_callback(System.IntPtr context, System.UInt64 timestamp, int rr)
        {
            aidlabDelegate.DidReceiveRr(timestamp, rr);
        }

        private void respiration_rate_c_callback(System.IntPtr context, System.UInt64 timestamp, System.UInt32 value)
        {
            aidlabDelegate.DidReceiveRespiration(timestamp, value);
        }

        private void wear_state_c_callback(System.IntPtr context, byte wearState)
        {
            aidlabDelegate.WearStateDidChange((WearState)wearState);
        }

        private void sound_volume_c_callback(System.IntPtr context, System.UInt64 timestamp, System.UInt16 soundVolume)
        {
            aidlabDelegate.DidReceiveSoundVolume(timestamp, soundVolume);
        }

        private void exercise_c_callback(System.IntPtr context, byte exercise)
        {
            aidlabDelegate.DidDetectExercise((Exercise)exercise);
        }

        private void receive_command_c_callback(System.IntPtr context) { }

        private void pressure_c_callback(System.IntPtr context, System.UInt64 timestamp, int[] pressure, int size)
        {
            aidlabDelegate.DidReceivePressure(timestamp, AverageCalc.GetAverage(pressure, size));
        }

        private void pressure_wear_state_c_callback(System.IntPtr context, byte pressureWearState)
        {
            aidlabDelegate.PressureWearStateDidChange((WearState)pressureWearState);
        }

        private void received_message_c_callback(System.IntPtr context, string process, string message) { }

        private void user_event_c_callback(System.IntPtr context, System.UInt64 timestamp) { }

        private void battery_c_callback(System.IntPtr context, byte stateOfCharge)
        {
            aidlabDelegate.DidReceiveBatteryLevel((int)stateOfCharge);
        }

        private void signal_quality_c_callback(System.IntPtr context, System.UInt64 timestamp, int value) { }

        private void error_c_callback(System.IntPtr context, string error) { }

        #endregion Callbacks
    }
}




