using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var text in GetComponentsInChildren<Text>())
        {
            text.text = "";
        } 
    }

    // Update is called once per frame
    public void SetTexts()
    {
        
    }
}
