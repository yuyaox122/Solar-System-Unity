using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerCamera;
    [SerializeField] float _speed = 100;
    [SerializeField] float jump_strength = 100;
    
    public Vector3 spawn_point;
    private Vector3 verticalMovementDir;
    private Vector3 horizontalMovementDir;
    private int planetTeleportIndex;
    private string[] planetNames = {"Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto"};

    private KeyCode[] teleportKeys = new KeyCode[] {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9};
    private Dictionary<KeyCode, string> teleportDict = new Dictionary<KeyCode, string>()
    {
        {KeyCode.Alpha1, "Mercury"},
        {KeyCode.Alpha2, "Venus"},
        {KeyCode.Alpha3, "Earth"},
        {KeyCode.Alpha4, "Mars"},
        {KeyCode.Alpha5, "Jupiter"},
        {KeyCode.Alpha6, "Saturn"},
        {KeyCode.Alpha7, "Uranus"},
        {KeyCode.Alpha8, "Neptune"},
        {KeyCode.Alpha9, "Pluto"}
        
    };
    
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
        
        foreach (var key in teleportKeys)
            if (Input.GetKey(key))
            {
                GameObject planet = GameObject.Find(teleportDict[key]);
                Vector3 pos = planet.transform.position;
                Debug.Log(planet.name);
                transform.position = new Vector3(pos.x, pos.y + 50, pos.z);
            }
    }
}
