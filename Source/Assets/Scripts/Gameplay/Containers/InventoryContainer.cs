using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : GameDataContainer
{
    public void Show()
    {
        Component.Display(DataID);
    }
}
