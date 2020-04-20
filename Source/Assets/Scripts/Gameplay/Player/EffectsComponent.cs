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

    public void SetupEffects(List<int> effectsIds)
    {
        CurrentEffects.Clear();
        for(int i = 0; i < effectsIds.Count; i++)
        {
            Effect newEffect = new Effect(); //Get Effect by Id
            CurrentEffects.Add(newEffect);
        }
        UpdateView();
    }

    public void UpdateView()
    {
        EffectsHandler.UpdateOnComponent(CurrentEffects, this);
    }

    public void ShowEffect(Effect effect)
    {
        UIPart.ShowObject(effect);
    }
}
