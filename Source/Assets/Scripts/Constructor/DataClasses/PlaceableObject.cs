using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceableObject : DndObject, IDisplayable
{
    public string Image { get; set; }
    public int CategoryId;
    public Vector3Ser Position;

    public PlaceableObject()
    {

    }

    public PlaceableObject(PlaceableObject obj)
    {
        this.Name = obj.Name;
        this.Description = obj.Description;
        this.Image = obj.Image;
        this.CategoryId = obj.CategoryId;
    }

    public void SetPosition(Vector3 position)
    {
        Position = new Vector3Ser(position);
    }

    public Vector3 GetPosition()
    {
        return Position.ToVector3();
    }

    public Vector2 GetPosition2D()
    {
        Vector3 pos = Position.ToVector3();
        return new Vector2(pos.x, pos.z);
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
