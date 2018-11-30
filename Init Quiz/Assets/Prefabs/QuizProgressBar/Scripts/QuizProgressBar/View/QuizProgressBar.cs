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
    private void Awake()
    {
        Instance = this;
        init();
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    List<ProgressBarItem> currentItems;
    #endregion


    #region Operations


    Sprite getIconFromState(int _index)
    {
        EProgressBarItemState paramState = ResultManager.Instance.currentResult[_index];
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

    public void init()
    {

        int _maxItemCount = ResultManager.Instance.currentResult.Length;

        currentItems = new List<ProgressBarItem>();

        for (int i = 0; i < _maxItemCount; i++)
        {
            GameObject obj = Utils.Spawn(itemPrefab, rootStart);
            ProgressBarItem item = obj.GetComponent<ProgressBarItem>();
            item.name = string.Format("{0:00}_Item", i);

            item.setIcon(EmptyItemIcon);
            currentItems.Add(item);
        }
        for (int i = 0; i < ResultManager.Instance.currentResult.Length; i++)
        {
            QuizProgressBar.Instance.SetItemState(i);
        }
        if (ResultManager.Instance.currentSceneIndex >= 0)
        {
            currentItems[ResultManager.Instance.currentSceneIndex].startRotation();
        }
    }


    public void SetItemState(int paramIndex)
    {
        if (paramIndex >= 0 && paramIndex < currentItems.Count)
        {
            currentItems[paramIndex].setIcon(getIconFromState(paramIndex));
        }
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
