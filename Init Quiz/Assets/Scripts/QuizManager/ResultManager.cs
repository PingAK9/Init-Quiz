using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class ResultManager
{
    /// <summary>
    /// Ref to the current Instance of this Class in App
    /// </summary>
    public static ResultManager Instance;
    /// <summary>
    /// The current question.
    /// </summary>

    #region Score Infomation
    public string[] allScene;
    public int currentSceneIndex;
    /// <summary>
    /// The total Question User have seen.
    /// </summary>
    public int TotalQuest;
    /// <summary>
    /// The number of Correct question that user Answer.
    /// </summary>
    public int CorrectQuest;
    /// <summary>
    /// The Current Star Status of this question
    /// </summary>
    public EProgressBarItemState[] currentResult;
    /// <summary>
    /// Mark this question has showed tutorial or not
    /// </summary>
    public bool[] haveTutorial;

    public QuestionResult[] questionTracker;

    #endregion
    public static void Init(string[] allScene)
    {
        Instance = new ResultManager();
        Instance.allScene = allScene;
        Instance.TotalQuest = 0;
        Instance.currentSceneIndex = 0;
        Instance.CorrectQuest = 0;

        Instance.currentResult = new EProgressBarItemState[Instance.allScene.Length];
        Instance.questionTracker = new QuestionResult[Instance.allScene.Length];
        for (int i = 0; i < Instance.allScene.Length; i++)
        {
            Instance.questionTracker[i] = new QuestionResult();
            Instance.questionTracker[i].test_type = 1;
            Instance.questionTracker[i].question_order = i;
        }
    }
    public void OnRetry()
    {
        SceneManager.LoadScene(allScene[currentSceneIndex]);

    }
    public void OnBack()
    {
        currentSceneIndex--;
        if (currentSceneIndex < 0)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(allScene[currentSceneIndex]);
    }
    public void OnNext()
    {
        currentSceneIndex++;
        if (TotalQuest < currentSceneIndex)
        {
            TotalQuest = currentSceneIndex;
        }

        if (currentSceneIndex >= allScene.Length)
        {
            SceneManager.LoadScene(SceneList.FinishScene);
        }
        else
        {
            SceneManager.LoadScene(allScene[currentSceneIndex]);
        }
    }
    /// <summary>
    /// Gets the final score.
    /// </summary>
    /// <returns>The final score.</returns>
    public int GetFinalScore()
    {
        Instance.CorrectQuest = 0;
        for (int i=1; i< Instance.currentResult.Length; i++)
        {
            if (Instance.currentResult [i] == EProgressBarItemState.FullCorrect || Instance.currentResult [i] == EProgressBarItemState.HalfCorrect)
            {
                Instance.CorrectQuest++;
            }
        }
        return Instance.CorrectQuest;
    }
    /// <summary>
    /// Get the Result in List
    /// </summary>
    /// <returns>The quiz result.</returns>
    public List<QuestionResult> GetQuizResult()
    {
        Instance.TotalQuest = 0;
        List<QuestionResult> results = new List<QuestionResult>();
        for (int i=0; i< Instance.questionTracker.Length; i++)
        {
            if (Instance.questionTracker[i].isDone)
            {
                Instance.TotalQuest++;
                results.Add(Instance.questionTracker[i]);
            }
        }
        return results;
    }

    public void SaveData()
    {
        CurrentUser.userInfo.result = new List<QuestionResult>();
        CurrentUser.userInfo.result = GetQuizResult();
        CurrentUser.userInfo.numberCorrect = GetFinalScore();
        CurrentUser.userInfo.numberIncorrect =Instance.TotalQuest - GetFinalScore();
        CurrentUser.userInfo.timeplay = (int)(DateTime.Now - CurrentUser.userInfo.date).TotalSeconds;
        SaveControl.control.AddPlayerRecord(CurrentUser.userInfo);
        SaveControl.control.Save();
    }
}
