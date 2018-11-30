using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveControl
{
    public static DataSync dataSync;

    private static string url = "http://abbott.ssd-asia.com/pharmacy/b2b_v2";

    public static void Post(Action<bool> response)
    {
        if (dataSync.player.Count == 0)
        {
            response(true);
            return;
        }

        BaseOnline.Instance.WWW(SaveControl.url, dataSync, (string message) => {
            try
            {
                if (BaseOnline.IsSuccess(message))
                {
                    dataSync.player = new List<UserInfo>();
                    response(true);
                }
                else
                {
                    response(false);
                }
            }
            catch (Exception)
            {
                response(false);
            }
        });

    }

    public static void SaveData()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            Debug.Log("Link file: " + Application.persistentDataPath);
            bf.Serialize(file, dataSync);
            file.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    static DataSync LoadData()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                DataSync dataLoad = (DataSync)bf.Deserialize(file);
                file.Close();
                return dataLoad;
            }
            else
            {
                Debug.Log("File isn't exist");
                return null;
            }
        }
        catch (Exception error)
        {
            Debug.LogError("Exception: " + error);
            return null;
        }
    }

    public static void ResetData()
    {
        dataSync.player.Clear();
        dataSync.player = new List<UserInfo>();
        SaveData();
    }

    public static void AddPlayerRecord(UserInfo getUserInfo)
    {
        Debug.Log("Add Player Record");
        UserInfo tempUser = new UserInfo();
        tempUser = getUserInfo;
        dataSync.player.Add(tempUser);
        SaveData();
    }

    public static void InitData()
    {
        dataSync = LoadData();
        if (dataSync == null)
        {
            dataSync = new DataSync();
            dataSync.device = new DeviceInfo();
        }
    }

}
