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
    public string welcomeScene;
    public string resultScene;
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

    public EProgressBarItemState UpdateResult(int paramIndex, bool paramCorrect)
    {
        EProgressBarItemState state = ResultManager.Instance.currentResult[paramIndex];
        EProgressBarItemState newState = state;
        switch (state)
        {
            case EProgressBarItemState.Empty:
                if (paramCorrect)
                    newState = EProgressBarItemState.FullCorrect;
                else
                    newState = EProgressBarItemState.Incorrect;
                break;

            case EProgressBarItemState.Incorrect:
                if (paramCorrect)
                    newState = EProgressBarItemState.HalfCorrect;
                break;
        }

        currentResult[paramIndex] = newState;
        if (newState == EProgressBarItemState.Empty || newState == EProgressBarItemState.Incorrect)
        {
            questionTracker[paramIndex].correct = false;
        }else
        {
            questionTracker[paramIndex].correct = true;
        }

        if (QuizProgressBar.Instance != null)
        {
            QuizProgressBar.Instance.SetItemState(paramIndex);
        }
        return newState;
    }
    #endregion
    public static void Init(string[] allScene, int test_type)
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
            Instance.questionTracker[i].test_type = test_type;
            Instance.questionTracker[i].question_order = i;
        }
    }
    public void StartGame()
    {
        if (string.IsNullOrEmpty(welcomeScene) == false)
        {
            SceneManager.LoadScene(Instance.welcomeScene);
        }else
        {
            SceneManager.LoadScene(Instance.allScene[0]);
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
        SaveControl.AddPlayerRecord(CurrentUser.userInfo);
    }
}
