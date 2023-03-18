using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatePlayButton : MonoBehaviour
{
    #region Referances
    [SerializeField] RectTransform _buttonRect;

    [SerializeField] float _animationStartDelay;
    [SerializeField] float _scaleDuration;
    [SerializeField] Vector3 _expandedSize;
    [SerializeField] int _animationLoopCount;

    #endregion

    #region Start and Update
    private void Start()
    {
        StartCoroutine(PulsateButton());
    }

    #endregion

    #region Private Functions
    private IEnumerator PulsateButton()
    {
        yield return new WaitForSeconds(_animationStartDelay);

        //Pulsate the button
        LeanTween.scale(_buttonRect, _expandedSize, _scaleDuration).setEaseInExpo().setLoopPingPong(_animationLoopCount)
            .setOnComplete(
            () => LeanTween.scale(_buttonRect, Vector3.one, _scaleDuration)
            );
    }    
    #endregion
}
