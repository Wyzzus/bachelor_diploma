using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContainer : MonoBehaviour
{
    public DndObjectUI UIPart;
    public PlayerController Data;
    public EntityComponent Component;

    public void Setup(PlayerController Data, EntityComponent Component)
    {
        this.Data = Data;
        this.Component = Component;
    }

    public void Update()
    {
        if(Data && Data.Data != null)
        {
            UIPart.Name.text = Data.Data.Name;
            UIPart.Descripion.text = Data.Data.Dice;
        }
    }

    public void Show()
    {
        GMInteractionComponent gmic = (GMInteractionComponent)Component;
        gmic.Show(Data);
    }
}
