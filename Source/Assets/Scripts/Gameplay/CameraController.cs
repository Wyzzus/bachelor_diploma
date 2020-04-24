﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 10;
    public float CameraSpeed = 100;
    public GameObject MainCamera;
    public GameObject PlayerPosition;
    public GameObject CameraShoulder;
    public bool Rotatable = false;
    public Vector3 CameraPosition;

    void Update()
    {
        CameraMovement();
    }

    public void CameraMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(x, 0, z);
        transform.position += Direction*Speed*Time.deltaTime;        
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && MainCamera.transform.position.z < -5)
        {
            Debug.Log("Колесико вверх");
            MainCamera.transform.localPosition += new Vector3(0, 0, CameraSpeed*Time.deltaTime);
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0 && MainCamera.transform.position.z > -60)
        {
            Debug.Log("Колесико вниз");
            MainCamera.transform.localPosition -= new Vector3(0, 0, CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Нажат пробел");
            //transform.position = PlayerPosition.transform.position;
            MainCamera.transform.localPosition = new Vector3(0, 0, -14);
        }
        if (Input.GetMouseButtonDown(2))
            Rotatable = !Rotatable;
        if (Rotatable == true)
        {
            Debug.Log(CameraShoulder.transform.rotation.eulerAngles.x);
            CameraShoulder.transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"), Space.World);
        }
        CameraPosition = MainCamera.transform.localPosition;
    }
}
