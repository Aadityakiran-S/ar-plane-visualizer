using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneBehaviour : MonoBehaviour
{
    #region Referances

    public ARPlane arPlane;

    #endregion

    #region Event Subscription & Unsubscription

    private void OnEnable() => arPlane.boundaryChanged += OnBoundaryChanged;

    private void OnDisable() => arPlane.boundaryChanged -= OnBoundaryChanged;

    #endregion

    void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
    {
        if (PlaneAreaManager.Instance.touchedPlaneName == this.gameObject.name)
        {
            Vector2[] boundaryPoints = obj.plane.boundary.ToArray();

            UIManager_CalculateArea.Instance.DisplayAreaOnText(
               PlaneAreaManager.CalculatePlaneArea(boundaryPoints).ToString("F1") + "m²");
        }
    }
}
