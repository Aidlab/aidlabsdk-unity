using Aidlab.BLE;

namespace Aidlab
{
    public static class AidlabCharacteristicsUUID
    {
        private static FullCharacteristic cmdUUID = new FullCharacteristic(
            "{44366e80-cf3a-11e1-9ab4-0002a5d5c51b}", "{51366e80-cf3a-11e1-9ab4-0002a5d5c51b}");
        private static FullCharacteristic firmwareUUID = new FullCharacteristic(
            "{0000180a-0000-1000-8000-00805f9b34fb}", "{00002a26-0000-1000-8000-00805f9b34fb}");
        private static FullCharacteristic hardwareUUID = new FullCharacteristic(
            "{0000180a-0000-1000-8000-00805f9b34fb}", "{00002a27-0000-1000-8000-00805f9b34fb}");

        public static FullCharacteristic CmdUUID { get { return cmdUUID; } }
        public static FullCharacteristic FirmwareUUID { get { return firmwareUUID; } }
        public static FullCharacteristic HardwareUUID { get { return hardwareUUID; } }

        // Enter chosen characteristics:
        public static FullCharacteristic[] Characteristics = { CmdUUID };
    }
}
