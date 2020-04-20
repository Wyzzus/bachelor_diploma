using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    public LayerMask LayerMask;
    public Vector3 NewPosition = Vector3.zero;
    public CommonEntity Common;
    public Text Result;
    
    public void Update()
    {
        Movement();
        Common.GetDiceResult();
    }
    #region Common

    public void Movement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("ПКМ нажата");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, LayerMask))
            {
                NewPosition = new Vector3(hit.point.x, 0, hit.point.z);
                //Debug.Log(NewPosition);
            }
        }
        Common.MoveTo(NewPosition);
    }

    public void CallDice(int n)
    {
        Common.CallDice(n);
    }

    #endregion
}
