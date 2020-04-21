using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesComponent : EntityComponent
{
    public ScrollViewHandler AttributesHandler;

    //public List<float> BaseValues = new List<float>();

    //public List<float> CurrentValues = new List<float>();

    public GameAttributeContainer[] gameAttributes;

    public PlayerData Data;

    public void SetupAttributes(PlayerData Data)
    {
        this.Data = Data;
        int i = 0;
        List<int> aIds = new List<int>();
        foreach(Attribute a in GameManager.instance.CurrentThemePack.Attributes)
        {
            Data.BaseAttributes.Add(0);
            Data.AdditionalAttributes.Add(0);
            aIds.Add(i); i++;
        }
        AttributesHandler.UpdateOnComponent(aIds, typeof(Attribute));
        gameAttributes = AttributesHandler.content.GetComponentsInChildren<GameAttributeContainer>();
    }

    public void FixedUpdate()
    {
        for(int i = 0; i < gameAttributes.Length; i++)
        {
            gameAttributes[i].ShowAttribute(Data.BaseAttributes[i] + Data.AdditionalAttributes[i]);
        }
    }

    public void CalculateAttributes()
    {
        ResetAttributes();
        foreach(int effectId in Data.Effects)
        {
            Effect effect = GameManager.instance.CurrentThemePack.Effects[effectId];

            for (int i = 0; i < Data.AdditionalAttributes.Count; i++)
            {
                Data.AdditionalAttributes[i] += effect.Attributes[i].Value;
            }
        }

        foreach (int itemId in Data.Equipment)
        {
            Equipment item = GameManager.instance.CurrentThemePack.InventoryItems[itemId];

            for (int i = 0; i < Data.AdditionalAttributes.Count; i++)
            {
                Data.AdditionalAttributes[i] += item.Attributes[i].Value;
            }
        }
    }

    public void ResetAttributes()
    {
        for(int i = 0; i < Data.AdditionalAttributes.Count; i++)
        {
            Data.AdditionalAttributes[i] = 0;
        }
    }
}
