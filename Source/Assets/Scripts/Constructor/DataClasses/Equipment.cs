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
        Texture2D tex = GetTexture();
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }

    public Texture2D GetTexture()
    {
        byte[] imageBytes = System.Convert.FromBase64String(Image);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        return tex;
    }

}
