using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEditor : DndEditor
{
    public DndEvent CurrentEditObject;

    public InputField MaxRoll;
    public InputField MinTime;
    public InputField MaxTime;
    
    public override void Start()
    {
        base.Start();
    }

    public override void Edit<T>(T objectToEdit)
    {
        base.Edit(objectToEdit);
        ShowBaseInfo(objectToEdit);
        DndEvent converted = objectToEdit as DndEvent;
        CurrentEditObject = converted;

        MaxRoll.text = CurrentEditObject.MaxRoll.ToString();
        MinTime.text = CurrentEditObject.MinTime.ToString();
        MaxTime.text = CurrentEditObject.MaxTime.ToString();
    }

    public void Save()
    {
        List<DndEvent> Events = PackConstructor.instance.CurrentThemePack.Events;

        SaveEventData();

        if (!Events.Contains(CurrentEditObject))
            Events.Add(CurrentEditObject);
        SaveBaseInfo(CurrentEditObject);
    }

    public void SaveEventData()
    {
        int roll = 6;
        int.TryParse(MaxRoll.text, out roll);
        CurrentEditObject.MaxRoll = roll;

        float min = 120;
        float max = 180;

        float.TryParse(MinTime.text, out min);
        float.TryParse(MaxTime.text, out max);

        CurrentEditObject.MinTime = min;
        CurrentEditObject.MaxTime = max;
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new DndEvent();
        MaxRoll.text = "6";
        MinTime.text = "120";
        MaxTime.text = "180";
        base.ClearEditor();
    }
}
