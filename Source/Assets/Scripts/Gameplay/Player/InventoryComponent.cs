using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : EntityComponent
{
    public ScrollViewHandler InventoryHandler;
    public ScrollViewHandler EquipmentHandler;

    public GameObject ItemInfoPanel;

    public DndObjectUI UIPart;

    public PlayerData Data;
    public int DisplayedItemId = -1;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void SetupInventory(PlayerData Data)
    {
        this.Data = Data;
        UpdateView(Data);
    }

    public void UpdateView(PlayerData Data)
    {
        InventoryHandler.UpdateOnComponent(Data.Inventory, typeof(Equipment), this);
        EquipmentHandler.UpdateOnComponent(Data.Equipment, typeof(Equipment), this);
        ItemInfoPanel.SetActive(Data.Inventory.Count > 0 ? true : false);
        if (Data.Inventory.Count > 0)
        {
            DisplayedItemId = Data.Inventory[0];
            Display(DisplayedItemId);
        }
    }

    public override void Display(int ID)
    {
        Equipment item = GameManager.instance.CurrentThemePack.InventoryItems[ID];
        UIPart.ShowObject(item);

        string Description = "<b>Описание</b>";
        Description += "\n" + item.Description + "\n";
        string attributes = GetAttributesDesctiption(item);
        if(attributes != "")
        {
            Description += "<b>Виляние на атрибуты:</b>\n";
            Description += attributes;
        }

        UIPart.Descripion.text = Description;

        DisplayedItemId = ID;
    }

    public void Equip()
    {
        if(DisplayedItemId >= 0)
        {
            Data.Equipment.Add(DisplayedItemId);
            Data.Inventory.Remove(DisplayedItemId);
        }
        UpdateView(Data);
    }

    public void DeEquip(int ID)
    {
        if (ID >= 0)
        {
            Data.Equipment.Remove(ID);
            Data.Inventory.Add(ID);
        }
        UpdateView(Data);
    }

    public void Remove()
    {
        if (DisplayedItemId >= 0)
        {
            Data.Inventory.Remove(DisplayedItemId);
        }
        UpdateView(Data);
    }
}
