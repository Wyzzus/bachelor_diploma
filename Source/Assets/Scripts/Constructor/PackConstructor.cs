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

    [Header("Editors")]
    public ObjectEditor PlaceableEditor;
    public LocationEditor LocationsEditor;
    public AvatarEditor AvatarEditor;

    [Header ("Current Pack")]
    public ThemePack CurrentThemePack = new ThemePack();
    
    public void UpdateView()
    {
        PlaceableHandler.Update(CurrentThemePack.Objects, PlaceableEditor);
        LocationsHandler.Update(CurrentThemePack.Locations, LocationsEditor);
        AvatarsHandler.Update(CurrentThemePack.Avatars, AvatarEditor);
    }

    public void Awake()
    {
        instance = this;
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
        UpdateView();
        NameField.text = CurrentThemePack.Name;
        DescriptionField.text = CurrentThemePack.Description;
        Debug.Log(Message);
    }
}
