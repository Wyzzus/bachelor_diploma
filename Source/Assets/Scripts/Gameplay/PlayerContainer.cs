using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContainer : MonoBehaviour
{
    public PlayerData playerData;
    public Text PlayerName;
    public Text RollCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerName.text = playerData.Name;
        RollCount.text = playerData.RollCount;
    }

    public void Setup(PlayerData data)
    {
        this.playerData = data;
    }
}
