using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public LayerMask layerMask;
    public Vector3 NewPosition = Vector3.zero;
    public PlayerEntity playerEntity;
    public Text Result;
    
    void Update()
    {
        Movement();
        Result.text = playerEntity.GetDice();
    }

    public void Movement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("ПКМ нажата");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                NewPosition = new Vector3(hit.point.x, 0, hit.point.z);
                Debug.Log(NewPosition);
            }
        }
        playerEntity.MoveTo(NewPosition);
    }

    public void CallDice(int n)
    {
        playerEntity.CallDice(n);
    }
}
