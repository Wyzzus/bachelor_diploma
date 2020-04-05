using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : DndObject, IDisplayable, IAttributeInteractable
{
    public List<Attribute> Attributes { get; set; }
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
