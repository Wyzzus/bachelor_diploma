using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEntity : Entity
{
    public MotorComponent Motor;
    public DiceComponent Dice;
    public MapComponent Map;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void MoveTo(Vector3 pos)
    {
        Motor.MoveTo(pos);
    }


    public void CallDice(int n)
    {
        Dice.DiceRoll(n);
    }

    public string GetDiceResult()
    {
        return Dice.GetResult();
    }


}
