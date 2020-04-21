using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttributeContainer : GameDataContainer
{
    public Text ValueDisplay;

    public void ShowAttribute(float value)
    {
        ValueDisplay.text = value.ToString();
    }
}
