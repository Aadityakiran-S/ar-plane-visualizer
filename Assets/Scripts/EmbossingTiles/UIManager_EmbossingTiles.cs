using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_EmbossingTiles : MonoBehaviour
{
    #region Referances
    public GameObject galleryCanvas;
    public GameObject chooseImageButton;
    public GameObject fullscreenPanel;
    public GameObject addMarkerButton;
    public GameObject close_openButton;
    public GameObject selecTilesButton;
    public GameObject galleryBackButton;

    public TapToPlaceObject tapToPlaceObject;

    public Button showTilesButton;
    public Button manual_autoButton;
    public Button refreshButton;

    public Sprite closeSprite;
    public Sprite clearSprite;

    /*##################### PRIVATE REFERANCES BELOW #######################*/

    private bool _inManualMode = true;

    #endregion

    #region Singleton
    public static UIManager_EmbossingTiles Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        //Waiting for the ScrollSnapRect component to sync up
        StartCoroutine(WaitTillScrollSnapInitializes());
    }


    public void OnClick_RefreshButton()
    {
        //So that the onDisable function in it works
        tapToPlaceObject.enabled = false;

        //Reloading the scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        //Closing the gallery
        ShowGalleryCanvas(false);
    }


    public void OnClick_BackButton()
    {
        SceneManager.LoadScene("Dashboard");
    }

    public void OnClick_CloseAreaButton()
    {
        if (addMarkerButton.GetComponent<Button>().interactable)
        {
            addMarkerButton.GetComponent<Button>().interactable = false;
            //close_openButton.GetComponentInChildren<Text>().text = "Clear";
            close_openButton.GetComponent<Image>().sprite = clearSprite;

            manual_autoButton.interactable = false;

            //Add function call to create mesh
            PlaneMeshManager.Instance.CreateMeshOnMarkedArea(
                GamePieceManipulator.Instance.Return_PositionMarkerGameObjectList());

            //Closes the loop by connecting linerender
            GamePieceManipulator.Instance.CloseLoop();
        }
        else
        {
            addMarkerButton.GetComponent<Button>().interactable = true;
            manual_autoButton.interactable = true;
            //close_openButton.GetComponentInChildren<Text>().text = "Close";
            close_openButton.GetComponent<Image>().sprite = closeSprite;

            //Add function call to clear mesh
            PlaneMeshManager.Instance.ClearAllMeshData();

            //Clears all objects placed and associated data
            GamePieceManipulator.Instance.ClearAllObjects();

        }
    }

    public void OnClick_Manual_AutoButton()
    {
        if (_inManualMode)
        {
            showTilesButton.interactable = false;
            refreshButton.interactable = false;

            tapToPlaceObject.enabled = true;

            addMarkerButton.SetActive(true);
            close_openButton.SetActive(true);

            manual_autoButton.GetComponentInChildren<Text>().text = "Auto";
            manual_autoButton.interactable = false;

            PlaneMeshManager.Instance.ToggleARPlaneDetection(false);

            _inManualMode = false;
        }
        else
        {
            showTilesButton.interactable = true;
            refreshButton.interactable = true;

            tapToPlaceObject.enabled = false;

            addMarkerButton.SetActive(false);
            close_openButton.SetActive(false);

            manual_autoButton.GetComponentInChildren<Text>().text = "Manual";
            manual_autoButton.interactable = true;

            PlaneMeshManager.Instance.ToggleARPlaneDetection(true);

            _inManualMode = true;
        }
    }

    public void OnClick_AddMarkerButton()
    {
        manual_autoButton.interactable = false;

        TapToPlaceObject.Instance.PlaceObject();
    }

    public void Change_SelectTileButton_Sprite(Sprite spriteToChange)
    {
        selecTilesButton.GetComponent<Image>().sprite = spriteToChange;
    }

    public void ShowGalleryCanvas(bool toShow)
    {
        galleryCanvas.SetActive(toShow);
    }

    public void ShowFullscreenPanel(bool toSetActive)
    {
        fullscreenPanel.SetActive(toSetActive);
        galleryBackButton.SetActive(!toSetActive);
    }

    public void OnClick_ChooseImage()
    {
        GalleryManager.Instance.ChooseImageBehaviour();
    }

    IEnumerator WaitTillScrollSnapInitializes()
    {
        yield return new WaitForFixedUpdate();
        fullscreenPanel.SetActive(false);
    }
}
