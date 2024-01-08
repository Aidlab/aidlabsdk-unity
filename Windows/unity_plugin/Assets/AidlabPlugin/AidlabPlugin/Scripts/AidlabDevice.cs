using System;

namespace Aidlab
{
    /// <summary>
    /// The <c>AidlabDevice</c> class represents an Aidlab or Aidmed One device.
    /// </summary>
    /// <remarks>
    /// This class provides information about the device, such as firmware version,
    /// hardware version, and device address (MAC address).
    /// </remarks>
    public class AidlabDevice : IAidlabDevice
    {
        private string firmwareRevision;
        private string hardwareRevision;
        private IntPtr address;

        public string FirmwareRevision { get { return firmwareRevision; } }
        public string HardwareRevision { get { return hardwareRevision; } }
        public IntPtr Address { get { return address; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="AidlabDevice"/> class.
        /// </summary>
        /// <param name="firmwareRevision">The firmware version of the Aidlab device.</param>
        /// <param name="hardwareRevision">The hardware version of the Aidlab device.</param>
        /// <param name="address">The device address (MAC address) of the Aidlab device.</param>
        public AidlabDevice(string firmwareRevision, string hardwareRevision, IntPtr address)
        {
            this.firmwareRevision = firmwareRevision;
            this.hardwareRevision = hardwareRevision;
            this.address = address;
        }
    }
}
