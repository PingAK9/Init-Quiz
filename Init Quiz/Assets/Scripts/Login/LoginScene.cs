using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    public GameObject SyncPanel;
    public GameObject LogoutPanel;
    public Text agentNameLbl;
    public GameObject warnPanel;

    private void Start()
    {
        agentNameLbl.text = SaveControl.dataSync.device.owner;

        int block = PlayerPrefs.GetInt("block", 0);

        if (block == 1)
        {
            warnPanel.SetActive(true);
        }
    }

    public void OnSync()
    {
        SyncPanel.SetActive(true);
    }

    public void OnLogOut()
    {
        LogoutPanel.SetActive(true);
    }

    public void OnConfirmLogOut()
    {
        SceneManager.LoadScene(SceneList.AgentScene);
    }

    public void OnCancelLogout()
    {
        LogoutPanel.SetActive(false);
    }
}