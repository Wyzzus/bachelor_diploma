using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableObjectContainer : DataContainer
{
    public Color CurrentColor;
    public Color RegularColor;
    public void Start()
    {
        Setup(Data);
    }

    public void SetCurrent(bool flag)
    {
        Image myImage = GetComponent<Image>();
        if (flag)
        {
            LocationEditor editor = (LocationEditor)Editor;
            editor.CurrentPlaceableObject = (PlaceableObject)Data;
            foreach(PlaceableObjectContainer container in Editor.ScrollViewHandler.content.GetComponentsInChildren<PlaceableObjectContainer>())
            {
                container.SetCurrent(false);
            }
            myImage.color = CurrentColor;
        }
        else
        {
            myImage.color = RegularColor;
        }
    }
}
