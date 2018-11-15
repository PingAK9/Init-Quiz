using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultItem : MonoBehaviour
{
    #region Fields

    public static ResultItem Instance { get; private set; }

    public Sprite IncorrectStar;
    public Sprite HalfCorrectStar;
    public Sprite FullCorrectStar;

    public Image StarSprite;

    public GameObject CorrectGo;
    public GameObject IncorrectGo;
    public AudioClip win;
    public AudioClip lose;

    #endregion

    #region Mono Operations

    private void Awake()
    {
        if (Instance != null)
        {
            try
            {
                Destroy(Instance.gameObject);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.StackTrace);
            }
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    #endregion

    #region Operations

    public void show(EProgressBarItemState paramState)
    {
        CorrectGo.SetActive(false);
        IncorrectGo.SetActive(false);

        Sprite starIcon = IncorrectStar;

        if (paramState == EProgressBarItemState.FullCorrect)
        {
            CorrectGo.SetActive(true);
            starIcon = FullCorrectStar;
            GetComponent<AudioSource>().PlayOneShot(win);
        }
        else if (paramState == EProgressBarItemState.HalfCorrect)
        {
            CorrectGo.SetActive(true);
            starIcon = HalfCorrectStar;
            GetComponent<AudioSource>().PlayOneShot(win);
        }
        else
        {
            IncorrectGo.SetActive(true);
            starIcon = IncorrectStar;
            GetComponent<AudioSource>().PlayOneShot(lose);
        }

        StarSprite.sprite = starIcon;
    }

    public void Retry()
    {
        ResultManager.Instance.OnRetry();
    }

    public void Back()
    {
        ResultManager.Instance.OnBack();
    }

    public void Next()
    {
        ResultManager.Instance.OnNext();
    }

    #endregion
}