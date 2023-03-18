using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The model class that the gallery uses to store raw data.
/// </summary>
[System.Serializable]
public class GalleryElementModelClass
{
    public int index;
    public Sprite thumbnailSprite;
    public Sprite fullscreenSprite;

    public Texture texture;
}
