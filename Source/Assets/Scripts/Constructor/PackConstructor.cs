using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackConstructor : MonoBehaviour
{
    public static PackConstructor instance;
    public SaveLoadManager saveLoadManager;

    [Header("UI")]
    public InputField NameField;
    public InputField DescriptionField;

    [Header("Scroll View Handlers")]
    public ScrollViewHandler PlaceableHandler;
    public ScrollViewHandler LocationsHandler;
    public ScrollViewHandler AvatarsHandler;
    public ScrollViewHandler AttributesHandler;
    public ScrollViewHandler EffectsHandler;
    public ScrollViewHandler EquipmentHandler;
    public ScrollViewHandler EventHandler;

    [Header("Editors")]
    public ObjectEditor PlaceableEditor;
    public LocationEditor LocationsEditor;
    public AvatarEditor AvatarsEditor;
    public AttributeEditor AttributesEditor;
    public EffectEditor EffectsEditor;
    public EquipmentEditor EquipmentsEditor;
    public EventEditor EventsEditor;

    [Header ("Current Pack")]
    public ThemePack CurrentThemePack = new ThemePack();
    
    public void UpdateView()
    {
        PlaceableHandler.Update(CurrentThemePack.Objects, PlaceableEditor);
        LocationsHandler.Update(CurrentThemePack.Locations, LocationsEditor);
        AvatarsHandler.Update(CurrentThemePack.Avatars, AvatarsEditor);
        AttributesHandler.Update(CurrentThemePack.Attributes, AttributesEditor);
        EffectsHandler.Update(CurrentThemePack.Effects, EffectsEditor);
        EquipmentHandler.Update(CurrentThemePack.InventoryItems, EquipmentsEditor);
        EventHandler.Update(CurrentThemePack.Events, EventsEditor);
    }

    public void Awake()
    {
        instance = this;
        UpdateView();
    }

    public void Save()
    {
        CurrentThemePack.Name = NameField.text;
        CurrentThemePack.Description = DescriptionField.text;
        string Message;
        saveLoadManager.Save(CurrentThemePack, out Message);
        Debug.Log(Message);
    }

    public void Load()
    {
        string Message;
        CurrentThemePack = saveLoadManager.Load(out Message);
        if(CurrentThemePack != null)
        {
            UpdateView();
            NameField.text = CurrentThemePack.Name;
            DescriptionField.text = CurrentThemePack.Description;
        }
        Debug.Log(Message);
    }
}
