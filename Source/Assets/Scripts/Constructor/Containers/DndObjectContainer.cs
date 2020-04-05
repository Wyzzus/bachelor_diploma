using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DndObjectType
{
    Placeable,
    Location,

}

public class DndObjectContainer : DataContainer
{
    public DndObjectType MyType;
    public void Edit()
    {
        switch(MyType)
        {
            case DndObjectType.Placeable:
                StartEditor<ObjectEditor, PlaceableObject>();
                break;
            case DndObjectType.Location:
                StartEditor<LocationEditor, Location>();
                break;
        }
    }

    public void StartEditor<T,Y>() where Y : DndObject where T : DndEditor
    {
        T editor = (T)Editor;
        Y obj = (Y)Data;
        editor.Edit(obj);
        editor.gameObject.SetActive(true);
    }

    public override void Remove()
    {
        switch (MyType)
        {
            case DndObjectType.Placeable:
                PackConstructor.instance.CurrentThemePack.Objects.Remove((PlaceableObject)Data);
                break;
            case DndObjectType.Location:
                PackConstructor.instance.CurrentThemePack.Locations.Remove((Location)Data);
                break;
        }
        base.Remove();
    }
}
