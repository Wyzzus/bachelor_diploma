using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Avatar : DndObject, IDisplayable
{
    public string Image { get; set; }

    public Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public Texture2D GetTexture()
    {
        throw new System.NotImplementedException();
    }
}
