using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AgentLogin : MonoBehaviour
{
    public InputField agentName;
    public Dropdown place;
    public Text notice;

    private void OnReg()
    {
        if (string.IsNullOrEmpty(agentName.text))
        {
            notice.text = "Thiếu thông tin!";
            return;
        }

        SaveControl.dataSync.device.owner = agentName.text;
        SaveControl.dataSync.device.branch = place.options[place.value].text;
        SaveControl.control.Save();
        SceneManager.LoadScene(SceneList.LoginScene);
    }
}