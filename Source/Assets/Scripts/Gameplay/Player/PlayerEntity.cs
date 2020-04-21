using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public EffectsComponent Effects;
    public InventoryComponent Inventory;
    public AttributesComponent Attributes;

    public override void Start()
    {
        base.Start();
    }

    public void SetupEffects(PlayerData Data)
    {
        Effects.SetupEffects(Data);
    }

    public void SetupInventory(PlayerData Data)
    {
        Inventory.SetupInventory(Data);
    }

    public void SetupAttributes(PlayerData Data)
    {
        Attributes.SetupAttributes(Data);
    }


}
