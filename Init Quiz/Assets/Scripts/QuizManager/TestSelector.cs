using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestSelector : MonoBehaviour
{
    void Start()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
    void BackToLogin(GameObject btn)
    {
        SceneManager.LoadScene(SceneList.LoginScene);
    }
    public void PlayGame()
    {
        CurrentUser.userInfo.date = System.DateTime.Now;
        ResultManager.Init(SceneList.GameList);
        SceneManager.LoadScene(ResultManager.Instance.allScene[0]);
    }
}
