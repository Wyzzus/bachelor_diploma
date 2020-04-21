using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDataContainer : MonoBehaviour
{
    public int DataID;
    public DndObjectUI UIPart;
    public EntityComponent Component;

    public void Setup(int ID, System.Type type, EntityComponent entityComponent)
    {
        DataID = ID;
        Component = entityComponent;

        ThemePack pack = GameManager.instance.CurrentThemePack;

        switch (type.Name)
        {
            case nameof(PlaceableObject):
                UIPart.ShowObject(pack.Objects[ID]);
                break;
            case nameof(Attribute):
                UIPart.ShowObject(pack.Attributes[ID]);
                break;
            case nameof(Effect):
                UIPart.ShowObject(pack.Effects[ID]);
                break;
            case nameof(Equipment):
                UIPart.ShowObject(pack.InventoryItems[ID]);
                break;
        }
    }

    public virtual void Action()
    {
        Component.Display(DataID);
    }
}
