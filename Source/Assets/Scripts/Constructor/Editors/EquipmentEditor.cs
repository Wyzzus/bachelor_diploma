using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentEditor : DndEditor
{
    public Equipment CurrentEditObject;
    public List<Attribute> CurrentAttributes;
    public ScrollViewHandler AttributesHandler;
    
    public override void Start()
    {
        base.Start();
    }

    public void UpdateCurrentAttributes()
    {
        CurrentAttributes = CloneAttributes(PackConstructor.instance.CurrentThemePack.Attributes);
        AttributesHandler.Update(CurrentAttributes);
    }

    public override void Edit<T>(T objectToEdit)
    {
        base.Edit(objectToEdit);
        ShowBaseInfo(objectToEdit);
        Equipment converted = objectToEdit as Equipment;
        CurrentEditObject = converted;

        imageHandler.ShowImage(converted.Image, CurrentImageSprite);
        CurrentImageBase64 = converted.Image;

        if (AttributesNotChangedIn(CurrentEditObject.Attributes))
            CurrentAttributes = CloneAttributes(CurrentEditObject.Attributes);
        else
            CurrentAttributes = CloneAttributes(PackConstructor.instance.CurrentThemePack.Attributes);

        AttributesHandler.Update(CurrentAttributes);
    }

    public void Save()
    {
        SaveImage(CurrentEditObject);
        CurrentEditObject.Attributes = CloneAttributes(CurrentAttributes);
        List<Equipment> Equipments = PackConstructor.instance.CurrentThemePack.InventoryItems;
        if (!Equipments.Contains(CurrentEditObject))
            Equipments.Add(CurrentEditObject);
        SaveBaseInfo(CurrentEditObject);
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new Equipment();
        base.ClearEditor();
    }
}
