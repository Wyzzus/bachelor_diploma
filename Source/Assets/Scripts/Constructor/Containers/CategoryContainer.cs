using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryContainer : DataContainer
{
    public void Start()
    {
        Setup(Data);
    }

    public override void Remove()
    {
        PackConstructor.instance.CurrentThemePack.Categories.Remove((DndCategory)Data);
        ObjectEditor editor = (ObjectEditor)Editor;
        editor.RemoveCategory((DndCategory)Data);
        base.Remove();
    }
}
