using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float sensitivity = 3;
    public GameObject player;
    public Vector3 offset = new Vector3(10, 10, 10);

    private float mouseX;
    private float mouseY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()   
    {
        if (Input.GetKey(KeyCode.R))
        {
            mouseX = 0;
            mouseY = 0;
        }
        else
        {
            mouseX += Input.GetAxis("Mouse X");
            // mouseX = Mathf.Clamp(mouseX, -90, 90);
            mouseY += Input.GetAxis("Mouse Y");
            mouseY = Mathf.Clamp(mouseY, -30, 30);
        }
        transform.localEulerAngles = sensitivity * new Vector3(-mouseY, mouseX, 0f);

    }

    private void LateUpdate()
    {
        // transform.position = player.transform.position + offset;
    }
}
