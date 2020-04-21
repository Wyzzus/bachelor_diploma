using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsComponent : EntityComponent
{
    public ScrollViewHandler EffectsHandler;
    public List<Effect> CurrentEffects;

    public DndObjectUI UIPart;

    public override void Start()
    {
        base.Start();
    }

    public void SetupEffects(PlayerData Data)
    {
        UpdateView(Data.Effects);
    }

    public void UpdateView(List<int> effectsIds)
    {
        EffectsHandler.UpdateOnComponent(effectsIds, typeof(Effect), this);
        if (effectsIds.Count > 0)
            Display(effectsIds[0]);
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
