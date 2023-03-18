using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnHover : MonoBehaviour, IHoverable
{
    #region References
    [SerializeField] Vector3 _expandedScale;
    [SerializeField] Vector3 _regularScale;

    private RectTransform _rect;
    #endregion

    #region Start and Update

    private void Awake()
    {
        _rect = this.GetComponent<RectTransform>();
    }

    #endregion

    #region Public Methods

    public void OnHover()
    {
        //throw new System.NotImplementedException();
    }

    public void OnHoverLost()
    {
        if (_rect == null)
            return;

        LeanTween.scale(_rect, _regularScale, 0.2f).setEaseInOutBounce();
    }

    public void OnHoverStart()
    {
        if (_rect == null)
            return;

        LeanTween.scale(_rect, _expandedScale, 0.2f).setEaseInOutBounce();
    }
    #endregion
}
