using UnityEngine;
using System.Collections;
using System;

public abstract class Result : MonoBehaviour
{
    public GameObject AnswerPanel;
    public ResultItem resultPanel;

    public abstract void CheckResult();

    public virtual void CheckResult(bool result)
    {
        try
        {
            AnswerPanel.SetActive(true);
            //Show AnswerPanel
            if (QuizProgressBar.Instance != null)
            {
                int index = ResultManager.Instance.currentSceneIndex;

                if (index >= 0)
                {
                    QuizProgressBar.Instance.notifyQuizResult(index, result);
                    if (ResultItem.Instance != null)
                    {
                        if (!result)
                            ResultItem.Instance.show(EProgressBarItemState.Incorrect);
                        else
                            ResultItem.Instance.show(QuizProgressBar.Instance.getItemState(index));
                    }
                }
                else
                {
                    if (ResultItem.Instance != null)
                    {
                        if (!result)
                            ResultItem.Instance.show(EProgressBarItemState.Incorrect);
                        else
                            ResultItem.Instance.show(EProgressBarItemState.FullCorrect);
                    }
                }

                ResultManager.Instance.currentResult[ResultManager.Instance.currentSceneIndex] =
                    QuizProgressBar.Instance.getItemState(index);
            }

            //Save the Result
            ResultManager.Instance.questionTracker[ResultManager.Instance.currentSceneIndex].correct = result;
        }
        catch (Exception error)
        {
            Debug.LogError("Exception: " + error);
        }
    }

    public virtual void OnCheckResult(GameObject btn)
    {
        CheckResult();
    }
}