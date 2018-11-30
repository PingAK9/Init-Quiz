using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    public Text txtOwer;

    private void Start()
    {
        txtOwer.text = SaveControl.dataSync.device.owner;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void OnLogOut()
    {
        SceneManager.LoadScene(SceneList.AgentScene);
        gameObject.SetActive(false);
    }
}