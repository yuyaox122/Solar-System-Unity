using Unity.VisualScripting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet2DOrbit : MonoBehaviour
{
    public string planet;
    public GameObject GameController;
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
    private Color[,] trail_colours = new Color[,]
    {
        {new Color (0.639f, 0.416f, 0.078f), new Color (0.839f, 0.616f, 0.278f)},
        {new Color (0.678f, 0.329f, 0.000f), new Color (0.878f, 0.529f, 0.176f)},
        {new Color (0.000f, 0.278f, 0.522f), new Color (0.157f, 0.478f, 0.722f)},
        {new Color (0.557f, 0.067f, 0.000f), new Color (0.757f, 0.267f, 0.055f)},
        {new Color (0.588f, 0.365f, 0.024f), new Color (0.788f, 0.565f, 0.224f)},
        {new Color (0.443f, 0.408f, 0.247f), new Color (0.643f, 0.608f, 0.447f)},
        {new Color (0.376f, 0.522f, 0.545f), new Color (0.576f, 0.722f, 0.745f)},
        {new Color (0.127f, 0.179f, 0.429f), new Color (0.247f, 0.329f, 0.729f)},
        {new Color (0.761f, 0.643f, 0.576f), new Color (0.961f, 0.843f, 0.776f)},
        
        
    };
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
    float time_scale;
    int index;
    private GameObject solarSystem;
    private bool threeDOrbit;
    private TrailRenderer tr;
    float new_time_scale;

    void Start()
    {
        solarSystem = transform.parent.gameObject;
        threeDOrbit = solarSystem.GetComponent<SolarSystemProperties>().ThreeDOrbits;
        radii = radii.Select(el => el / 23454.8f).ToArray();
        // semiMajor = semiMajor.Select(el => el * 100).ToArray();
        // radii = radii.Select(el => el * 10).ToArray();
        if (planet != "Sun") {
            time_scale = GameController.GetComponent<EventController>().ReturnTimeScale();
            index = Array.IndexOf(planets, planet);
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
            gameObject.AddComponent<TrailRenderer>();
            tr = GetComponent<TrailRenderer>();
            tr.material = new Material(Shader.Find("Sprites/Default"));
            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(trail_colours[index, 1], 0.0f), new GradientColorKey(trail_colours[index, 0], 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            tr.colorGradient = gradient;
            tr.time = orbital_period * time_scale * 0.9f;
            Debug.Log(tr.time);
            tr.startWidth = radiusScale * 0.8f;
            tr.endWidth = radiusScale * 0.6f;
            // Debug.Log(planet);
            // Debug.Log(orbital_period);
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
            
            tr.time = orbital_period * time_scale * 0.9f;
            new_time_scale = GameController.GetComponent<EventController>().ReturnTimeScale();
            if (new_time_scale != time_scale) {
                time_scale = new_time_scale;
                StartCoroutine(ResetTrail(tr));
                tr.Clear();
            }
        }
        if (planet != "Sun") {
            float t = ((Time.time * 2 * Mathf.PI) / (orbital_period)) * time_scale;
            if (!threeDOrbit)
            {
                // Debug.Log(t);
                transform.position = new Vector3(orbit_scale * get_r(a, eccentricity, t) * Mathf.Cos(t), 0f,
                orbit_scale * get_r(a, eccentricity, t) * Mathf.Sin(t));
                // UpdatePlanet();
            }
            else if (threeDOrbit)
            {
                // Debug.Log(t);
                transform.position = new Vector3(
                    orbit_scale * get_r(a, eccentricity, t) * Mathf.Cos(t) * Mathf.Cos(inclination_angle * Mathf.PI / 180),
                    orbit_scale * get_r(a, eccentricity, t) * Mathf.Cos(t) * Mathf.Sin(inclination_angle * Mathf.PI / 180),
                    orbit_scale * get_r(a, eccentricity, t) * Mathf.Sin(t)
                );
                // UpdatePlanet();
            }
        }  
    }

    float get_r(float a, float epsilon, float theta) {
        return (a * (1 - Mathf.Pow(epsilon, 2))) / (1 - epsilon * Mathf.Cos(theta));
    }

    static IEnumerator ResetTrail(TrailRenderer tr)
    {
        var trailTime = tr.time;
        tr.time = 0;
        yield return new WaitForSeconds(.1f);;
        tr.time = trailTime;
    }        

    public string getPlanet() {
        return planet;
    }

    public float getMass() {
        return a;
    }

    public float getSemiMajor() {
        return a;
    }

    public float getRadius() {
        return radius;
    }

    public float getRotationalPeriod() {
        return rotational_period;
    }

    public float getGravity() {
        return gravity;
    }

    public float getEccentricity() {
        return eccentricity;
    }

    public float getInclinationAngle() {
        return inclination_angle;
    }
}
