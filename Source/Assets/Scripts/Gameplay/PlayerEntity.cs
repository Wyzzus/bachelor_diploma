using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public MotorComponent Motor;
    public DiceComponent Dice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3 newPosition)
    {
        Motor.MoveTo(newPosition);
    }

    public void CallDice(int n)
    {
        Dice.DiceRoll(n);
    }

    public string GetDice()
    {
        return Dice.GetResult();
    }
}
