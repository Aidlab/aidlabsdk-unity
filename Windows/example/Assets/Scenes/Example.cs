using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Aidlab.AidlabSDK.init();
        Aidlab.AidlabSDK.aidlabDelegate.temperature.Subscribe(ReceiveTemperature);
        Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(ReceiveWearState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReceiveTemperature()
    {
        Debug.Log("Temperature: " + Aidlab.AidlabSDK.aidlabDelegate.temperature.value + " [*C]");
    }

    private void ReceiveWearState()
    {
        Debug.Log("WearState: " + Aidlab.AidlabSDK.aidlabDelegate.wearState.value);
    }
}
