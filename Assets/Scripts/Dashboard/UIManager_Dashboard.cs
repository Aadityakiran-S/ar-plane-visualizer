using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_Dashboard : MonoBehaviour
{
    #region Referance

    public GameObject helpPanel;
    public Button dismissHelpButton;
    public GameObject helperDataManager;
    public Text helperText;

    private List<HelperDataModelClass> _helperData;

    #endregion

    #region Event subscription and Unsubscription
    private void OnEnable()
    {
        ScrollSnapRect.Instance.OnIndexChanged_Event += UpdateHelperText;
    }
    private void OnDisable()
    {
        ScrollSnapRect.Instance.OnIndexChanged_Event -= UpdateHelperText;
    }
    #endregion

    #region Unity Functions

    private void Awake()
    {
        StartCoroutine(WaitTillScrollSnapInitializes());
    }


    private void Start()
    {
        StoreHelperData(helperDataManager);

        helperText.text = _helperData[0].helpText;

        if (!PlayerPrefs.HasKey("showHelpPanelOnStart"))
        {
            OnClick_ToggleHelpPanel(true);

            Debug.Log("Showing user the help screen since it's the firsrt time");
        }

        Debug.Log("Is Player Pref Set with that key? " + PlayerPrefs.HasKey("showHelpPanelOnStart"));
    }

    #endregion

    #region Public Functions

    public void OnClick_ToggleHelpPanel(bool toShow)
    {
        if (toShow)
        {
            if (!PlayerPrefs.HasKey("showHelpPanelOnStart"))
            {
                dismissHelpButton.GetComponentInChildren<Text>().text = "Start";
                MakeVisible_DismissHelpButton(false);
            }
            else
            {
                dismissHelpButton.GetComponentInChildren<Text>().text = "Close Help";
                MakeVisible_DismissHelpButton(true);
            }
        }

        Debug.Log("HelperPanel Toggle function invoked, toShow is: " + toShow);

        helpPanel.SetActive(toShow);
    }

    public void OnClick_DismissHelpButton()
    {
        if (!PlayerPrefs.HasKey("showHelpPanelOnStart"))
            PlayerPrefs.SetInt("showHelpPanelOnStart", 1);

        OnClick_ToggleHelpPanel(false);
    }

    #region Scene Navigation Functions

    public void OnClick_EmbossButton()
    {
        SceneManager.LoadScene("EmbossTiles");
        Debug.Log("Emboss Button Clicked");
    }

    public void OnClick_CalculateAreaButton()
    {
        SceneManager.LoadScene("CalculateArea");
        Debug.Log("Calculate area Button Clicked");
    }

    #endregion

    #endregion

    #region Private Functions

    private void StoreHelperData(GameObject helperDataManager)
    {
        _helperData = new List<HelperDataModelClass>();

        foreach (var data in helperDataManager.GetComponent<HelperDataManager>().helperData)
        {
            _helperData.Add(data);
        }
    }

    private void UpdateHelperText(object sender, ScrollSnapRect.OnIndexChanged_EventArgs e)
    {
        string textToDisplay = _helperData[e.index].helpText;

        helperText.text = textToDisplay;

        if (e.index == _helperData.Count - 1)
            MakeVisible_DismissHelpButton(true);
    }

    private void MakeVisible_DismissHelpButton(bool makeVisible)
    {
        dismissHelpButton.gameObject.SetActive(makeVisible);
    }

    IEnumerator WaitTillScrollSnapInitializes()
    {
        yield return new WaitForFixedUpdate();
        OnClick_ToggleHelpPanel(false);
    }

    #endregion
}
