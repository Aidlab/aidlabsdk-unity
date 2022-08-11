using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAidlab {

    string firmwareRevision { get; }
    string hardwareRevision { get; }
    string serialNumber { get; }

    void disconnect();

}
