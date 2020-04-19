using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class DndObjectContainer : DataContainer
{
    public void Edit()
    {
        switch(Data.GetType().Name)
        {
            case nameof(PlaceableObject):
                StartEditor<ObjectEditor, PlaceableObject>();
                break;
            case nameof(Location):
                StartEditor<LocationEditor, Location>();
                break;
            case nameof(Avatar):
                StartEditor<AvatarEditor, Avatar>();
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
        switch (Data.GetType().Name)
        {
            case nameof(PlaceableObject):
                PackConstructor.instance.CurrentThemePack.Objects.Remove((PlaceableObject)Data);
                break;
            case nameof(Location):
                PackConstructor.instance.CurrentThemePack.Locations.Remove((Location)Data);
                break;
            case nameof(Avatar):
                PackConstructor.instance.CurrentThemePack.Avatars.Remove((Avatar)Data);
                break;
        }
        base.Remove();
    }
}
