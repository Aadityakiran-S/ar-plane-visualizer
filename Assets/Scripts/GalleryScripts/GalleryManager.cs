using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

/// <summary>
/// This class manages everything that is related to the functionality of the gallery as a whole
/// </summary>
public class GalleryManager : MonoBehaviour
{
    #region Referances

    public GameObject galleryDataManager;

    public GameObject thumbnailContent;
    public GameObject thumbnailElementPrefab;

    public GameObject fullscreenPanel;
    public Sprite fullscreenImage;

    public GameObject[] thumbnailElement;


    /*######### PRIVATE VARIABLES BELOW #################*/

    private List<GalleryElementModelClass> _galleryData;
    private int _elementCount;

    [SerializeField] private int _currentlySelectedImageIndex;
    [SerializeField] private int _chosenImageIndex;

    #endregion

    #region Event Declaration

    public event EventHandler<ChooseImageEventArgs> ChooseImageEvent;
    public class ChooseImageEventArgs : EventArgs
    {
        public int chosenIndex;
    }

    #endregion

    #region Event Subscription
    private void OnEnable()
    {
        //ScrollSnapRect.Instance.OnIndexChanged_Event += UpdateIndex;
    }
    private void OnDisable()
    {
        ScrollSnapRect.Instance.OnIndexChanged_Event -= UpdateIndex;
    }
    #endregion

    #region Singleton
    public static GalleryManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    private void Start()
    {
        ScrollSnapRect.Instance.OnIndexChanged_Event += UpdateIndex;
        _galleryData = new List<GalleryElementModelClass>();

        //Storing the gallery data locally in the class
        foreach (var obj in galleryDataManager.GetComponent<GalleryDataManager>().galleryData)
        {
            _galleryData.Add(obj);
        }

        //Initializing gallery count
        _elementCount = _galleryData.Count;

        //Spawning the thumbnails
        SpawnThumbnailElements(_galleryData);
    }


    /// <summary>
    /// Function that assigns the raw data to each gallery thumbnail. 
    /// </summary>
    /// <param name="spawnElements">List of elements to spawn</param>
    void SpawnThumbnailElements(List<GalleryElementModelClass> spawnElements)
    {
        for (int i = 0; i < _elementCount; i++)
        {
            thumbnailElement[i].GetComponent<ThumbnailElement>().SetElementValues(spawnElements[i]);
        }
    }

    /// <summary>
    /// Sets a new panel active to show the enlarged image on it.
    /// </summary>
    /// <param name="index">index of the image to be shown enlarged from _galleryData (in this case)</param>
    public void ShowEnlargedImage(int index)
    {
        UIManager_EmbossingTiles.Instance.ShowFullscreenPanel(true);

        _currentlySelectedImageIndex = index;

        ScrollSnapRect.Instance.MoveToSelectedPage(index);
    }

    /// <summary>
    /// Assigns the chosen image as the plane's default Texture
    /// </summary>
    public void ChooseImageBehaviour()
    {
        _chosenImageIndex = _currentlySelectedImageIndex;

        //Firing event
        ChooseImageEvent?.Invoke(this, new ChooseImageEventArgs{chosenIndex = _chosenImageIndex });

        //Assigning texture and changing button sprite
        Texture tex = _galleryData[_currentlySelectedImageIndex].texture;
        PlaneMeshManager.Instance.ChangePlaneTexture(tex);
        UIManager_EmbossingTiles.Instance.Change_SelectTileButton_Sprite(
            _galleryData[_currentlySelectedImageIndex].thumbnailSprite);

        //Closing Gallery
        UIManager_EmbossingTiles.Instance.ShowFullscreenPanel(false);
        UIManager_EmbossingTiles.Instance.ShowGalleryCanvas(false);
    }

    public void UpdateIndex(object sender, ScrollSnapRect.OnIndexChanged_EventArgs e)
    {
        Debug.Log("UpdateIndexEntered");

        _currentlySelectedImageIndex = e.index;

        Debug.Log("Update index called and current index updated to: " + _currentlySelectedImageIndex);
        Debug.Log("Passed in Value: " + e.index);
    }

    public void SetCurrentIndex(int index)
    {
        _currentlySelectedImageIndex = index;
    }
}
