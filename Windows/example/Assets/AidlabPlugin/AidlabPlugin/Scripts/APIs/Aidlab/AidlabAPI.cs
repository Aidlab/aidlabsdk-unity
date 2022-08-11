using System;
using System.Runtime.InteropServices;

namespace Aidlab
{
    public class AidlabAPI
    {
       
        [DllImport("aidlabsdk.dll", EntryPoint = "initial")]
        public static extern IntPtr Initial();

        [DllImport("aidlabsdk.dll", EntryPoint = "setFirmwareRevision")]
        public static extern void SetFirmwareRevision(byte[] firmwareRevision, int size, IntPtr context);

        [DllImport("aidlabsdk.dll", EntryPoint = "setHardwareRevision")]
        public static extern void SetHardwareRevision(byte[] hardwareRevision, int size, IntPtr context);

        [DllImport("aidlabsdk.dll", EntryPoint = "get_collect_command")]
        public static extern IntPtr GetCollectCommand(byte[] realSignals, int realSize, byte[] syncSignals, int syncSize, IntPtr context);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void ecg_c_callback(System.IntPtr context, System.UInt64 timestamp, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] values, int size);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void respiration_c_callback(System.IntPtr context, System.UInt64 timestamp, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] values, int size);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void temperature_c_callback(System.IntPtr context, System.UInt64 timestamp, float value);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void activity_c_callback(System.IntPtr context, System.UInt64 timestamp, byte activity);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void steps_c_callback(System.IntPtr context, System.UInt64 timestamp, System.UInt64 steps);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void accelerometer_c_callback(System.IntPtr context, System.UInt64 timestamp, float ax, float ay, float az);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void gyroscope_c_callback(System.IntPtr context, System.UInt64 timestamp, float gx, float gy, float gz);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void magnetometer_c_callback(System.IntPtr context, System.UInt64 timestamp, float mx, float my, float mz);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void quaternion_c_callback(System.IntPtr context, System.UInt64 timestamp, float qw, float qx, float qy, float qz);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void orientation_c_callback(System.IntPtr context, System.UInt64 timestamp, float roll, float pitch, float yaw);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void body_position_c_callback(System.IntPtr context, System.UInt64 timestamp, byte bodyPosition);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void heart_rate_c_callback(System.IntPtr context, System.UInt64 timestamp, int heartRate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void rr_c_callback(System.IntPtr context, System.UInt64 timestamp, int rr);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void respiration_rate_c_callback(System.IntPtr context, System.UInt64 timestamp, System.UInt32 value);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void wear_state_c_callback(System.IntPtr context, byte wearState);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void sound_volume_c_callback(System.IntPtr context, System.UInt64 timestamp, System.UInt16 soundVolume);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void exercise_c_callback(System.IntPtr context, byte exercise);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void receive_command_c_callback(System.IntPtr context);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void pressure_c_callback(System.IntPtr context, System.UInt64 timestamp, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] int[] pressure, int size);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void pressure_wear_state_c_callback(System.IntPtr context, byte pressureWearState);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void received_message_c_callback(System.IntPtr context, [MarshalAs(UnmanagedType.LPStr)] string process, [MarshalAs(UnmanagedType.LPStr)] string message);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void user_event_c_callback(System.IntPtr context, System.UInt64 timestamp);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void battery_c_callback(System.IntPtr context, byte stateOfCharge);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void signal_quality_c_callback(System.IntPtr context, System.UInt64 timestamp, int value);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void error_c_callback(System.IntPtr context, [MarshalAs(UnmanagedType.LPStr)] string error);


        [DllImport("aidlabsdk.dll", EntryPoint = "internalProcessCMD")]
        public static extern void InternalProcessCMD(
            byte[] data, 
            int size,
            [MarshalAs(UnmanagedType.FunctionPtr)] ecg_c_callback ecg_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] respiration_c_callback respiration_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] temperature_c_callback temperature_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] accelerometer_c_callback accelerometer_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] gyroscope_c_callback gyroscope_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] magnetometer_c_callback magnetometer_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] battery_c_callback battery_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] activity_c_callback activity_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] steps_c_callback steps_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] orientation_c_callback orientation_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] quaternion_c_callback quaternion_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] respiration_rate_c_callback respiration_rate_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] wear_state_c_callback wear_state_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] heart_rate_c_callback heart_rate_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] rr_c_callback rr_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] sound_volume_c_callback sound_volume_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] exercise_c_callback exercise_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] receive_command_c_callback receive_command_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] received_message_c_callback received_message_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] user_event_c_callback user_event_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] pressure_c_callback pressure_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] pressure_wear_state_c_callback pressure_wear_state_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] body_position_c_callback body_position_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] error_c_callback error_c_callback,
            [MarshalAs(UnmanagedType.FunctionPtr)] signal_quality_c_callback signal_quality_c_callback,
            IntPtr handler);
    }
}
