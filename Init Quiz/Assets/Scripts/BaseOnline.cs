using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using LitJson;

public class BaseOnline : MonoBehaviour
{
    public float timeout = 20;

    public delegate void FinishPost(bool success);

    public FinishPost OnFinishSend;
    public Hashtable headers;
    private readonly string url = "http://abbott.ssd-asia.com/pharmacy/b2b_v2";
    public WWW www;

    private IEnumerator Post(DataSync data)
    {
        bool haveData = false;
        float timeset = 0;

        if (data.player.Count == 0)
        {
            OnFinishSend(true);
            yield return false;
        }

        data.device.token = "6wJLq6cy2P";
        Debug.Log(JsonMapper.ToJson(data));
        string _json = JsonMapper.ToJson(data);
        byte[] dataSend = Encoding.UTF8.GetBytes(_json);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        www = new WWW(url, dataSend, headers);

        while (timeset <= timeout)
        {
            timeset++;
            if (!www.isDone)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("Waiting");
            }
            else
            {
                haveData = true;
                break;
            }
        }

        if (haveData)
        {
            try
            {
                Debug.Log(www.text);
                ServerMessage serverRes = JsonMapper.ToObject<ServerMessage>(www.text);
                if (serverRes.status.Contains("success"))
                {
                    SaveControl.control.ResetData();
                    OnFinishSend(true);
                }
                else if (serverRes.status.Contains("rewardDoctor"))
                {
                    PlayerPrefs.SetInt("Reward", 1);
                    OnFinishSend(true);
                    PlayerPrefs.Save();
                }
                else if (serverRes.status.Contains("rewardPharmacy"))
                {
                    PlayerPrefs.SetInt("Reward", 2);
                    OnFinishSend(true);
                    PlayerPrefs.Save();
                }
                else if (serverRes.status.Contains("block"))
                {
                    PlayerPrefs.SetInt("block", 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    OnFinishSend(false);
                }
                data.device.token = "";
            }
            catch (Exception err)
            {
                Debug.Log("Updata Error: " + err);
                OnFinishSend(false);
            }
        }
        else
        {
            OnFinishSend(false);
        }
    }

    //Only for testing
    private void InitData()
    {
        DeviceInfo deviceInfo = new DeviceInfo();

        DataSync data = new DataSync();
        data.device = deviceInfo;

        for (int i = 0; i < 1; i++)
        {
            UserInfo userInfo = new UserInfo();
            for (int j = 0; j < 10; j++)
            {
                QuestionResult qr = new QuestionResult();
                userInfo.result.Add(qr);
            }
            userInfo.fullname = i.ToString();
            data.player.Add(userInfo);
        }

        //SaveControl.control.Save(data);
        //Debug.Log(LitJson.JsonMapper.ToJson(data));
        StartCoroutine(Post(data));
        //StartCoroutine(Post(SaveControl.control.Load()));
    }

    public void SendData(DataSync data)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OnFinishSend(false);
        }
        StartCoroutine(Post(data));
    }
}