using Unity.VisualScripting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    SolarSystem currentSolarSystem;
    public string planet;
    public string solarSystemName;
    public GameObject GameController;
    float G = 6.67f * Mathf.Pow(10f, (-11f));
    [SerializeField] Transform Sun;
    PlanetOrbit SunScript;
    public string[] planets;
    public float[] masses; // Earth masses
    public float[] semiMajor; //AU
    public float[] radii; // Earth radii
    public float[] rotational_periods; // days
    public float[] orbital_periods; // years
    public float[] gravities; // in terms of g = 9.81 m/s^2
    public float[] eccentricities;
    public float[] inclination_angles;
    public float[] trail_red;
    public float[] trail_green;
    public float[] trail_blue;
    private float t;
    bool logarithmicSizes = false;
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
    float orbit_scale = 10000;
    float time_scale;
    int index;
    private GameObject solarSystem;
    private bool threeDOrbit;
    private TrailRenderer tr;
    float new_time_scale;

    void Start()
    {
        solarSystemName = PlayerPrefs.GetString("!ActiveSolarSystem!");
        currentSolarSystem = SaveAndLoad.LoadSolarSystem(solarSystemName);
        planets = currentSolarSystem.planets;
        masses = currentSolarSystem.masses; // Earth masses
        semiMajor = currentSolarSystem.semiMajor; //AU
        radii = currentSolarSystem.radii; // Earth radii
        rotational_periods = currentSolarSystem.rotational_periods; // days
        orbital_periods = currentSolarSystem.orbital_periods; // years
        gravities = currentSolarSystem.gravities; // in terms of g = 9.81 m/s^2
        eccentricities = currentSolarSystem.eccentricities;
        inclination_angles = currentSolarSystem.inclination_angles;
        trail_red = currentSolarSystem.trail_red;
        trail_green = currentSolarSystem.trail_green;
        trail_blue = currentSolarSystem.trail_blue;
        solarSystem = transform.parent.gameObject;
        threeDOrbit = solarSystem.GetComponent<SolarSystemProperties>().ThreeDOrbits;
        SunScript = Sun.GetComponent<PlanetOrbit>();
        // radii = radii.Select(el => el / 23454.8f).ToArray();
        // semiMajor = semiMajor.Select(el => el * 100).ToArray();
        // radii = radii.Select(el => el * 10).ToArray();
        if (planet != "Sun")
        {
            time_scale = GameController.GetComponent<EventController>().ReturnTimeScale();
            index = Array.IndexOf(planets, planet);
            mass = masses[index];
            if (logarithmicOrbits)
            {
                a = Mathf.Log(semiMajor[index], 1.001f);
            }
            else
            {
                a = semiMajor[index];
            }
            radius = radii[index];
            rotational_period = rotational_periods[index];
            orbital_period = orbital_periods[index];
            gravity = gravities[index];
            eccentricity = eccentricities[index];
            inclination_angle = inclination_angles[index];

            if (logarithmicSizes)
            {
                radiusScale = Mathf.Log(radius, 10) + 1;
            }
            else
            {
                radiusScale = radius * 50;
            }
            transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
            gameObject.AddComponent<TrailRenderer>();
            tr = GetComponent<TrailRenderer>();
            tr.material = new Material(Shader.Find("Sprites/Default"));
            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            float trail_red_1 = Mathf.Clamp(trail_red[index] + 0.15f, 0f, 1f);
            float trail_green_1 = Mathf.Clamp(trail_green[index] + 0.15f, 0f, 1f);
            float trail_blue_1 = Mathf.Clamp(trail_blue[index] + 0.15f, 0f, 1f);
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(new Color (trail_red_1, trail_green_1, trail_blue_1), 0.0f), new GradientColorKey(new Color (trail_red[index], trail_blue[index], trail_green[index]), 0.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            tr.colorGradient = gradient;
            tr.time = orbital_period / time_scale;
            tr.startWidth = radiusScale * 0.2f;
            tr.endWidth = radiusScale * 0.2f;
            // Debug.Log(planet);
            // Debug.Log(orbital_period);   
        }
        else
        {
        mass = 332837f;
        radius = 109.12f;
        gravity = 27.95f;
        if (logarithmicSizes)
        {
            radiusScale = Mathf.Log(radius, 10) + 1;
        }
        else
        {
            radiusScale = radius * 10;
        }
        transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
    }

    }

    // Update is called once per frame
    void Update()
    {
    
    if (planet != "Sun")
    {
        if (logarithmicSizes)
            {
                radiusScale = Mathf.Log(radius, 10) + 1;
            }
            else
            {
                radiusScale = radius * 50;
            }
        orbital_period = get_orbital_period(a, G, mass, Sun.GetComponent<PlanetOrbit>().getMass());
        tr.time = orbital_period / time_scale;
        // Debug.Log(tr.time);
        // Debug.Log(planet);
        new_time_scale = GameController.GetComponent<EventController>().ReturnTimeScale();
        if (new_time_scale != time_scale)
        {
            time_scale = new_time_scale;
            clearTrails();
        }

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
    else {
        if (logarithmicSizes)
        {
            radiusScale = Mathf.Log(radius, 10) + 1;
        }
        else
        {
            radiusScale = radius * 10;
        }
    }
}

public void clearTrails()
{
    tr.enabled = false;
    // Debug.Log(planet);
    tr.Clear();
    tr.enabled = true;
}

double updateSemiMajor(float m, float M, float r, float v)
{
    float mu = G * (m + M);
    return (mu * r) / (2 * mu - v * v * r);
}

double updateEccentricity(float m, float M, float r, float v)
{
    float mu = G * (m + M);
    return Math.Sqrt(1-r*r*v*v*(2*mu-v*v*r)/(mu*mu*r));
}

float get_r(float a, float epsilon, float theta)
{
    return (a * (1 - Mathf.Pow(epsilon, 2f))) / (1 - epsilon * Mathf.Cos(theta));
}

float get_orbital_period(float a, float G, float m, float M)
{
    return ((Mathf.Sqrt((4 * Mathf.Pow(Mathf.PI, 2f) * Mathf.Pow(a * 1.496f * Mathf.Pow(10f, 11f), 3f)) / (G * (5.97f * Mathf.Pow(10f, 24f)) * (M + m)))) / (365 * 24 * 60 * 60));
}

public string getPlanet()
{
    return planet;
}

public float getMass()
{
    return mass;
}

public float getSemiMajor()
{
    return a;
}

public float getRadius()
{
    return radius;
}

public float getRotationalPeriod()
{
    return rotational_period;
}

public float getGravity()
{
    return gravity;
}

public float getEccentricity()
{
    return eccentricity;
}

public float getInclinationAngle()
{
    return inclination_angle;
}

public void changeGameName(string newGameName)
{
    solarSystemName = newGameName;
    Debug.Log("Change game name: " + solarSystemName);
}
public void changePlanet(string newPlanet)
{
    planet = newPlanet;
    planets[index] = planet;
    clearTrails();
}

public void changeMass(float newMass)
{
    mass = newMass;
    SunScript.masses[index] = mass;
    clearTrails();
}

public void changeSemiMajor(float newSemiMajor)
{
    a = newSemiMajor;
    SunScript.semiMajor[index] = a;
    clearTrails();
}

public void changeRadius(float newRadius)
{
    radius = newRadius;
    SunScript.radii[index] = radius;
    clearTrails();
}

public void changeRotationalPeriod(float newRotationalPeriod)
{
    rotational_period = newRotationalPeriod;
    SunScript.rotational_periods[index] = rotational_period;
    clearTrails();
}

public void changeGravity(float newGravity)
{
    gravity = newGravity;
    SunScript.gravities[index] = gravity;
    clearTrails();
}

public void changeEccentricity(float newEccentricity)
{
    eccentricity = newEccentricity;
    SunScript.eccentricities[index] = eccentricity;
    clearTrails();
}

public void changeInclinationAngle(float newInclinationAngle)
{
    inclination_angle = newInclinationAngle;
    SunScript.inclination_angles[index] = inclination_angle;
    clearTrails();
}
}
