using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : DndEditor
{
    public Effect CurrentEditObject;
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
        Effect converted = objectToEdit as Effect;
        CurrentEditObject = converted;

        if(AttributesNotChangedIn(CurrentEditObject.Attributes))
            CurrentAttributes = CloneAttributes(CurrentEditObject.Attributes);
        else
            CurrentAttributes = CloneAttributes(PackConstructor.instance.CurrentThemePack.Attributes);

        AttributesHandler.Update(CurrentAttributes);
    }

    public void Save()
    {
        CurrentEditObject.Attributes = CloneAttributes(CurrentAttributes);
        List<Effect> Effects = PackConstructor.instance.CurrentThemePack.Effects;
        if (!Effects.Contains(CurrentEditObject))
            Effects.Add(CurrentEditObject);
        SaveBaseInfo(CurrentEditObject);
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new Effect();
        base.ClearEditor();
    }
}
