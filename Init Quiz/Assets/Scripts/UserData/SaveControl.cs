using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveControl : MonoBehaviour
{
    public static SaveControl control;
    public static DataSync dataSync;

    private void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
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

    public DataSync Load()
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
            Debug.Log("File isn't exist");
            return null;
        }
        catch (Exception error)
        {
            Debug.LogError("Exception: " + error);
            return null;
        }
    }

    public void ResetData()
    {
        dataSync.player.Clear();
        dataSync.player = new List<UserInfo>();
        Save();
    }

    public void AddPlayerRecord(UserInfo getUserInfo)
    {
        Debug.Log("Add Player Record");
        UserInfo tempUser = new UserInfo();
        tempUser = getUserInfo;
        dataSync.player.Add(tempUser);
    }

    private void InitData()
    {
        dataSync = Load();

        if (dataSync == null)
        {
            dataSync = new DataSync();
            dataSync.device = new DeviceInfo();
        }

        if (string.IsNullOrEmpty(dataSync.device.owner))
        {
            SceneManager.LoadScene(SceneList.AgentScene);
        }
    }

    private void Start()
    {
        InitData();
    }
}