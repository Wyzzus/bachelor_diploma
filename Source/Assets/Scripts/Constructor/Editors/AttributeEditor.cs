using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeEditor : DndEditor
{
    public Attribute CurrentEditObject;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Edit<T>(T objectToEdit)
    {
        base.Edit(objectToEdit);
        ShowBaseInfo(objectToEdit);
        Attribute converted = objectToEdit as Attribute;
        CurrentEditObject = converted;
    }

    public void Save()
    {
        List<Attribute> Attributes = PackConstructor.instance.CurrentThemePack.Attributes;
        if (!Attributes.Contains(CurrentEditObject))
            Attributes.Add(CurrentEditObject);
        SaveBaseInfo(CurrentEditObject);
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new Attribute();
        base.ClearEditor();
    }
}
