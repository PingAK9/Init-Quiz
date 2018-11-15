using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class DataSync
{
    public DeviceInfo device;
    public List<UserInfo> player = new List<UserInfo>();
}