using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Базовый класс компонента сущности
public class EntityComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Display(int ID)
    {

    }

    public string GetAttributesDesctiption(IAttributeInteractable obj)
    {
        string attributes = "";
        foreach (Attribute a in obj.Attributes)
        {
            if (a.Value != 0)
            {
                attributes += a.Name + ": ";

                if (a.Value > 0)
                    attributes += "<color=lime>+" + a.Value.ToString() + "</color>\n";

                if (a.Value < 0)
                    attributes += "<color=red>" + a.Value.ToString() + "</color>\n";
            }
        }
        return attributes;
    }
}
