// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerSpaceShip : MonoBehaviour
// {
    
//     Rigidbody spaceshipRB;

//     float verticalMove;
//     float horizontalMove;
//     float mouseInputX;
//     float mouseInputY;
//     float rollInput;

//     float speedMult = 10f;
//     float speedMultAngle = 0.1f;
//     float speedRollMultAngle = 0.05f;
//     float maxSpeed = 10000f;
//     Vector3 currentVelocity;
//     void Start()
//     {
//         // Cursor.lockState = CursorLockMode.Locked;
//         spaceshipRB = GetComponent<Rigidbody>();
//         // transform.position = new Vector3(1000, 1000, 1000);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         verticalMove = Input.GetAxis("Vertical");
//         horizontalMove = Input.GetAxis("Horizontal");
//         rollInput = Input.GetAxis("Roll");

//         mouseInputX = Input.GetAxis("Mouse X");
//         mouseInputY = Input.GetAxis("Mouse Y");
//     }

//     void FixedUpdate() {
//         currentVelocity = GetComponent<Rigidbody>().velocity;
//         spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
//         spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.right) * horizontalMove * speedMult, ForceMode.VelocityChange);
//         spaceshipRB.AddTorque(spaceshipRB.transform.right * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
//         spaceshipRB.AddTorque(spaceshipRB.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);
//         spaceshipRB.AddTorque(spaceshipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);
//         transform.Rotate(0, 0, rollInput, Space.Self);
//         Debug.Log(currentVelocity);
//     }

// }

