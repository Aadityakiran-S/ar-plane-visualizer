using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that is attached to each thumbnail element in the gallery. Manages the individual identity of 
/// each element in the Gallery.
/// </summary>
public class ThumbnailElement : MonoBehaviour
{
    #region Referances
    public Image elementImage;
    public int elementIndex;
    public Sprite elementThumbnail_Sprite;
    public Sprite elementFullscreen_Sprite;
    #endregion

    #region Event Subscription and Unsubscription
    private void OnEnable()
    {
        //GalleryManager.Instance.ChooseImageEvent += HighlightSelf;
    }

    private void OnDisable()
    {
        GalleryManager.Instance.ChooseImageEvent -= HighlightSelf;
    }
    #endregion

    #region Start and Update

    private void Start()
    {
        GalleryManager.Instance.ChooseImageEvent += HighlightSelf;
    }

    #endregion

    /// <summary>
    /// Sets the raw data to the element, such as sprites and index
    /// </summary>
    /// <param name="obj">Object of type GalleryElementModelClass</param>
    public void SetElementValues(GalleryElementModelClass obj)
    {
        elementImage.sprite = obj.thumbnailSprite;

        elementIndex = obj.index;
        elementThumbnail_Sprite = obj.thumbnailSprite;
        elementFullscreen_Sprite = obj.fullscreenSprite;
    }

    /// <summary>
    /// Behaviour when the element is tapped
    /// </summary>
    public void OnClick_Element()
    {
        GalleryManager.Instance.ShowEnlargedImage(elementIndex);
    }

    private void HighlightSelf(object sender, GalleryManager.ChooseImageEventArgs e)
    {
        Color color = this.gameObject.GetComponent<Image>().color;

        if (e.chosenIndex == elementIndex)
        {
            Color newColor = new Color(color.r, color.g, color.b, 0.2f);
            this.gameObject.GetComponent<Image>().color = newColor;

            Debug.Log("Highlight self called for thumbnail with index " + elementIndex);
        }
        else
        {
            Color newColor = new Color(color.r, color.g, color.b, 1f);
            this.gameObject.GetComponent<Image>().color = newColor;
        }
    }
}
