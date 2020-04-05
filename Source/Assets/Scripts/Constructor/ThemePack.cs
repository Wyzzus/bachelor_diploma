using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThemePack
{
    [Header("Pack Data")]
    public string Name;
    public string Description;

    [Header ("Objects and Locations")]
    public List<Location> Locations = new List<Location>();
    public List<PlaceableObject> Objects = new List<PlaceableObject>();
    public List<DndCategory> Categories = new List<DndCategory>();

    [Header ("Player Objects")]
    public List<Avatar> Avatars = new List<Avatar>();
    public List<Equipment> InventoryItems = new List<Equipment>();
    public List<Effect> Effects = new List<Effect>();
    public List<Attribute> Attributes = new List<Attribute>();

    [Header ("Events")]
    public List<DndEvent> Events = new List<DndEvent>();
}
