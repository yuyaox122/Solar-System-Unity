using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
    private Transform myTransform;
 
 
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }
 
 
    void Update()
    {
        attractor.Attract(myTransform);
    }
}