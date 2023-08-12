using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlanet : MonoBehaviour
{
    public Transform target;
    public string activePlanet;
    public Vector3 offset;
    public bool active;
    Camera MainCamera;
    SphericalCameraMovement SphericalScript;
    FreeCameraMovement FreeScript;

    void Start()
    {
        MainCamera = Camera.main;
        FreeScript = MainCamera.GetComponent<FreeCameraMovement>();
        SphericalScript = MainCamera.GetComponent<SphericalCameraMovement>();
        active = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            offset =  new Vector3(200, 200, 200) * target.GetComponent<PlanetOrbit>().radius / 0.383f;
            // Debug.Log(target.GetComponent<PlanetOrbit>().radius);
            transform.position = target.position + offset;
            transform.LookAt(target);
            FreeScript.enabled = false;
            SphericalScript.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           active = false;
        }
    }

    public void ToggleActive() {
        active = !active;
    }

    public void GoToPlanet(Transform newTarget) {
        target = newTarget;
        active = true;
    }
}
