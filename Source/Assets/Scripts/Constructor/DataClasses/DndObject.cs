using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DndObject
{
    public string Name;
    public string Description;

    public DndObject()
    {
        this.Name = "Object name";
        this.Description = "Object description";
    }

    public DndObject(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }

    public string GetHeader()
    {
        string header = "<b>" + Name + "</b>" + '\n'
                        + Description;
        return header;
    }
}

[System.Serializable]
public class Vector3Ser
{
    public float x;
    public float y;
    public float z;

    public Vector3Ser()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }

    public Vector3Ser(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Ser(Vector3 original)
    {
        this.x = original.x;
        this.y = original.y;
        this.z = original.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(this.x, this.y, this.z);
    }
}

[System.Serializable]
public class DndObjectUI
{
    public Text Name;
    public Text Descripion;
    public Image ImageView;

    public ImageHandler ImageHandler;

    public void ShowObject(DndObject dndObject)
    {
        if(Name) Name.text = dndObject.Name;
        if(Descripion) Descripion.text = dndObject.Description;
        if(ImageView) ImageHandler.ShowImage(((IDisplayable)dndObject).Image, ImageView);
    }
}

public interface IDisplayable
{
    string Image { get; set; }

    Sprite GetSprite();
    Texture2D GetTexture();
}

public interface IAttributeInteractable
{
    List<Attribute> Attributes { get; set; }
}