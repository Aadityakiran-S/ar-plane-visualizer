using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftHomeButtonPosition : MonoBehaviour
{
    #region Referances
    public List<SceneNameAndCorrespondingPosition> sceneNameAndPositions;

    [Header("Screen Buffer")]
    public float xBuffer;
    public float yBuffer;

    private RectTransform _rect;
    #endregion

    #region Start and Update

    private void Awake()
    {
        _rect = this.GetComponent<RectTransform>();
    }

    #endregion

    #region Event Subscriptions

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region Private Mehtods

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        //Resetting position on navigating back to home scene
        if (scene.name == "HomeScene")
            ChangeHomeButtonPos(default);

        List<SceneNameAndCorrespondingPosition> currentSceneNamePos = sceneNameAndPositions.Where(x => x.sceneName == scene.name)
            .ToList();

        //Return if the given scene doesn't need changing
        if (currentSceneNamePos.Count == 0)
            return;

        ChangeHomeButtonPos(currentSceneNamePos[0].homeButtonPos);
    }

    private void ChangeHomeButtonPos(HomeButtonPositions pos)
    {
        Vector2 newAnchorPos = Vector2.zero;
        Vector2 anchorMin = Vector2.zero;
        Vector2 anchorMax = Vector2.zero;
        switch (pos)
        {
            case HomeButtonPositions.TopLeft:
                anchorMin = new Vector2(0, 1);
                anchorMax = new Vector2(0, 1);
                newAnchorPos = new Vector2(0f + xBuffer, 0f - yBuffer);
                break;
            case HomeButtonPositions.TopRight:
                anchorMin = new Vector2(1, 1);
                anchorMax = new Vector2(1, 1);
                newAnchorPos = new Vector2(0f - xBuffer, 0f - yBuffer);
                break;
            case HomeButtonPositions.BottomLeft:
                anchorMin = new Vector2(0, 0);
                anchorMax = new Vector2(0, 0);
                newAnchorPos = new Vector2(0f + xBuffer, 0f + yBuffer);
                break;
            case HomeButtonPositions.BottomRight:
                anchorMin = new Vector2(1, 0);
                anchorMax = new Vector2(1, 0);
                newAnchorPos = new Vector2(0f - xBuffer, 0f + yBuffer);
                break;
            default:
                anchorMin = new Vector2(0, 1);
                anchorMax = new Vector2(0, 1);
                newAnchorPos = new Vector2(0f + xBuffer, 0f - yBuffer);
                break;
        }
        _rect.anchorMin = anchorMin;
        _rect.anchorMax = anchorMax;
        _rect.anchoredPosition = newAnchorPos;
    }

    #endregion

}

[System.Serializable]
public struct SceneNameAndCorrespondingPosition
{
    public string sceneName;
    public HomeButtonPositions homeButtonPos;
}

[System.Serializable]
public enum HomeButtonPositions
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}
