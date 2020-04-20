using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceComponent : MonoBehaviour
{
    public string Result;
    public int diceCount;

    public string GetResult()
    {
        return Result;
    }

    public void DiceRoll(int n)
    {
        Result = "Бросаем кубик ";
        StopAllCoroutines();
        StartCoroutine(DelayedRoll(n));
    }

    IEnumerator DelayedRoll(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            Result += ".";
            yield return new WaitForSeconds(0.4F);
        }
        diceCount = Random.Range(1, n + 1);
        Result = "Выпало " + diceCount;
    }

    public int GetDiceCount()
    {
        return diceCount;
    }
}
