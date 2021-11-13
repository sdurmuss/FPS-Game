using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float rotateX = 0;
    float rotateY = 0;
    [SerializeField] float sensivity = 5f;
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        rotateX += Input.GetAxis("Mouse X") * sensivity;
        rotateY += Input.GetAxis("Mouse Y") * sensivity/2;
        rotateY = Mathf.Clamp(rotateY, -85, 85);
        transform.eulerAngles = new Vector3(-rotateY, rotateX, 0f);
    }
}
