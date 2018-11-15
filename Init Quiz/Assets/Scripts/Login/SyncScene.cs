using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SyncScene : MonoBehaviour
{
    public GameObject okBtn;
    public GameObject cancelBtn;
    public GameObject closeBtn;
    public Text progress;
    public Text titleMess;
    public BaseOnline connect;
    public GameObject comboBtn;

    private bool _isSending;

    private void Start()
    {
        progress.gameObject.SetActive(false);

        connect.OnFinishSend = OnFinishSend;
    }

    private void OnOK(GameObject btn)
    {
        if (SaveControl.dataSync.player.Count == 0)
        {
            titleMess.text = "Không có dữ liệu mới!";
            closeBtn.SetActive(true);
            comboBtn.SetActive(false);
            return;
        }

        titleMess.text = "Đang cập nhật dữ liệu lên Server...";
        connect.SendData(SaveControl.dataSync);
        comboBtn.SetActive(false);
        progress.gameObject.SetActive(true);
    }

    private void OnCancel(GameObject btn)
    {
        titleMess.text = "Hệ thống sẽ tiến hành cập nhật thông tin lên server!";
        comboBtn.SetActive(true);
        closeBtn.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnFinishSend(bool success)
    {
        _isSending = false;
        progress.gameObject.SetActive(false);

        if (success)
        {
            titleMess.text = "Cập nhật dữ liệu thành công!";
        }
        else
        {
            titleMess.text = "Cập nhật dữ liệu không thành công!";
        }

        closeBtn.SetActive(true);
    }

    private void Update()
    {
        if (_isSending)
        {
            progress.text = (int)connect.www.progress + "%";
        }
    }
}