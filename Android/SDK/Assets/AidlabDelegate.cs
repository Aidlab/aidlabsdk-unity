using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ActivityType {

    walking,
    running,
    still,
    cycling,
    automotive
}

public enum WearState {

    placedUpsideDown,
    placedProperly,
    detached
}

public enum Exercise {
    pushUp,
    jump,
    sitUp,
    burpee,
    pullUp,
    squat,
    plankStart,
    plankEnd
}

public enum BodyPosition
{
    undefined,
    front,
    back,
    leftSide,
    rightSide
}

public enum Signal {
    ecg,
    respiration,
    temperature,
    motion,
    battery,
    activity,
    orientation,
    steps,
    heartRate,
    healthThermometer,
    soundVolume
}

public enum DisconnectReason {
    timeout,
    deviceDisconnected,
    appDisconnected,
    sdkOutdated,
    unknownError
}

public interface AidlabDelegate {

    void didConnectAidlab(IAidlab aidlab);

    void didDisconnectAidlab(IAidlab aidlab, DisconnectReason reason);

    /**
     * Called when a new ECG sample was received.
     * @param  value     The new sample
     */
    void didReceiveECG(IAidlab aidlab, Int64 timestamp, float[] value);

    /**
     * Called when a new respiration sample was received.
     * @param  value     The new sample
     */
    void didReceiveRespiration(IAidlab aidlab, Int64 timestamp, float[] value);

    /**
     * Called when a new respiration rate was received.
     * @param value New respiration rate.
     */
    void didReceiveRespirationRate(IAidlab aidlab, Int64 timestamp, int value);

    /**
     * If battery monitoring is enabled, this event will notify about Aidlab's
     * state of charge. You never want Aidlab to run low on battery, as it can
     * lead to it's sudden turn off. Use this event to inform your users about
     * Aidlab's low energy.
     * @param stateOfCharge    Battery level in % (0.0 - 100.0).
     */
    void didReceiveBatteryLevel(IAidlab aidlab, int stateOfCharge);
    
    /**
     * Called when a skin temperature was received.
     * @param value    Skin temperature in °C
     */
    void didReceiveSkinTemperature(IAidlab aidlab, Int64 timestamp, float value);
    
    void didReceiveAccelerometer(IAidlab aidlab, Int64 timestamp, float ax, float ay, float az);
    
    void didReceiveGyroscope(IAidlab aidlab, Int64 timestamp, float qx, float qy, float qz);
    
    void didReceiveMagnetometer(IAidlab aidlab, Int64 timestamp, float mx, float my, float mz);
    
    void didReceiveQuaternion(IAidlab aidlab, Int64 timestamp, float qw, float qx, float qy, float qz);
    
    /**
     * Called when received orientation, represented in RPY angles.
     * @param roll
     * @param pitch
     * @param yaw
     */
    void didReceiveOrientation(IAidlab aidlab, Int64 timestamp, float roll, float pitch, float yaw);

    void didReceiveBodyPosition(IAidlab aidlab, Int64 timestamp, BodyPosition bodyPosition);
    
    /**
     * Called when a new activity type was detected.
     * @param activity  new activity type
     */
    void didReceiveActivity(IAidlab aidlab, Int64 timestamp, ActivityType activity);
    
    /**
     * Called when the number of total steps did change.
     * @param steps The total number of steps the user has taken with Aidlab.
     */
    void didReceiveSteps(IAidlab aidlab, Int64 timestamp, int steps);
    
    /**
     * Called when heart rate did change.
     * @param hrv  array of times between the peaks of the heart in ms
     * @param hearRate  new heart rate value
     */
    void didReceiveHeartRate(IAidlab aidlab, Int64 timestamp, int heartRate);

    void didReceiveHrv(IAidlab aidlab, Int64 timestamp, int[] hrv);
    
    /**
     * Called when a significant change of wear state did occur.
     * @param  wearState    Current wear state.
     */
    void wearStateDidChange(IAidlab aidlab, WearState wearState);
    
    void didDetectExercise(IAidlab aidlab, Exercise exercise);

    void didReceiveSoundVolume(IAidlab aidlab, Int64 timestamp, int value);

    void didReceiveSignalQuality(IAidlab aidlab, Int64 timestamp, int value);

    void didReceiveError(string error);
}