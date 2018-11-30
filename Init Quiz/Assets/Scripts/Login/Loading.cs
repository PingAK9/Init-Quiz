using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	void Start () {

        LoadSceneFromOther.isLoad = true;
        SaveControl.InitData();
        if (string.IsNullOrEmpty(SaveControl.dataSync.device.owner))
        {
            SceneManager.LoadScene(SceneList.AgentScene);
        }else
        {
            SceneManager.LoadScene(SceneList.LoginScene);
        }
    }
}
