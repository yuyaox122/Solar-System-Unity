using Unity.VisualScripting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet2DOrbit : MonoBehaviour
{
    public string planet;
    [SerializeField] Transform Sun;
    private string[] planets = {"Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto"};
    private float[] masses = new float[] {0.055f, 0.815f, 1.000f, 0.107f, 317.85f, 95.159f, 14.500f, 17.204f, 0.003f}; // Earth masses
    private float[] semiMajor = new float[] {0.387f, 0.723f, 1.000f, 1.523f, 5.202f, 9.576f, 19.293f, 30.246f, 39.509f}; //AU
    private float[] radii = new float[] {0.383f, 0.949f, 1.000f, 0.533f, 11.209f, 9.449f, 4.007f, 3.883f, 0.187f}; // Earth radii
    private float[] rotational_periods = new float[] {58.646f, 243.018f, 0.997f, 1.026f, 0.413f, 0.444f, 0.718f, 0.671f, 6.387f}; // days
    private float[] orbital_periods = new float[] {0.241f, 0.615f, 1.000f, 1.881f, 11.861f, 29.628f, 84.747f, 166.344f, 248.348f}; // years
    private float[] gravities = new float[] {0.37f, 0.90f, 1.00f, 0.38f, 2.53f, 1.07f, 0.90f, 1.14f, 0.09f}; // in terms of g = 9.81 m/s^2
    private float[] eccentricities = new float[] {0.21f, 0.01f, 0.02f, 0.09f, 0.05f, 0.06f, 0.05f, 0.01f, 0.25f};
    private float[] inclination_angles = new float[] {7.00f, 3.39f, 0.00f, 1.85f, 1.31f, 2.49f, 0.77f, 1.77f, 17.5f};
    private float t;
    bool logarithmicSizes = true;
    bool logarithmicOrbits = false;
    float mass; // Earth masses
    float a; //AU
    float radius; // Earth radii
    float rotational_period; // days
    float orbital_period; // years
    float gravity; // in terms of g = 9.81 m/s^2
    float eccentricity;
    float inclination_angle;
    float radiusScale;
    float orbit_scale = 50;
    float time_scale = 1000;

    void Start()
    {
        // radii = radii.Select(el => el / 23454.8f).ToArray();
        if (planet != "Sun") {
            int index = Array.IndexOf(planets, planet);
            mass = masses[index];
            if (logarithmicOrbits) {
                a = Mathf.Log(semiMajor[index], 1.001f);
            } else
            {
                a = semiMajor[index];
            }
            radius = radii[index]; 
            rotational_period = rotational_periods[index];
            orbital_period = orbital_periods[index];
            gravity = gravities[index]; 
            eccentricity = eccentricities[index];
            inclination_angle = inclination_angles[index];
            
            if (logarithmicSizes) {
                radiusScale = Mathf.Log(radius, 10) + 1;
            } else {
                radiusScale = radius;
            }
            transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
            

        }
        else {
            mass = 332837f;
            radius = 109.12f;
            gravity = 27.95f;
            if (logarithmicSizes)
            {
                radiusScale = Mathf.Log(radius, 10) + 1;
            }
            else
            {
                radiusScale = radius;
            }
            transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (planet != "Sun") {
            float t = (((Time.time / 0.01f) / (orbital_period * time_scale)) * 2 * Mathf.PI);
            // Debug.Log(t);
            transform.position = new Vector3(orbit_scale * get_r(a, eccentricity, t) * Mathf.Cos(t), 0f, orbit_scale * get_r(a, eccentricity, t) * Mathf.Sin(t));
            // UpdatePlanet();
        }  
    }

    // IEnumerator UpdatePlanet() {
        
    //     yield return new WaitForSeconds(1);
    //     tick += 1;
    // }

    float get_r(float a, float epsilon, float theta) {
        return (a * (1 - Mathf.Pow(epsilon, 2))) / (1 - epsilon * Mathf.Cos(theta));
    }
}
