using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnHover : MonoBehaviour, IHoverable
{
    #region References

    [SerializeField] Sprite _normalImage;
    [SerializeField] Sprite _hoverImage;
    [SerializeField] Sprite _clickedImage;
    [SerializeField] Image _targetGraphic;

    #endregion


    #region Interface Implementations

    public void OnHover()
    {
        //Remove this later
        //Debug.Log("Mouse is hovering now");
    }

    public void OnHoverLost()
    {
        _targetGraphic.sprite = _normalImage;
    }

    public void OnHoverStart()
    {
        _targetGraphic.sprite = _hoverImage;
    }

    #endregion

    #region Public Functions

    public void OnClick_PlayButton_ChangeSprite()
    {
        _targetGraphic.sprite = _clickedImage;
    }

    #endregion
}
