using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IndexQuestion : MonoBehaviour
{
    public Text questNo;

    private void Start()
    {
        questNo.text = "" + (ResultManager.Instance.currentSceneIndex);
    }
}