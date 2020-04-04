using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect : DndObject, IAttributeInteractable
{
    public List<Attribute> Attributes { get; set; }

    public Effect()
    {
        Attributes = new List<Attribute>();
    }
}
