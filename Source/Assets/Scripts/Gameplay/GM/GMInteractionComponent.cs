using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMInteractionComponent : EntityComponent
{
    public PlayerController CurrentPlayer;
    public GameObject PlayerInfoWindow;

    public ScrollViewHandler AttributesHandler;
    public ScrollViewHandler EffectsHandler;
    public ScrollViewHandler ItemsHandler;
    public ScrollViewHandler GearHandler;

    public Image SkinImage;
    public ImageHandler ImageHandler;

    public Dropdown EffectDropdown;
    public Dropdown ItemsDropdown;
    public Dropdown EquipmentDropdown;

    public Text PlayerName;
    public Text SkinName;

    public GameObject EventWindow;
    public DndObjectUI EventUIPart;
    public AttributeEditContainer[] AttributeViews;

    #region Interaction

    public void AddItem(int id)
    {
        CurrentPlayer.ClientAddItem(CurrentPlayer.Data.PlayerId, id);
    }

    public void RemoveItem(int id)
    {
        CurrentPlayer.ClientRemoveItem(CurrentPlayer.Data.PlayerId, id);
    }

    public void AddEquipment(int id)
    {
        CurrentPlayer.ClientAddEquipment(CurrentPlayer.Data.PlayerId, id);
    }

    public void RemoveEquipment(int id)
    {
        CurrentPlayer.ClientRemoveEquipment(CurrentPlayer.Data.PlayerId, id);
    }

    public void AddEffect(int id)
    {
        CurrentPlayer.ClientAddEffect(CurrentPlayer.Data.PlayerId, id);
    }

    public void RemoveEffect(int id)
    {
        CurrentPlayer.ClientRemoveEffect(CurrentPlayer.Data.PlayerId, id);
    }

    public void SetAttribute(int id, float value)
    {
        CurrentPlayer.ClientSetAttribute(CurrentPlayer.Data.PlayerId, value, id);
    }

    #endregion

    public void Show(PlayerController pc)
    {
        StopAllCoroutines();
        this.CurrentPlayer = pc;
        PlayerInfoWindow.SetActive(true);
        PlayerName.text = CurrentPlayer.Data.Name;
        ShowSkin();
        StartCoroutine(ShowInfo());
    }

    public void ShowSkin()
    {
        Avatar currentAvatar = GameManager.instance.CurrentThemePack.Avatars[CurrentPlayer.Data.Skin];
        ImageHandler.ShowImage(currentAvatar.Image, SkinImage);
        SkinName.text = currentAvatar.Name;
    }

    public IEnumerator ShowInfo()
    {
        if(AttributeViews.Length == 0)
        {
            FillAllDropDowns();
            List<int> aIds = new List<int>();
            int index = 0;
            foreach (float value in CurrentPlayer.Data.BaseAttributes)
            {
                aIds.Add(index); index++;
            }
            AttributesHandler.UpdateOnPlayerInfo(aIds, typeof(Attribute), this);
            AttributeViews = AttributesHandler.content.GetComponentsInChildren<AttributeEditContainer>();
        }
        EffectsHandler.UpdateOnPlayerInfo(CurrentPlayer.Data.Effects, typeof(Effect), this);
        Debug.Log("CurrentInvetorySize: " + CurrentPlayer.Data.Inventory.Count);
        ItemsHandler.UpdateOnPlayerInfo(CurrentPlayer.Data.Inventory, typeof(Equipment), this);
        GearHandler.UpdateOnPlayerInfo(CurrentPlayer.Data.Equipment, typeof(Equipment), this, true);
        yield return null;
    }

    public void FixedUpdate()
    {
        if(CurrentPlayer)
        {
            for (int i = 0; i < CurrentPlayer.Data.BaseAttributes.Count; i++)
            {
                if (AttributeViews.Length > i && AttributeViews[i])
                {
                    float value = CurrentPlayer.Data.BaseAttributes[i] + CurrentPlayer.Data.AdditionalAttributes[i];
                    AttributeViews[i].UIPart.Descripion.text = value.ToString();
                }
            }
        }
    }

    public void FillAllDropDowns()
    {
        FillDropDownWith(EffectDropdown, GameManager.instance.CurrentThemePack.Effects);
        FillDropDownWith(ItemsDropdown, GameManager.instance.CurrentThemePack.InventoryItems);
        FillDropDownWith(EquipmentDropdown, GameManager.instance.CurrentThemePack.InventoryItems);
    }

    public void AddEffectClick()
    {
        AddEffect(EffectDropdown.value);
        EffectsHandler.UpdateOnPlayerInfo(CurrentPlayer.Data.Effects, typeof(Effect), this);
        Debug.Log("Add effect " + EffectDropdown.value);
    }

    public void AddEquipmentClick()
    {
        AddEquipment(EquipmentDropdown.value);
        GearHandler.UpdateOnPlayerInfo(CurrentPlayer.Data.Equipment, typeof(Equipment), this, true);
        Debug.Log("Add effect " + EquipmentDropdown.value);
    }

    public void AdditemClick()
    {
        AddItem(ItemsDropdown.value);
        ItemsHandler.UpdateOnPlayerInfo(CurrentPlayer.Data.Inventory, typeof(Equipment), this);
        Debug.Log("Add effect " + ItemsDropdown.value);
    }

    public void FillDropDownWith<T>(Dropdown dd, List<T> Objects) where T : DndObject
    {
        dd.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (T a in Objects)
        {
            Dropdown.OptionData option = new Dropdown.OptionData
            {
                text = a.Name
            };
            options.Add(option);
        }
        dd.AddOptions(options);
    }

    public void GenerateEvent(int id, string PlayerName)
    {
        EventWindow.SetActive(true);
        DndEvent newEvent = GameManager.instance.CurrentThemePack.Events[id];
        EventUIPart.Name.text = newEvent.Name;
        string Description = "\n" + newEvent.Description.Replace("@player_name@", "<b>" + PlayerName + "</b>");
        Description += "\nНеобходимо выбросить больше <b>" + newEvent.MaxRoll.ToString() + "</b>!";
        EventUIPart.Descripion.text = Description;
    }
}
