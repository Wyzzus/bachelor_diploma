using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Skin;
    public string Dice;

    public List<float> AttributesValues;
    public List<int> Effects;

    public List<int> Inventory;
    public List<int> Equipment;

    public List<Marker> Markers;
}

[System.Serializable]
public class Marker
{
    public string Name;
    public Vector3Ser Position;
}
