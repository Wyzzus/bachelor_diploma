using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataInteractionContainer : MonoBehaviour
{
    public DndObjectUI UIPart;
    public bool Equipable;
    public EntityComponent Component;
    public int Data;
    public System.Type DataType;

    public virtual void Setup(int itemId, System.Type ObjectType, EntityComponent Component, bool Equipable = false)
    {
        this.Equipable = Equipable;
        this.Component = Component;
        this.Data = itemId;
        this.DataType = ObjectType;
        switch (DataType.Name)
        {
            case nameof(Effect):
                UIPart.Name.text = GameManager.instance.CurrentThemePack.Effects[itemId].Name;
                break;
            case nameof(Equipment):
                UIPart.Name.text = GameManager.instance.CurrentThemePack.InventoryItems[itemId].Name;
                break;
        }
    }

    public void Remove()
    {
        GMInteractionComponent gmic = (GMInteractionComponent)Component;
        switch (DataType.Name)
        {
            case nameof(Effect):
                gmic.RemoveEffect(Data);
                break;
            case nameof(Equipment):
                if (Equipable)
                    gmic.RemoveEquipment(Data);
                else
                    gmic.RemoveItem(Data);
                break;
        }
        Destroy(gameObject);
    }
}
