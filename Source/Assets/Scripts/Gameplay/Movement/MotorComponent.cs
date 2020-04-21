using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorComponent : EntityComponent
{
    public float MoveDamp = 10f;
    public Transform Body;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void MoveTo(Vector3 newPosition)
    {
        Body.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * MoveDamp);
    }
}
