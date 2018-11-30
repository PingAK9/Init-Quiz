using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_IOS || UNITY_IPHONE
using UnityEngine.iOS;
#endif

[Serializable]
public class DataSync
{
    public DeviceInfo device;
    public List<UserInfo> player = new List<UserInfo>();
    public DataSync()
    {
        device = new DeviceInfo();
        player = new List<UserInfo>();
    }
}
[Serializable]
public class UserInfo
{
    public string fullname = "";
    public string hospital = "";
    public string major = "";
    public string place = "";
    public float lat = 0f;
    public float lng = 0f;
    public int timeplay = 0;
    public int numberCorrect = 0;
    public int numberIncorrect = 0;
    public DateTime date;
    public bool isDoctor = true;
    public List<QuestionResult> result = new List<QuestionResult>();
    public UserInfo()
    {
        result = new List<QuestionResult>();
    }
}

[Serializable]
public class QuestionResult
{
    public int test_type = 0;
    public bool isDone = false;
    public int question_order = 0;
    public string user_answer = "";
    public bool correct = false;
}

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
    public string token = "6wJLq6cy2P"; // default token
    public string owner = "";
    public string branch = "";
}


public static class CurrentUser
{
    public static UserInfo userInfo;
}