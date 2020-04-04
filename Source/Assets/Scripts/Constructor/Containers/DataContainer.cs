using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataContainer : MonoBehaviour
{
    public Text Name;
    public Text Description;

    public DndObject Data;
    public DndEditor Editor;

    public void Setup(DndObject dndObject, DndEditor editor = null)
    {
        if(dndObject != null)
        {
            Data = dndObject;
            Name.text = dndObject.Name;
            Description.text = dndObject.Description;
        }

        if (editor != null)
            Editor = editor;
    }

    public virtual void Remove()
    {
        Destroy(gameObject);
    }
}
