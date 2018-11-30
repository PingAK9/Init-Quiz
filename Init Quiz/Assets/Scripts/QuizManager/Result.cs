using UnityEngine;
using System.Collections;
using System;

public abstract class Result : MonoBehaviour
{
    public GameObject AnswerPanel;
    public ResultItem resultPanel;

    public virtual void CheckResult()
    {
    }

    public virtual void CheckResult(bool result)
    {
        try
        {
            AnswerPanel.SetActive(true);
            int index = ResultManager.Instance.currentSceneIndex;
            EProgressBarItemState state = ResultManager.Instance.UpdateResult(index, result);
            if (ResultItem.Instance != null)
            {
                ResultItem.Instance.show(state);
            }

        }
        catch (Exception error)
        {
            Debug.LogError("Exception: " + error);
        }
    }

}