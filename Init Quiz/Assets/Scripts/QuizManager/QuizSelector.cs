using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuizSelector : MonoBehaviour
{
    void Start()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
    public void BackToLogin()
    {
        SceneManager.LoadScene(SceneList.LoginScene);
    }
    public void PlayGame()
    {
        CurrentUser.userInfo.date = System.DateTime.Now;
        ResultManager.Init(SceneList.GameList, 1);
        ResultManager.Instance.welcomeScene = "";
        ResultManager.Instance.resultScene = SceneList.FinishScene;
        ResultManager.Instance.StartGame();
    }
}
