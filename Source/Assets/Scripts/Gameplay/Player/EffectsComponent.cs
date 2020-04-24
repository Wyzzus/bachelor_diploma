using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsComponent : EntityComponent
{
    public ScrollViewHandler EffectsHandler;
    public List<Effect> CurrentEffects;

    public DndObjectUI UIPart;

    public PlayerData Data;

    public override void Start()
    {
        base.Start();
    }

    public void SetupEffects(PlayerData Data)
    {
        this.Data = Data;
        UpdateView(Data.Effects);
    }

    public void UpdateView(List<int> effectsIds)
    {
        EffectsHandler.UpdateOnComponent(effectsIds, typeof(Effect), this);
        if (effectsIds.Count > 0)
            Display(effectsIds[0]);
    }

    public void Add(int id)
    {
        Data.Effects.Add(id);
        UpdateView(Data.Effects);
    }

    public void Remove(int id)
    {
        Data.Effects.Remove(id);
        UpdateView(Data.Effects);
    }

    public override void Display(int ID)
    {
        Effect effect = GameManager.instance.CurrentThemePack.Effects[ID];
        string Description = "<b>" + effect.Name + "</b>";
        Description += "\n" + effect.Description + "\n";
        Description += "<b>Виляние на атрибуты:</b>\n";
        Description += GetAttributesDesctiption(effect);

        UIPart.Name.text = Description;
    }
}
