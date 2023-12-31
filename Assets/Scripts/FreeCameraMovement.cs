using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FreeCameraMovement : MonoBehaviour
{
    public float sensitivity = 5;
    private float mouseX;
    private float mouseY;
    private bool rotateEnabled = false;

    [SerializeField] float speed = 5000;
    [SerializeField] float jumpStrength = 5000;
    private Vector3 verticalMovementDir;
    private Vector3 horizontalMovementDir;
    void Start()
    {
        transform.eulerAngles = new Vector3(45, 225, 0);
        transform.position = new Vector3(3000, 3000, 3000);
    }

    // Update is called once per frame
    void Update()   
    {
        if (Input.GetKey(KeyCode.R))
        {
            mouseX = 0;
            mouseY = 0;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rotateEnabled = true;
        }

        if (Input.GetMouseButtonUp (1)) {
            rotateEnabled = false;
        }

        if (rotateEnabled) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseX += Input.GetAxis("Mouse X");
            // mouseX = Mathf.Clamp(mouseX, -90, 90);
            mouseY += Input.GetAxis("Mouse Y");
            mouseY = Mathf.Clamp(mouseY, -30, 30);
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        transform.localEulerAngles = sensitivity * new Vector3(-mouseY, mouseX, 0f);
            
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // using transform.Translate performs the action locally as opposed to globally, so the camera
        // rotates according to localEulerAngles. Use transform.position += instead.
        transform.position += vertical * speed * Time.deltaTime * new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        // transform.position += new Vector3(horizontal * _speed * Time.deltaTime, 0f, 0f);
        

        if (Input.GetKey(KeyCode.Space)) {  
            transform.position += new Vector3(0f, jumpStrength * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {  
            transform.position += new Vector3(0f, -jumpStrength * Time.deltaTime, 0f);
        }
    }
}