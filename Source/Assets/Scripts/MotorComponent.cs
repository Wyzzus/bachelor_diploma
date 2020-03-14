﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorComponent : EntityComponent
{
    public float MoveDamp = 10f;
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
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * MoveDamp);
    }
}