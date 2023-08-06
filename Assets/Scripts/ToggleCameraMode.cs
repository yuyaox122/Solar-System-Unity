using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCameraMode : MonoBehaviour
{
    [SerializeField] Toggle ToggleCamera;
    [SerializeField] FreeCameraMovement FreeCamera;
    [SerializeField] SphericalCameraMovement SphericalCamera;
    bool toggled = false;
    void Start()
    {
        ToggleCamera.onValueChanged.AddListener((v) => {
            toggled = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (toggled) {
            FreeCamera.enabled = true;
            SphericalCamera.enabled = false;
        }
        else {
            FreeCamera.enabled = false;
            SphericalCamera.enabled = true;
        }
    }
}
