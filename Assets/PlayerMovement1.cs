using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 10;
    [SerializeField] float jump_strength = 5;
    [SerializeField] float sensitivity = 1;
    [SerializeField] Transform PlayerCameraTransform;
    int planet_teleport_index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(speed * Time.deltaTime * horizontal, 0f, 0f);
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(0f, 0f, speed * Time.deltaTime * vertical);
        if (Input.GetKey(KeyCode.Space)) {  
            // GetComponent<Rigidbody> ().AddForce(0f, jump_strength, 0F, ForceMode.Impulse);
            transform.Translate(0f, jump_strength * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {  
            // GetComponent<Rigidbody> ().AddForce(0f, jump_strength, 0F, ForceMode.Impulse);
            transform.Translate(0f, -jump_strength * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.E)) {  
            GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
            transform.position = planets[planet_teleport_index].transform.position;
            planet_teleport_index ++;
            planet_teleport_index %= planets.Length;
        }
        
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");
        PlayerCameraTransform.Rotate(0f, sensitivity * mousex, 0f);
        PlayerCameraTransform.Rotate(-sensitivity * mousey, 0f, 0f);
    }
}
