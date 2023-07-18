using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Vector3 mouseTurn;
    public float sensitivity = 3;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseTurn.x -= Input.GetAxis("Mouse Y");
        mouseTurn.y += Input.GetAxis("Mouse X");
        transform.eulerAngles = sensitivity * mouseTurn;
    }
}
