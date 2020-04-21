using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContainer : GameDataContainer
{
    public void DeEquip()
    {
        InventoryComponent inv = (InventoryComponent)Component;
        inv.DeEquip(DataID);
    }
}
