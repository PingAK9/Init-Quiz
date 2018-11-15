using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class QuizProgressBar : MonoBehaviour
{
    #region Fields
    public GameObject itemPrefab;
    public Transform rootStart;
    public Sprite EmptyItemIcon;
    public Sprite IncorrectItemIcon;
    public Sprite HalfCorrectItemIcon;
    public Sprite FullCorrectItemIcon;

    public static QuizProgressBar Instance { get; private set; }

    List<ProgressBarItem> currentItems;
    List<EProgressBarItemState> currentItemStates;
    #endregion

    /////////////////////////////////////////////////////////////////////////////////////

    #region Events

    public void notifyQuizResult(int paramIndex, bool paramCorrect)
    {
        updateItemState(paramIndex - 1, paramCorrect);
    }

    public void notifyQuizChanged(int paramNewIndex)
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (i == (paramNewIndex - 1))
                currentItems [i].startRotation();
            else
                currentItems [i].stopRoration();
        }
    }

    #endregion


    #region Operations
    void BackQuestion()
    {
        ResultManager.Instance.OnBack();
    }

    void BackToMain()
    {
        ResultManager.Instance.SaveData();
        SceneManager.LoadScene(SceneList.LoginScene);
    }
       

    Sprite getIconFromState(EProgressBarItemState paramState)
    {
        switch (paramState)
        {
            case EProgressBarItemState.Empty:
                return EmptyItemIcon;

            case EProgressBarItemState.Incorrect:
                return IncorrectItemIcon;

            case EProgressBarItemState.HalfCorrect:
                return HalfCorrectItemIcon;

            case EProgressBarItemState.FullCorrect:
                return FullCorrectItemIcon;

            default:
                return EmptyItemIcon;
        }
    }

    /// <summary>
    /// function to init the progress bar:
    /// - delete old items
    /// </summary>
    /// <param name=""></param>
    public void init()
    {

        int _maxItemCount = ResultManager.Instance.currentResult.Length;

        currentItems = new List<ProgressBarItem>();
        currentItemStates = new List<EProgressBarItemState>();

        for (int i = 0; i < _maxItemCount; i++)
        {
            GameObject obj = Utils.Spawn(itemPrefab, rootStart);
            ProgressBarItem item = obj.GetComponent<ProgressBarItem>();
            item.name = string.Format("{0:00}_Item", i);

            item.setIcon(EmptyItemIcon);

            currentItems.Add(item);
            currentItemStates.Add(EProgressBarItemState.Empty);
        }
        for (int i = 0; i < ResultManager.Instance.currentResult.Length; i++)
        {
            QuizProgressBar.Instance.SetItemState(i, ResultManager.Instance.currentResult[i]);
        }
    }

    /// <summary>
    /// Get progress bar item state at index X
    /// </summary>
    /// <param name="paramIndex">item index</param>
    /// <returns></returns>
    public EProgressBarItemState getItemState(int paramIndex)
    {
        if (paramIndex < 0)
            return EProgressBarItemState.Incorrect;
        if (paramIndex == 0)
            return EProgressBarItemState.FullCorrect;
        if (currentItemStates == null || paramIndex > currentItemStates.Count)
            return EProgressBarItemState.Incorrect;
        return currentItemStates [paramIndex - 1];
    }

    public void SetItemState(int paramIndex, EProgressBarItemState newState)
    {
        if (paramIndex <= 0)
            return;
        if (paramIndex < currentItems.Count && paramIndex < currentItemStates.Count)
        {
            currentItems [paramIndex - 1].setIcon(getIconFromState(newState));
            currentItemStates [paramIndex - 1] = newState;
        }
    }
    public void updateItemState(int paramIndex, bool paramCorrect)
    {
        if (paramIndex < 0)
            return;

        if (paramIndex < currentItems.Count && paramIndex < currentItemStates.Count)
        {
            EProgressBarItemState state = currentItemStates[paramIndex];
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

            currentItemStates[paramIndex] = newState;
            currentItems[paramIndex].setIcon(getIconFromState(newState));
        }
    }
    #endregion

    #region MonoBehavior

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else
            Instance = this;
    }
    #endregion
}

public enum EProgressBarItemState
{
    Empty,
    Incorrect,
    HalfCorrect,
    FullCorrect,
}
