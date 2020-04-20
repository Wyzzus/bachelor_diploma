using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public MotorComponent Motor;
    public DiceComponent Dice;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
}
