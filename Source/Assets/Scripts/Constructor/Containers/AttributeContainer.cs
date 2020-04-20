using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeContainer : DataContainer
{
    public InputField AttributeValue;
    
    public void Start()
    {
        ShowValue();
    }

    public void ShowValue()
    {
        Attribute currentAttribute = (Attribute)Data;
        AttributeValue.text = currentAttribute.Value.ToString();
    }

    public void SetValue()
    {
        Attribute currentAttribute = (Attribute)Data;
        float value = 0;
        float.TryParse(AttributeValue.text, out value);
        currentAttribute.Value = value;
    }
}
