using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationObjectContainer : DataContainer
{
    public Image image;
    public void Start()
    {
        Setup(Data);
        StartCoroutine(ShowImage());
    }

    public IEnumerator ShowImage()
    {
        ImageHandler im = new ImageHandler
        {
            MaxH = 50,
            MaxW = 50
        };
        LocationEditor editor = (LocationEditor)Editor;
        PlaceableObject obj = (PlaceableObject)Data;
        if (editor.CurrentPlaceableObject.Image != null)
        {
            im.ShowImage(obj.Image, image);
        }

        yield return null;
    }

    public void SetPosition(Vector2 uiPos)
    {
        PlaceableObject obj = (PlaceableObject)Data;
        obj.SetPosition(new Vector3(uiPos.x, 0, uiPos.y));
    }

    public void ConvertPosition(Vector2 uiPos)
    {

    }

    public override void Remove()
    {
        LocationEditor editor = (LocationEditor)Editor;
        PlaceableObject obj = (PlaceableObject)Data;
        editor.RemoveObjectFromMap(obj);
        base.Remove();
    }
}
