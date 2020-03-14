using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceComponent : MonoBehaviour
{
    public string Result;
    public Text Field;

    private void Update()
    {
        Field.text = Result;
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
        
        Result = "Выпало " + Random.Range(1, n + 1);
    }
}
