using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceableObject : DndObject, IDisplayable
{
    public string Image { get; set; }
    public int CategoryId;
    public Vector3Ser Position;

    public void SetPosition(Vector3 position)
    {
        Position = new Vector3Ser(position);
    }

    public Vector3 GetPosition()
    {
        return Position.ToVector3();
    }

    public Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public Texture2D GetTexture()
    {
        throw new System.NotImplementedException();
    }
}
