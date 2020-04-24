using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMEntity : Entity
{
    public Dropdown LocationsDropdown;

    public GMInteractionComponent GMInteraction;

    public void SetupLocationsDropDown()
    {
        SetupDropDown(GameManager.instance.CurrentThemePack.Locations, LocationsDropdown);
    }

    public void SetupDropDown<T>(List<T> objects, Dropdown dropdown) where T : DndObject
    {
        dropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (T obj in objects)
        {
            Dropdown.OptionData option = new Dropdown.OptionData
            {
                text = obj.Name
            };
            options.Add(option);
        }
        dropdown.AddOptions(options);
    }


}
