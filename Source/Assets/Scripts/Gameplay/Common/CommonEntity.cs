using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEntity : Entity
{
    public MotorComponent Motor;
    public DiceComponent Dice;
    public MarkerComponent Marker;
    public PlayerInfoComponent PlayerInfo;
    
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

    public void SetupPlayerInfo()
    {
        PlayerInfo.FillSkinSelector(GameManager.instance.CurrentThemePack.Avatars);
    }

    #region Getters


    public string GetName()
    {
        return PlayerInfo.Name.text;
    }

    public int GetSkin()
    {
        return PlayerInfo.SkinSelector.value;
    }

    public string GetDiceResult()
    {
        return Dice.GetResult();
    }

    //public 

    #endregion
}
