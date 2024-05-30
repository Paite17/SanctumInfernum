using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private GameObject orientation;
    [SerializeField] private float rotateSpeed = 5f;
    float inputX;
    float inputY;
    float desiredAngleX;
    float desiredAngleY;
    Quaternion rotation;
    Vector3 offset;

    private void Start()
    {
        offset = orientation.transform.position - transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        
    }

    private void LateUpdate()
    {
        inputX = Input.GetAxis("Mouse X") * rotateSpeed;
        inputY = Input.GetAxis("Mouse Y") * rotateSpeed;
        orientation.transform.Rotate(inputY, inputX, 0);

        desiredAngleY = orientation.transform.eulerAngles.y;
        desiredAngleX = orientation.transform.eulerAngles.x;
        
        //desiredAngleX = Mathf.Clamp(orientation.transform.eulerAngles.x, -90, 90);

        rotation = Quaternion.Euler(-desiredAngleX, desiredAngleY, 0);
        transform.position = orientation.transform.position - (rotation * offset);

        transform.LookAt(orientation.transform.position);
    }
}

