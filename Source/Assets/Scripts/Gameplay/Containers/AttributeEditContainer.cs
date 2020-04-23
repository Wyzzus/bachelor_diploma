using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeEditContainer : DataInteractionContainer
{
    public InputField BaseValueEditor;
    GMInteractionComponent gmic;
    public override void Setup(int itemId, Type ObjectType, EntityComponent Component, bool Equipable = false)
    {
        base.Setup(itemId, ObjectType, Component, Equipable);
        UIPart.Name.text = GameManager.instance.CurrentThemePack.Attributes[itemId].Name;
        gmic = (GMInteractionComponent)Component;
        BaseValueEditor.text = gmic.CurrentPlayer.Data.BaseAttributes[Data].ToString();
    }

    public void SetAttribute(string value)
    {
        gmic.SetAttribute(Data, float.Parse(value));
        BaseValueEditor.text = gmic.CurrentPlayer.Data.BaseAttributes[Data].ToString();
    }

    public void AddValue(float value)
    {
        gmic.SetAttribute(Data, gmic.CurrentPlayer.Data.BaseAttributes[Data] + value);
        BaseValueEditor.text = gmic.CurrentPlayer.Data.BaseAttributes[Data].ToString();
    }
}
