using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aidlab : AndroidJavaProxy, IAidlab {

    public string firmwareRevision { 
        get{
            return this.aidlab.Get<String>("firmwareRevision");
        } 
    }
    public string hardwareRevision { 
        get{
            return this.aidlab.Get<String>("hardwareRevision");
        } 
    }
    public string serialNumber { 
        get{
            return this.aidlab.Get<String>("serialNumber");
        } 
    }
    
    public void disconnect() {
        this.aidlab.Call("disconnect");
    }

    public Aidlab(AidlabDelegate aidlabDelegate) : base("com.aidlab.sdk.communication.AidlabDelegate") {

        this.aidlabDelegate = aidlabDelegate;

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AidlabSDK aidlabSDK = new AidlabSDK(this);

        androidAidlabSDK = new AndroidJavaObject("com.aidlab.sdk.communication.AidlabSDK", currentActivity, aidlabSDK);

        bool status = androidAidlabSDK.Call<bool>("checkBluetooth", currentActivity);
    }

    void didConnectAidlab(AndroidJavaObject aidlab) {

        this.aidlab = aidlab;

        this.aidlabDelegate.didConnectAidlab(this);
    }

    void didDisconnectAidlab(AndroidJavaObject aidlab, AndroidJavaObject reason) {
       
        this.deviceDetected = false;

        string reasonStr = reason.Call<string>("toString");
        DisconnectReason reasonEnum = DisconnectReason.unknownError;

        if(reasonStr == "timeout") {
            reasonEnum = DisconnectReason.timeout;
        }
        else if(reasonStr == "deviceDisconnected") {
            reasonEnum = DisconnectReason.deviceDisconnected;
        }
        else if (reasonStr == "appDisconnected") {
            reasonEnum = DisconnectReason.appDisconnected;
        }
        else if (reasonStr == "sdkOutdated") {
            reasonEnum = DisconnectReason.sdkOutdated;
        }
        else {
            reasonEnum = DisconnectReason.unknownError;
        }

        this.aidlabDelegate.didDisconnectAidlab(this, reasonEnum);
    }

    void didReceiveECG(AndroidJavaObject aidlab, Int64 timestamp, float[] values) {

        this.aidlabDelegate.didReceiveECG(this, timestamp, values);
    }

    void didReceiveRespiration(AndroidJavaObject aidlab, Int64 timestamp, float[] values) {
        
        this.aidlabDelegate.didReceiveRespiration(this, timestamp, values);
    }

    void didReceiveRespirationRate(AndroidJavaObject aidlab, Int64 timestamp, int value) {

        this.aidlabDelegate.didReceiveRespirationRate(this, timestamp, value);
    }

    void didReceiveBatteryLevel(AndroidJavaObject aidlab, int stateOfCharge) {

        this.aidlabDelegate.didReceiveBatteryLevel(this, stateOfCharge);
    }
    
    void didReceiveSkinTemperature(AndroidJavaObject aidlab, Int64 timestamp, float value) {

        this.aidlabDelegate.didReceiveSkinTemperature(this, timestamp, value);
    }
    
    public void didReceiveAccelerometer(AndroidJavaObject aidlab, Int64 timestamp, float ax, float ay, float az) {

        this.aidlabDelegate.didReceiveAccelerometer(this, timestamp, ax, ay, az);
    }
    
    public void didReceiveGyroscope(AndroidJavaObject aidlab, Int64 timestamp, float qx, float qy, float qz) {

        this.aidlabDelegate.didReceiveGyroscope(this, timestamp, qx, qy, qz);
    }
    
    public void didReceiveMagnetometer(AndroidJavaObject aidlab, Int64 timestamp, float mx, float my, float mz) {

        this.aidlabDelegate.didReceiveMagnetometer(this, timestamp, mx, my, mz);
    }
    
    public void didReceiveQuaternion(AndroidJavaObject aidlab, Int64 timestamp, float qw, float qx, float qy, float qz) {

        this.aidlabDelegate.didReceiveQuaternion(this, timestamp, qw, qx, qy, qz);
    }
    
    void didReceiveOrientation(AndroidJavaObject aidlab, Int64 timestamp, float roll, float pitch, float yaw) {

        this.aidlabDelegate.didReceiveOrientation(this, timestamp, roll, pitch, yaw);
    }
    void didReceiveBodyPosition(AndroidJavaObject aidlab, Int64 timestamp, AndroidJavaObject bodyPosition) {

        String bodyPositionStr = bodyPosition.Call<string>("toString");
        BodyPosition bodyPositionEnum = BodyPosition.back;

        if(bodyPositionStr == "undefined") {
            bodyPositionEnum = BodyPosition.undefined;
        }
        else if(bodyPositionStr == "front") {
            bodyPositionEnum = BodyPosition.front;
        }
        else if (bodyPositionStr == "back") {
            bodyPositionEnum = BodyPosition.back;
        }
        else if (bodyPositionStr == "leftSide") {
            bodyPositionEnum = BodyPosition.leftSide;
        }
        else if (bodyPositionStr == "rightSide") {
            bodyPositionEnum = BodyPosition.rightSide;
        }

        this.aidlabDelegate.didReceiveBodyPosition(this, timestamp, bodyPositionEnum);
    }
    void didReceiveActivity(AndroidJavaObject aidlab, Int64 timestamp, AndroidJavaObject activity) {

        string activityString = activity.Call<string>("toString");

        if (activityString == "STILL") {
            this.aidlabDelegate.didReceiveActivity(this, timestamp, ActivityType.still);
        } else if (activityString == "WALKING") {
            this.aidlabDelegate.didReceiveActivity(this, timestamp, ActivityType.walking);
        } else if (activityString == "RUNNING") {
            this.aidlabDelegate.didReceiveActivity(this, timestamp, ActivityType.running);
        } else if (activityString == "CYCLING") {
            this.aidlabDelegate.didReceiveActivity(this, timestamp, ActivityType.cycling);
        } else if (activityString == "AUTOMOTIVE") {
            this.aidlabDelegate.didReceiveActivity(this, timestamp, ActivityType.automotive);
        } 
    }

    void didReceiveSteps(AndroidJavaObject aidlab, Int64 timestamp, int steps) {

        this.aidlabDelegate.didReceiveSteps(this, timestamp, steps);
    }

    void didReceiveSignalQuality(AndroidJavaObject aidlab, Int64 timestamp, int value) {

        this.aidlabDelegate.didReceiveSignalQuality(this, timestamp, value);
    }
    
    void didReceiveHeartRate(AndroidJavaObject aidlab, Int64 timestamp, int heartRate) {

        this.aidlabDelegate.didReceiveHeartRate(this, timestamp, heartRate);
    }

    void didReceiveHrv(AndroidJavaObject aidlab, Int64 timestamp, int[] hrv) {

        this.aidlabDelegate.didReceiveHrv(this, timestamp, hrv);
    }
  
    void wearStateDidChange(AndroidJavaObject aidlab, AndroidJavaObject wearState) {

        string wearStateString = wearState.Call<string>("toString");

        if (wearStateString == "placedProperly") {
            this.aidlabDelegate.wearStateDidChange(this, WearState.placedProperly);
        } else if (wearStateString == "placedUpsideDown") {
            this.aidlabDelegate.wearStateDidChange(this, WearState.placedUpsideDown);
        } else if (wearStateString == "loose") {
            this.aidlabDelegate.wearStateDidChange(this, WearState.detached);
        } else if (wearStateString == "detached") {
            this.aidlabDelegate.wearStateDidChange(this, WearState.detached);
        }
    }

    void didDetectExercise(AndroidJavaObject aidlab, AndroidJavaObject exercise) {

        this.aidlab = aidlab;

        string exerciseString = exercise.Call<string>("toString");

        if( exerciseString == "jump") {
            this.aidlabDelegate.didDetectExercise(this, Exercise.jump);
        }
        else if(exerciseString == "pushUp") {
            this.aidlabDelegate.didDetectExercise(this, Exercise.pushUp);
        }
        else if(exerciseString == "sitUp") {
            this.aidlabDelegate.didDetectExercise(this, Exercise.sitUp);
        }
        else if(exerciseString == "burpee") {
            this.aidlabDelegate.didDetectExercise(this, Exercise.burpee);
        }
        else if(exerciseString == "pullUp")
        {
            this.aidlabDelegate.didDetectExercise(this, Exercise.pullUp);
        }
        else if (exerciseString == "squat")
        {
            this.aidlabDelegate.didDetectExercise(this, Exercise.squat);
        }
        else if (exerciseString == "plankStart")
        {
            this.aidlabDelegate.didDetectExercise(this, Exercise.plankStart);
        }
        else if (exerciseString == "plankEnd")
        {
            this.aidlabDelegate.didDetectExercise(this, Exercise.plankEnd);
        }

    }

    void didReceiveSoundVolume(AndroidJavaObject aidlab, Int64 timestamp, int value)
    {
        this.aidlabDelegate.didReceiveSoundVolume(this, timestamp, value);
    }

    void didReceiveCommand(AndroidJavaObject aidlab) {}

    void didReceiveError(string error)
    {
        this.aidlabDelegate.didReceiveError(error);
    }

    void didReceiveMessage(AndroidJavaObject aidlab, string process, string message) {}


    public void onDeviceDetectedEvent() {
        
        this.deviceDetected = true;
    }

    public void onDeviceScanStartedEvent() {}

    public void onDeviceScanStoppedEvent() {
       
        /// No aidlab connected - scan again
        if (!this.deviceDetected)
            scanForAidlab();
    }

    public void onBluetoothStarted() {
       
        this.scanForAidlab();
    }

    public bool isAidlabDetected() {

        return this.deviceDetected;
    }

    //-- Private ---------------------------------------------------------------

    private AidlabDelegate aidlabDelegate;

    private AndroidJavaObject aidlab;
    private AndroidJavaObject androidAidlabSDK;
    private bool deviceDetected = false;
    
    private void scanForAidlab() {
        
        /// Clear the detected device list first to detect devices that were
        /// already detected once
        this.androidAidlabSDK.Call("clearDeviceList");
        this.androidAidlabSDK.Call("scanForDevices");
    }
}