using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkComponent : MonoBehaviour
{
    public MarkerComponent Marker;
    public MarkData Data;
    public Text MarkName;
    public Text Input;

    public void DestroyMark(GameObject obj)
    {
        Marker.DestroyMark(obj);
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
