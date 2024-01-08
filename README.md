# Introduction

Home of AidlabSDK repository for Unity (Android and Windows only).

You can check our [website](http://www.aidlab.com/developer) to get the answers for the most common questions related to Aidlab.

# Windows

1. Import unitypackage
2. Create your example and call the static init method from the AidlabSDK class. When it is called, it will start scanning and trying to connect the nearest device.
3. Integrate signals from the device using methods from AidlabDelegate. Example can be found in the file `Example.cs` in the example folder.
4. To specify which signals to collect from the Aidlab device check GetCollectMethod in AidlabSDK.cs.

**Directory structure**

`ble_dll` - fork of [BleWinrtDll](https://github.com/adabru/BleWinrtDll), a Bluetooth supporting library for Windows written in C++.

`unity_plugin` - Utilities for Bluetooth connectivity for AidlabSDK. The whole project was exported to `unitypackage` for easy import.

`example` - Project example.

# Android

You can find the entire tutorial with description on our [blog](https://www.aidlab.com/pl/blog/reading-user-respiration-in-unity).

# Questions and Support

If you have problems, create an issue or join our [Discord channel](https://discord.gg/sPay3Xm).
