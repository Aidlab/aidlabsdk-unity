using System;

namespace Aidlab
{
    public class AidlabDevice : IAidlabDevice
    {
        private string firmwareRevision;
        private string hardwareRevision;
        private IntPtr address;

        public string FirmwareRevision { get { return firmwareRevision; } }
        public string HardwareRevision { get { return hardwareRevision; } }
        public IntPtr Address { get { return address; } }


        public AidlabDevice(string firmwareRevision, string hardwareRevision, IntPtr address)
        {
            this.firmwareRevision = firmwareRevision;
            this.hardwareRevision = hardwareRevision;
            this.address = address;
        }
    }
}
