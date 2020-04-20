using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute : DndObject
{
    public float Value;

    public Attribute()
    {
        Value = 0;
    }

    public Attribute(Attribute old)
    {
        this.Name = old.Name;
        this.Description = old.Description;
        this.Value = old.Value;
    }
}
