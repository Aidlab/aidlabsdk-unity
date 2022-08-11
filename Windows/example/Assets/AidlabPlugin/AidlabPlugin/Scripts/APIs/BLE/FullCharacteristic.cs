using System.Collections.Generic;

namespace Aidlab.BLE
{
    public class FullCharacteristic
    {
        public string serviceUuid;
        public string characteristicUuid;

        public FullCharacteristic(string serviceUuid, string characteristicUuid)
        {
            this.serviceUuid = serviceUuid;
            this.characteristicUuid = characteristicUuid;
        }

        public override bool Equals(object obj)
        {
            return obj is FullCharacteristic characteristic &&
                serviceUuid == characteristic.serviceUuid &&
                characteristicUuid == characteristic.characteristicUuid;
        }

        public bool Equals(string serviceUuid, string characteristicUuid)
        {
            return this.serviceUuid == serviceUuid &&
                this.characteristicUuid == characteristicUuid;
        }

        public override int GetHashCode()
        {
            int hashCode = 1763003582;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(serviceUuid);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(characteristicUuid);
            return hashCode;
        }
    }
}
