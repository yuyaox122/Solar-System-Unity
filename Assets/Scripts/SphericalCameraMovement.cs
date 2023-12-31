using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalCameraMovement : MonoBehaviour
{
    bool ballEnabled = false;
 
    float mouseRotationSpeed = 1f;
    float keysRotationSpeed = 3f;
    float r = 3000f;
    float scrollSpeed = 100f;
 
    Vector3 last = new Vector3();

    [SerializeField] Transform target;
    Vector3 sc = new Vector3 ();
    Vector3 target_position;

    void Start ()
    {
        target_position = target.position;
        sc = new Vector3 (r * 10, 0.0f, 1.0f);
        transform.position = getCartesianCoordinates(sc);
        transform.LookAt (target_position);
    }
 
    Vector3 getSphericalCoordinates(Vector3 cartesian)
    {
        Debug.Log(r);
        float phi = Mathf.Atan2(cartesian.z / cartesian.x, cartesian.x);
        float theta = Mathf.Acos(cartesian.y / r);
 
        if (cartesian.x < 0)
            phi += Mathf.PI;
 
        return new Vector3 (r, phi, theta);
    }
 
    Vector3 getCartesianCoordinates(Vector3 spherical)
    {
        Vector3 ret = new Vector3 ();
 
        ret.x = r * Mathf.Cos (spherical.z) * Mathf.Cos (spherical.y);
        ret.y = r * Mathf.Sin (spherical.z);
        ret.z = r * Mathf.Cos (spherical.z) * Mathf.Sin (spherical.y);
 
        return ret;
    }
     
    void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            last = Input.mousePosition;
 
            ballEnabled = true;
        }
 
        if (Input.GetMouseButtonUp (1))
            ballEnabled = false;

        if (ballEnabled)
        {
            float dx = (last.x - Input.mousePosition.x) * mouseRotationSpeed;
            float dy = (last.y - Input.mousePosition.y) * mouseRotationSpeed;
            
            r += -Input.mouseScrollDelta.y * scrollSpeed;

            sc.y += dx * Time.deltaTime;

            sc.z = Mathf.Clamp (sc.z + dy * Time.deltaTime, -1.5f, 1.5f);
            r = Mathf.Clamp(r, 250, 1000000);

            
        }

        if (Input.GetKey(KeyCode.W)) {
            r -= scrollSpeed / 10;
        }
            
        if (Input.GetKey(KeyCode.S)) {
            r += scrollSpeed / 10;
        }

        if (Input.GetKey(KeyCode.A)) {
            sc.y += -keysRotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            sc.y += keysRotationSpeed * Time.deltaTime;
        }

        transform.position = getCartesianCoordinates (sc) + target_position;

        transform.LookAt(target_position);

        last = Input.mousePosition;
    }
}
