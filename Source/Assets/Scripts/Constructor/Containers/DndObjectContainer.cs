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
            case nameof(Attribute):
                StartEditor<AttributeEditor, Attribute>();
                break;
            case nameof(Effect):
                StartEditor<EffectEditor, Effect>();
                break;
            case nameof(Equipment):
                StartEditor<EquipmentEditor, Equipment>();
                break;
            case nameof(DndEvent):
                StartEditor<EventEditor, DndEvent>();
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
            case nameof(Attribute):
                PackConstructor.instance.CurrentThemePack.Attributes.Remove((Attribute)Data);
                break;
            case nameof(Effect):
                PackConstructor.instance.CurrentThemePack.Effects.Remove((Effect)Data);
                break;
            case nameof(Equipment):
                PackConstructor.instance.CurrentThemePack.InventoryItems.Remove((Equipment)Data);
                break;
            case nameof(DndEvent):
                PackConstructor.instance.CurrentThemePack.Events.Remove((DndEvent)Data);
                break;
        }
        base.Remove();
    }
}
