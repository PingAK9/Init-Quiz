using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SyncScene : MonoBehaviour
{
    public GameObject objConfirm;
    public GameObject objNodata;
    public GameObject objSuccess;
    public GameObject objUpdating;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        objSuccess.SetActive(false);
        objUpdating.SetActive(false);
        if (SaveControl.dataSync.player.Count == 0)
        {
            objNodata.SetActive(true);
            objConfirm.SetActive(false);
        }
        else
        {
            objConfirm.SetActive(true);
            objNodata.SetActive(false);
        }
    }
    public void OnClickSend()
    {
        objConfirm.SetActive(false);
        objUpdating.SetActive(true);
        // send data
        OnFinishSend("");
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }

    private void OnFinishSend(string message)
    {
        objUpdating.SetActive(false);
        objSuccess.SetActive(true);
    }

}