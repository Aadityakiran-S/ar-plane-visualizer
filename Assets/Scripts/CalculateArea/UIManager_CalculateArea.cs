using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class UIManager_CalculateArea : MonoBehaviour
{
    #region Referances
    public GameObject autoButton;
    public GameObject planeManager;
    public GameObject interactionManager;
    public GameObject addMarkerButton;
    public GameObject manualButton;
    public GameObject closeButton;
    public GameObject aRSessionOrigin;

    public Text areaText;

    public Sprite closeSprite;
    public Sprite clearSprite;
    #endregion

    #region Singleton
    public static UIManager_CalculateArea Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Public Functions

    public void OnClick_RefreshButton()
    {
        //So that the onDisable function in it works
        interactionManager.SetActive(false);

        //Reloading the scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OnClick_BackButton()
    {
        //So that the onDisable function in it works
        interactionManager.SetActive(false);

        SceneManager.LoadScene("Dashboard");
    }

    public void DisplayAreaOnText(string textToDisplay)
    {
        areaText.text = textToDisplay;
    }

    public void OnClick_ManualButton()
    {
        autoButton.SetActive(true);
        planeManager.SetActive(false);
        interactionManager.SetActive(true);

        areaText.text = "Tap + to place Object";

        ToggleARPlaneDetection(false);

        addMarkerButton.SetActive(true);
        closeButton.SetActive(true);
        manualButton.SetActive(false);
    }

    public void OnClick_AddObjectButton()
    {
        TapToPlaceObject.Instance.PlaceObject();
    }

    public void OnClick_AutoButton()
    {
        if (addMarkerButton.GetComponent<Button>().interactable)
        {
            autoButton.SetActive(false);

            planeManager.SetActive(true);
            interactionManager.SetActive(false);

            areaText.text = "Tap On Plane";

            GamePieceManipulator.Instance.ClearAllObjects();
            ToggleARPlaneDetection(true);

            addMarkerButton.SetActive(false);
            closeButton.SetActive(false);
            manualButton.SetActive(true);
        }
    }

    public void OnClick_CloseAreaButton()
    {
        if (addMarkerButton.GetComponent<Button>().interactable)
        {
            addMarkerButton.GetComponent<Button>().interactable = false;
            GamePieceManipulator.Instance.CloseLoop();

            autoButton.GetComponent<Button>().interactable = false;

            //closeButton.GetComponentInChildren<Text>().text = "Clear";
            closeButton.GetComponent<Image>().sprite = clearSprite;

            DisplayAreaOnText(PlaneAreaManager.CalculatePlaneArea(
                GamePieceManipulator.Instance.Return_PositionMarkerPositionArray())
                .ToString("F1") + "m²");
        }
        else
        {
            GamePieceManipulator.Instance.ClearAllObjects();
            addMarkerButton.GetComponent<Button>().interactable = true;

            autoButton.GetComponent<Button>().interactable = true;

            //closeButton.GetComponentInChildren<Text>().text = "Close";
            closeButton.GetComponent<Image>().sprite = closeSprite;

            areaText.text = "Tap + to place Object";
        }
    }

    public void ToggleARPlaneDetection(bool turnOn)
    {
        var planeManager = aRSessionOrigin.GetComponent<ARPlaneManager>();

        planeManager.SetTrackablesActive(turnOn);
        //planeManager.enabled = turnOn;
        planeManager.planePrefab.SetActive(turnOn);
    }

    #endregion
}
