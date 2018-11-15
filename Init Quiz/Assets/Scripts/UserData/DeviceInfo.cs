using UnityEngine;
using System.Collections;
using System;
#if UNITY_IOS || UNITY_TVOS
using UnityEngine.iOS;

#endif

[Serializable]
public class DeviceInfo
{
    public string client_name = SystemInfo.deviceName;
    public string client_version = SystemInfo.deviceModel;
    public string platform_name = SystemInfo.operatingSystem;

#if UNITY_EDITOR
    public string platform_version = "1.0.0";
#elif UNITY_IOS || UNITY_IPHONE
    public string platform_version = Device.generation.ToString();
#endif

    public string uid = SystemInfo.deviceUniqueIdentifier;
    public string token;
    public string owner = "";
    public string branch = "";
}