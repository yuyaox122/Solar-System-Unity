using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float _speed = 10;
    public GameObject playerCamera;
    [SerializeField] float jump_strength = 5;
    [SerializeField] Transform PlayerCameraTransform;
    private Vector3 verticalMovementDir;
    private Vector3 horizontalMovementDir;
    private int planetTeleportIndex;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        // viewAngles = playerCamera.transform.eulerAngles;
        // Debug.Log(viewAngles);
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        verticalMovementDir = vertical * _speed * Time.deltaTime * playerCamera.transform.forward;
        horizontalMovementDir = horizontal * _speed * Time.deltaTime * playerCamera.transform.right;
        transform.Translate(
            verticalMovementDir.x + horizontalMovementDir.x,
            0f,
            verticalMovementDir.z + horizontalMovementDir.z
            );
        
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
            transform.position = planets[planetTeleportIndex].transform.position;
            planetTeleportIndex ++;
            planetTeleportIndex %= planets.Length;
        }
    }
}
