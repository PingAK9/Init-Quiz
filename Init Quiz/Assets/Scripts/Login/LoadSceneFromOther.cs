using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromOther : MonoBehaviour {
    public static bool isLoad = false;
    void Awake()
    {
        if (isLoad)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoad = true;
            SceneManager.LoadScene(0);
        }
    }
}
