using System;

namespace Aidlab
{
    public interface IAidlabDevice
    {
        public string FirmwareRevision { get; }
        public string HardwareRevision { get; }
        public IntPtr Address { get; }
    }
}
