using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlanet : MonoBehaviour
{
    public Transform target;
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
        offset = new Vector3(500, 500, 500);
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            transform.position = target.position + offset;
            transform.LookAt(target);
            FreeScript.enabled = false;
            SphericalScript.enabled = false;
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
