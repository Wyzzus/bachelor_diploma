using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkComponent : MonoBehaviour
{
    public MapComponent Map;
    public MarkData Data;
    public Text MarkName;
    public Text Input;

    public void DestroyMark(GameObject obj)
    {
        
        Map = GetComponentInParent<MapComponent>();
        Map.DestroyMark(obj);
    }

    public void SetupMark(MarkData data)
    {
        Data = data;
        SetupName();
    }

    public void SetupName()
    {
        Debug.Log(Input.text);
        Data.text = Input.text;
        MarkName.text = Data.text;
    }

    public void SetupID(int n)
    {
        Data.id = n;
    }
}
