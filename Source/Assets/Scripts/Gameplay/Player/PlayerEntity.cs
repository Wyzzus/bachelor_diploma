using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public EffectsComponent Effects;


    public override void Start()
    {
        base.Start();
    }

    public void SetupEffects(List<int> effectsIds)
    {
        Effects.SetupEffects(effectsIds);
    }


}
