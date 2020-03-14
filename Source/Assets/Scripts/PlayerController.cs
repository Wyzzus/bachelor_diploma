using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public LayerMask layerMask;
    public Vector3 NewPosition = Vector3.zero;
    public float MoveDamp = 10f;

    void Update()
    {
        Movement();
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
        transform.position = Vector3.Lerp(transform.position, NewPosition, Time.deltaTime * MoveDamp);
    }

}
