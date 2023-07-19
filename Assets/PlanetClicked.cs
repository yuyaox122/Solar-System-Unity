// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class DisplayPlanetInfo : MonoBehaviour
// {
//     public bool trackingPlanet;

//     public Text PlanetInfo;
//     public GameObject player; // Assign your player in the inspector
//     public Text distanceText; // Assign your Text UI element in the inspector
//     // Start is called before the first frame update
//     void Start()
//     {
//         trackingPlanet = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void OnMouseDown()
//     {
//         trackingPlanet = true;
//         float distance = Vector3.Distance(player.transform.position, transform.position);
//         distanceText.text = "Distance: " + distance.ToString();
//         PlanetInfo.GetComponent<DisplayPlanetInfo>().DisplayInfo();

//     }

//     void OnMouseUp()
//     {
//         // Clear the distance text
//         distanceText.text = "";
//         trackingPlanet = false;
//     }

// }