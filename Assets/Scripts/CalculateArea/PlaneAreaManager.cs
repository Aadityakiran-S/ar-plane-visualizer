using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class PlaneAreaManager : MonoBehaviour
{
    #region Referances

    public string touchedPlaneName;

    #endregion

    #region Singleton

    public static PlaneAreaManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Unity Functions

    //Touching the plane to show overall area on screen
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(raycast, out RaycastHit raycastHit))
                    {
                        ARPlaneBehaviour planeAreaBehaviour = raycastHit.collider.gameObject.
                            GetComponent<ARPlaneBehaviour>();

                        ARPlane arPlane = raycastHit.collider.gameObject.
                            GetComponent<ARPlane>();

                        /*MARK*/ //Come back and change this name comparison thing
                        if (planeAreaBehaviour != null)
                        {
                            touchedPlaneName = planeAreaBehaviour.gameObject.name;
                        }

                        if (arPlane != null)
                        {
                            Vector2[] boundaryPoints = arPlane.boundary.ToArray();

                            UIManager_CalculateArea.Instance.DisplayAreaOnText(CalculatePlaneArea(
                                   boundaryPoints).ToString("F1") + "m²");
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        touchedPlaneName = null;
    }

    #endregion

    #region Public Functions

    public static float CalculatePlaneArea(Vector2[] boundaryPoints)
    {
        float temp = 0;

        if(boundaryPoints.Length != 0)
        {
            for (int i = 0; i < boundaryPoints.Length; i++)
            {
                if (i != boundaryPoints.Length - 1)
                {
                    float mulA = boundaryPoints[i].x * boundaryPoints[i + 1].y;
                    float mulB = boundaryPoints[i + 1].x * boundaryPoints[i].y;
                    temp += (mulA - mulB);
                }
                else
                {
                    float mulA = boundaryPoints[i].x * boundaryPoints[0].y;
                    float mulB = boundaryPoints[0].x * boundaryPoints[i].y;
                    temp += (mulA - mulB);
                }
            }

        }

        temp *= 0.5f;
        return Mathf.Abs(temp);
    }

    #endregion

}
