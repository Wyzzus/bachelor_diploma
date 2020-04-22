using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public int Skin;
    [SerializeField]
    public string Dice;

    [SerializeField]
    public List<float> BaseAttributes;
    [SerializeField]
    public List<float> AdditionalAttributes;

    [SerializeField]
    public List<int> Effects;
    [SerializeField]
    public List<int> Inventory;
    [SerializeField]
    public List<int> Equipment;

    [SerializeField]
    public List<Marker> Markers;
}

[System.Serializable]
public class Marker
{
    public string Name;
    public Vector3Ser Position;

    public Marker()
    {

    }
}
