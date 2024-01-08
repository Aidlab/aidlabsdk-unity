using UnityEngine;

namespace Aidlab
{
    /// <summary>
    /// The `AidlabDelegate` class contains implementations of methods to handle data received from the Aidlab device.
    /// </summary>
    public class AidlabDelegate
    {
        /// <summary>
        /// Represents Electrocardiogram (ECG) data using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<float> ecg = new Data.Data1<float>("ECG");

        /// <summary>
        /// Represents Respiration data using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<float> respiration = new Data.Data1<float>("Respiration");

        /// <summary>
        /// Represents Skin temperature data using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<float> temperature = new Data.Data1<float>("Temperature");

        /// <summary>
        /// Represents the battery level using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<int> battery = new Data.Data1<int>("Battery");
        /// <summary>
        /// Represents the activity type using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<ActivityType> activity = new Data.Data1<ActivityType>("Activity");

        /// <summary>
        /// Represents the number of steps using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<System.UInt64> steps = new Data.Data1<System.UInt64>("Steps");

        /// <summary>
        /// Represents the respiration rate using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<System.UInt32> respirationRate = new Data.Data1<System.UInt32>("Respiration Rate");
        /// <summary>
        /// Represents the wear state using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<WearState> wearState = new Data.Data1<WearState>("Wear State");
        /// <summary>
        /// Represents the heart rate using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<int> heartRate = new Data.Data1<int>("Heart Rate");
        /// <summary>
        /// Represents the RR interval using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<int> rr = new Data.Data1<int>("RR");
        /// <summary>
        /// Represents the sound volume using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<System.UInt16> soundVolume = new Data.Data1<System.UInt16>("Sound Volume");

        /// <summary>
        /// Represents the exercise type using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<Exercise> exercise = new Data.Data1<Exercise>("Exercise");
        /// <summary>
        /// Represents accelerometer data using the Data3 class.
        /// </summary>
        /// <remarks>
        /// The `Data3` class provides a three-value data structure (x, y, z) with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data3<float> accelerometer = new Data.Data3<float>("Accelerometer");
        /// <summary>
        /// Represents gyroscope data using the Data3 class.
        /// </summary>
        /// <remarks>
        /// The `Data3` class provides a three-value data structure (x, y, z) with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data3<float> gyroscope = new Data.Data3<float>("Gyroscope");
        /// <summary>
        /// Represents magnetometer data using the Data3 class.
        /// </summary>
        /// <remarks>
        /// The `Data3` class provides a three-value data structure (x, y, z) with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data3<float> magnetometer = new Data.Data3<float>("Magnetometer");
        /// <summary>
        /// Represents orientation data using the Data3 class.
        /// </summary>
        /// <remarks>
        /// The `Data3` class provides a three-value data structure (x, y, z) with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data3<float> orientation = new Data.Data3<float>("Orientation");
        /// <summary>
        /// Represents body position using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<BodyPosition> bodyPosition = new Data.Data1<BodyPosition>("Body Position");

        /// <summary>
        /// Represents quaternion data using the Data4 class.
        /// </summary>
        /// <remarks>
        /// The `Data4` class provides a four-value data structure (x, y, z, w) with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data4<float, float> quaternion = new Data.Data4<float, float>("Quaternion");

        /// <summary>
        /// Represents pressure data using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<int> pressure = new Data.Data1<int>("Pressure");

        /// <summary>
        /// Represents the wear state of pressure using the Data1 class.
        /// </summary>
        /// <remarks>
        /// The `Data1` class provides a single-value data structure with event-based delegates for handling data updates.
        /// </remarks>
        public Data.Data1<WearState> pressureWearState = new Data.Data1<WearState>("Pressure Wear State");

        /// <summary>
        /// Delegate class that handles data received from the Aidlab device. Defines methods that you use to receive events.
        /// </summary>
        public AidlabDelegate() { }
        /// <summary>
        /// Notifies when Electrocardiogram (ECG) data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="value">ECG data value.</param>
        public void DidReceiveECG(System.UInt64 timestamp, float value)
        {
            ecg.ReceiveData(value, timestamp);
        }

        /// <summary>
        /// Notifies when respiration data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="value">Respiration data value.</param>
        public void DidReceiveRespiration(System.UInt64 timestamp, float value)
        {
            respiration.ReceiveData(value, timestamp);
        }

        /// <summary>
        /// Notifies when respiration rate data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="value">Respiration rate data value.</param>
        public void DidReceiveRespirationRate(System.UInt64 timestamp, System.UInt32 value)
        {
            respirationRate.ReceiveData(value, timestamp);
        }

        /// <summary>
        /// Notifies when battery level data is received.
        /// </summary>
        /// <param name="stateOfCharge">State of charge of the battery.</param>
        public void DidReceiveBatteryLevel(int stateOfCharge)
        {
            battery.ReceiveData(stateOfCharge, 0);
        }

        /// <summary>
        /// Notifies when skin temperature data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="value">Skin temperature data value.</param>
        public void DidReceiveSkinTemperature(System.UInt64 timestamp, float value)
        {
            temperature.ReceiveData(value, timestamp);
        }

        /// <summary>
        /// Notifies when accelerometer data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="ax">X-axis acceleration value.</param>
        /// <param name="ay">Y-axis acceleration value.</param>
        /// <param name="az">Z-axis acceleration value.</param>
        public void DidReceiveAccelerometer(System.UInt64 timestamp, float ax, float ay, float az)
        {
            accelerometer.ReceiveData(ax, ay, az, timestamp);
        }

        /// <summary>
        /// Notifies when gyroscope data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="qx">X-axis gyroscope value.</param>
        /// <param name="qy">Y-axis gyroscope value.</param>
        /// <param name="qz">Z-axis gyroscope value.</param>
        public void DidReceiveGyroscope(System.UInt64 timestamp, float qx, float qy, float qz)
        {
            gyroscope.ReceiveData(qx, qy, qz, timestamp);
        }

        /// <summary>
        /// Notifies when magnetometer data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="mx">X-axis magnetometer value.</param>
        /// <param name="my">Y-axis magnetometer value.</param>
        /// <param name="mz">Z-axis magnetometer value.</param>
        public void DidReceiveMagnetometer(System.UInt64 timestamp, float mx, float my, float mz)
        {
            magnetometer.ReceiveData(mx, my, mz, timestamp);
        }

        /// <summary>
        /// Notifies when quaternion data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="qx">X-component of the quaternion.</param>
        /// <param name="qy">Y-component of the quaternion.</param>
        /// <param name="qz">Z-component of the quaternion.</param>
        /// <param name="qw">W-component of the quaternion.</param>
        public void DidReceiveQuaternion(System.UInt64 timestamp, float qx, float qy, float qz, float qw)
        {
            quaternion.ReceiveData(qw, qx, qy, qz, timestamp);
        }

        /// <summary>
        /// Notifies when orientation data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="roll">Roll value of the orientation.</param>
        /// <param name="pitch">Pitch value of the orientation.</param>
        /// <param name="yaw">Yaw value of the orientation.</param>
        public void DidReceiveOrientation(System.UInt64 timestamp, float roll, float pitch, float yaw)
        {
            orientation.ReceiveData(roll, pitch, yaw, timestamp);
        }

        /// <summary>
        /// Notifies when body position data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="bodyPosition">Body position data.</param>
        public void DidReceiveBodyPosition(System.UInt64 timestamp, BodyPosition bodyPosition)
        {
            this.bodyPosition.ReceiveData(bodyPosition, timestamp);
        }

        /// <summary>
        /// Notifies when activity data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="activity">Activity data.</param>
        public void DidReceiveActivity(System.UInt64 timestamp, ActivityType activity)
        {
            this.activity.ReceiveData(activity, timestamp);
        }

        /// <summary>
        /// Notifies when steps data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="steps">Steps data value.</param>
        public void DidReceiveSteps(System.UInt64 timestamp, System.UInt64 steps)
        {
            this.steps.ReceiveData(steps, timestamp);
        }

        /// <summary>
        /// Notifies when heart rate data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="heartRate">Heart rate data value.</param>
        public void DidReceiveHeartRate(System.UInt64 timestamp, int heartRate)
        {
            this.heartRate.ReceiveData(heartRate, timestamp);
        }

        /// <summary>
        /// Notifies when RR interval data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="rr">RR interval data value.</param>
        public void DidReceiveRr(System.UInt64 timestamp, int rr)
        {
            this.rr.ReceiveData(rr, timestamp);
        }

        /// <summary>
        /// Notifies when wear state changes.
        /// </summary>
        /// <param name="wearState">New wear state.</param>
        public void WearStateDidChange(WearState wearState)
        {
            this.wearState.ReceiveData(wearState, 0);
        }

        /// <summary>
        /// Notifies when an exercise is detected.
        /// </summary>
        /// <param name="exercise">Detected exercise.</param>
        public void DidDetectExercise(Exercise exercise)
        {
            this.exercise.ReceiveData(exercise, 0);
        }

        /// <summary>
        /// Notifies when sound volume data is received.
        /// </summary>
        /// <param name="timestamp">Timestamp of the received data.</param>
        /// <param name="value">Sound volume data value.</param>
        public void DidReceiveSoundVolume(System.UInt64 timestamp, System.UInt16 value)
        {
            soundVolume.ReceiveData(value, timestamp);
        }

        /// <summary>
        /// Handles the reception of pressure data from the Aidlab device.
        /// </summary>
        /// <param name="timestamp">The timestamp associated with the received pressure data.</param>
        /// <param name="pressure">The pressure value received from the Aidlab device.</param>
        public void DidReceivePressure(System.UInt64 timestamp, int pressure)
        {
            this.pressure.ReceiveData(pressure, timestamp);
        }

        /// <summary>
        /// Handles the change in wear state related to pressure data.
        /// </summary>
        /// <param name="wearState">The new wear state related to pressure data.</param>
        public void PressureWearStateDidChange(WearState wearState)
        {
            pressureWearState.ReceiveData(wearState, 0);
        }
    }
}
