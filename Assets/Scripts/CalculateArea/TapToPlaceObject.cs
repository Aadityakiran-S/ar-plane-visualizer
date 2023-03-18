using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceObject : MonoBehaviour
{
    #region Referances

    public ARRaycastManager aRRaycastManager;

    public GameObject placementIndicator;
    public GameObject objectToPlace;

    private Pose _placementPose;
    private bool _placementPoseIsValid;

    #endregion

    #region Singleton
    public static TapToPlaceObject Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Unity Functions

    private void Update()
    {
        Update_PlacementPose();
        Update_PlacementIndicator();
    }

    private void OnDisable()
    {
        placementIndicator.SetActive(false);
    }

    #endregion

    #region Public FUnctions

    public void PlaceObject()
    {
        if (_placementPoseIsValid)
        {
            GameObject currentlySpawnedObject =  (GameObject)Instantiate(objectToPlace, 
                _placementPose.position, _placementPose.rotation);

            GamePieceManipulator.Instance.AddToList(currentlySpawnedObject);
            GamePieceManipulator.Instance.UpdateGamePieceLineRender();
        }
    }

    #endregion

    #region Private Functions

    //Update the possible positions where the object could be placed. 
    void Update_PlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid)
        {
            _placementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    //Update the reticle that we're using as a marker to place the object
    void Update_PlacementIndicator()
    {
        if (_placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position,
                _placementPose.rotation);
        }
        else
            placementIndicator.SetActive(false);
    }

    #endregion
   
}

