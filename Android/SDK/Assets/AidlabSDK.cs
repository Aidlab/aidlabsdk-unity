using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class AidlabSDK : AndroidJavaProxy {
 
    Aidlab aidlab = null;

    public AidlabSDK(Aidlab aidlab)
        : base("com.aidlab.sdk.communication.AidlabSDKDelegate") {
       
        this.aidlab = aidlab;
    }

    /**
     * Called after detecting a new aidlab device
     * @param  aidlab      Aidlab that was detected
     */
    void onAidlabDetected(AndroidJavaObject device, int rssi) {

        /// Connect to first found device
        if (!this.aidlab.isAidlabDetected()) {

            this.aidlab.onDeviceDetectedEvent();

            // choose the signal which you would like to subscribe to
            //Signal[] signals = { Signal.respiration };

            Signal[] signals = { Signal.respiration };

            device.Call("connect", signals.Select(x => (int) x).ToArray(), false, this.aidlab);
        }
    }

    /**
    * Called after obtaining necessary permissions and enabling bluetooth
    */
    void onBluetoothStarted() {

        this.aidlab.onBluetoothStarted();
    }

    /**
    * Called after the device scan was started
    */
    void onDeviceScanStarted() {

        this.aidlab.onDeviceScanStartedEvent();
    }

    /**
     * Called after the device scan was stopped
     */
    void onDeviceScanStopped() {

        this.aidlab.onDeviceScanStoppedEvent();
    } 
}
