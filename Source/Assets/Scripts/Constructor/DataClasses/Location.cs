using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Location : MonoBehaviour, IDisplayable
{
    public string Image { get; set; }
    public List<PlaceableObject> PlacedObjects = new List<PlaceableObject>();

    public Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public Texture2D GetTexture()
    {
        throw new System.NotImplementedException();
    }
}
