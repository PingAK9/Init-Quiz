using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishScene : MonoBehaviour
{
    public Sprite EmptyStar;
    public Sprite HalfStar;
    public Sprite FullStar;

    public GameObject star;
    public Transform grid1;
    public Transform grid2;

    public GameObject RefPanel;
    void Start()
    {
        RefPanel.SetActive(true);
        for (int i = 1; i < ResultManager.Instance.currentResult.Length; i++)
        {
            if (ResultManager.Instance.currentResult[i] == EProgressBarItemState.Incorrect || ResultManager.Instance.currentResult[i] == EProgressBarItemState.Empty)
            {
                GameObject starGO = Utils.Spawn(star, grid2);
                ProgressBarItem icon = starGO.GetComponent<ProgressBarItem>();
                icon.setIcon(EmptyStar);
            }
            else if (ResultManager.Instance.currentResult[i] == EProgressBarItemState.FullCorrect)
            {
                GameObject starGO = Utils.Spawn(star, grid1);
                ProgressBarItem icon = starGO.GetComponent<ProgressBarItem>();
                icon.setIcon(FullStar);
            }
            else
            {
                GameObject starGO = Utils.Spawn(star, grid1);
                ProgressBarItem icon = starGO.GetComponent<ProgressBarItem>();
                icon.setIcon(HalfStar);
            }
        }
    }

    public void OnFinish()
    {
        ResultManager.Instance.SaveData();
        SceneManager.LoadScene(SceneList.LoginScene);
    }

    void OnCloseRef()
    {
        RefPanel.SetActive(false);
    }
}
