using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBarItem : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Reference to item's UI2DSprite
    /// </summary>
    public Image IconSprite;
    public Animation anim;

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////

    #region Operations

    public void setIcon(Sprite paramIcon)
    {
        IconSprite.sprite = paramIcon;
    }

    public void startRotation()
    {
        anim.enabled = true;
    }

    public void stopRoration()
    {
        anim.enabled = false;
    }

    #endregion
}