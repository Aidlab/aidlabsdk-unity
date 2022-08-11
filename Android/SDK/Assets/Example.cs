using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;

public class Example : MonoBehaviour, AidlabDelegate {

    void Start() {

        enableBluetooth();
        setLocationPermission();
        aidlab = new Aidlab(this);
    }

    void Update() {

        var cube = GameObject.Find("Cube");
        Vector3 position = cube.transform.position;
        position.y = normalizeRespiration(currentRespirationSample);
        cube.transform.position = position;
    }

    //-- AidlabDelegate ---------------------------------------------------------------
    
    public void didConnectAidlab(IAidlab aidlab) {}

    public void didDisconnectAidlab(IAidlab aidlab, DisconnectReason reason) {}

    public void didReceiveECG(IAidlab aidlab, Int64 timestamp, float[] values) {}

    public void didReceiveRespiration(IAidlab aidlab, Int64 timestamp, float[] values) {

        float val = values.Average();

        currentRespirationSample = val;
    }

    public void didReceiveBatteryLevel(IAidlab aidlab, int stateOfCharge) {}

    public void didReceiveSkinTemperature(IAidlab aidlab, Int64 timestamp, float value) {}

    public void didReceiveAccelerometer(IAidlab aidlab, Int64 timestamp, float ax, float ay, float az) {}
    
    public void didReceiveGyroscope(IAidlab aidlab, Int64 timestamp, float qx, float qy, float qz) {}
    
    public void didReceiveMagnetometer(IAidlab aidlab, Int64 timestamp, float mx, float my, float mz) {}
    
    public void didReceiveQuaternion(IAidlab aidlab, Int64 timestamp, float qw, float qx, float qy, float qz) {}

    public void didReceiveOrientation(IAidlab aidlab, Int64 timestamp, float roll, float pitch, float yaw) {}

    public void didReceiveBodyPosition(IAidlab aidlab, Int64 timestamp, BodyPosition bodyPosition) {}

    public void didReceiveActivity(IAidlab aidlab, Int64 timestamp, ActivityType activity) {}

    public void didReceiveSteps(IAidlab aidlab, Int64 timestamp, int steps) {}

    public void didReceiveHeartRate(IAidlab aidlab, Int64 timestamp, int heartRate) {}

    public void didReceiveHrv(IAidlab aidlab, Int64 timestamp, int[] hrv) {}

    public void didReceiveRespirationRate(IAidlab aidlab, Int64 timestamp, int value) {}

    public void wearStateDidChange(IAidlab aidlab, WearState wearState) {}

    public void didDetectExercise(IAidlab aidlab, Exercise exercise) {}

    public void didReceiveSoundVolume(IAidlab aidlab, long timestamp, int value) {}

    public void didReceiveSignalQuality(IAidlab aidlab, long timestamp, int value) {}

    public void didReceiveCommand(IAidlab aidlab) {}

    public void didReceiveError(string error) {}

    //-- Private ---------------------------------------------------------------

    private Aidlab aidlab;

    private float currentRespirationSample = 0.0f;

    private void enableBluetooth() {
      
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity")) {
            try {
                using (var BluetoothManager = activity.Call<AndroidJavaObject>("getSystemService", "bluetooth")) {
                    using (var BluetoothAdapter = BluetoothManager.Call<AndroidJavaObject>("getAdapter")) {
                        BluetoothAdapter.Call<bool>("enable");
                    }
                }
            } catch (Exception e) {
                
                Debug.Log(e);
            }
        }
    }

    private void setLocationPermission() {

        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
           
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    private float normalizeRespiration(float sample) {

        return Mathf.Clamp(sample * 10, -4, 4);
    }


}
