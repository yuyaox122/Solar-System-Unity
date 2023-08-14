using Unity.VisualScripting;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlanetOrbit : MonoBehaviour
{
    SolarSystem currentSolarSystem;
    public string planet;
    public string solarSystemName;
    public GameObject GameController;
    string explore;
    public  float G = 6.67e-11f;
    public Transform Star;
    PlanetOrbit StarScript;
    public List<string> planets;
    public List<float> masses; // Earth masses
    public List<float> radii; // Earth radii
    public List<float> orbitalRadii; // AU
    public List<float> orbitalVelocities; // km/s
    public List<float> inclinationAngles;
    public List<float> trailRed;
    public List<float> trailGreen;
    public List<float> trailBlue;
    public List<string> planetPresets;
    private float t;
    bool logarithmicSizes = false;
    bool logarithmicOrbits = false;
    public float mass; // Earth masses
    public float radius; // Earth radii
    float orbitalRadius; // AU
    public float orbitalVelocity; // km/s
    float inclinationAngle;
    public float starMass;
    float semiMajor;
    float eccentricity;
    float orbitalPeriod;

    float radiusScale;
    float orbitScale = 10000;
    float timeScale;
    int index;
    private GameObject solarSystem;
    private bool threeDOrbit;
    private TrailRenderer tr;
    float newTimeScale;

    void Start()
    {
        solarSystemName = PlayerPrefs.GetString("!ActiveSolarSystem!");
        explore = PlayerPrefs.GetString("!Explore!");
        currentSolarSystem = SaveAndLoad.LoadSolarSystem(solarSystemName);
        planets = currentSolarSystem.planets;
        masses = currentSolarSystem.masses; // Earth masses
        radii = currentSolarSystem.radii; // Earth radii
        orbitalRadii = currentSolarSystem.orbitalRadii; // AU
        orbitalVelocities = currentSolarSystem.orbitalVelocities; // km/s
        inclinationAngles = currentSolarSystem.inclinationAngles;
        trailRed = currentSolarSystem.trailRed;
        trailGreen = currentSolarSystem.trailGreen;
        trailBlue = currentSolarSystem.trailBlue;
        planetPresets = currentSolarSystem.planetPresets;
        solarSystem = transform.parent.gameObject;
        threeDOrbit = solarSystem.GetComponent<SolarSystemProperties>().ThreeDOrbits;
        StarScript = Star.GetComponent<PlanetOrbit>();

        if (planet != "Sun")
        {
            timeScale = GameController.GetComponent<EventController>().ReturnTimeScale();
            index = planets.IndexOf(planet);
            mass = masses[index] * 5.972e24f; // kg
            radius = radii[index]; // Earth radii
            orbitalRadius = orbitalRadii[index] * 1.496e11f; // m
            orbitalVelocity = orbitalVelocities[index] * 1e3f; // m/s
            inclinationAngle = inclinationAngles[index];
            //starMass = StarScript.mass;
            starMass = 1.988e30f;
            semiMajor = calculateSemiMajor(mass, starMass, orbitalRadius, orbitalVelocity); // m
            eccentricity = calculateEccentricity(mass, starMass, orbitalRadius, orbitalVelocity);
            orbitalPeriod = calculateOrbitalPeriod(mass, starMass, semiMajor);

            semiMajor /= 1.496e11f;

            // Debug.Log($"{planet}: orbitalPeriod={orbitalPeriod}");

            if (logarithmicSizes)
            {
                radiusScale = Mathf.Log(radius, 10) + 1;
            }
            else
            {
                radiusScale = radius * 50;
            }

            if (explore == "1") {
                radiusScale *= 10;
                orbitScale *= 5;
            }
            transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
            gameObject.AddComponent<TrailRenderer>();
            tr = GetComponent<TrailRenderer>();
            tr.material = new Material(Shader.Find("Sprites/Default"));
            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            float trailRed1 = Mathf.Clamp(trailRed[index] + 0.15f, 0f, 1f);
            float trailGreen1 = Mathf.Clamp(trailGreen[index] + 0.15f, 0f, 1f);
            float trailBlue1 = Mathf.Clamp(trailBlue[index] + 0.15f, 0f, 1f);
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(new Color(trailRed1, trailGreen1, trailBlue1), 0.0f), new GradientColorKey(new Color(trailRed[index], trailBlue[index], trailGreen[index]), 0.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            tr.colorGradient = gradient;
            if (explore == "1")
            {
                tr.time = orbitalPeriod / timeScale;
            }
            else
            {
                tr.time = 0;
            }

            tr.startWidth = radiusScale * 0.2f;
            tr.endWidth = radiusScale * 0.2f;
            // tr.startWidth = 0.2f;
            // tr.endWidth = 0.2f;
        }
        else
        {
            mass = 1.988e30f;
            radius = 109.076f;
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

        if (planet == "Earth")
        {
            Debug.Log($"Earth - eccentricity: {eccentricity}, semiMajor: {semiMajor}");
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
            orbitalPeriod = calculateOrbitalPeriod(mass, starMass, semiMajor);
            newTimeScale = GameController.GetComponent<EventController>().ReturnTimeScale() / 1e18f;

            if (newTimeScale != timeScale)
            {
                timeScale = newTimeScale;
                clearTrails();
            }

            if (explore == "1") {
                timeScale /= 100;
            }
            tr.time = orbitalPeriod / timeScale;
            // Debug.Log(tr.time);
            float t = ((Time.time * 2 * Mathf.PI) / (orbitalPeriod)) * timeScale;

            if (!threeDOrbit)
            {
                // Debug.Log(t);
                transform.position = new Vector3(orbitScale * calculateR(semiMajor, eccentricity, t) * Mathf.Cos(t), 0f,
                orbitScale * calculateR(semiMajor, eccentricity, t) * Mathf.Sin(t));
                // UpdatePlanet();
            }
            else if (threeDOrbit)
            {
                // Debug.Log(t);
                transform.position = new Vector3(
                    orbitScale * calculateR(semiMajor, eccentricity, t) * Mathf.Cos(t) * Mathf.Cos(inclinationAngle * Mathf.PI / 180),
                    orbitScale * calculateR(semiMajor, eccentricity, t) * Mathf.Cos(t) * Mathf.Sin(inclinationAngle * Mathf.PI / 180),
                    orbitScale * calculateR(semiMajor, eccentricity, t) * Mathf.Sin(t)
                );
                // UpdatePlanet();
            }
        }
        else
        {
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
        // Debug.Log("cleared");
        tr.Clear();
        tr.enabled = true;
    }

    float calculateSemiMajor(float m, float M, float r, float v)
    {
        float mu = G * (m + M);
        return (mu * r) / (2 * mu - v * v * r);
    }

    float calculateEccentricity(float m, float M, float r, float v)
    {
        float mu = G * (m + M);
        return Mathf.Sqrt(1 - r * r * v * v * (2 * mu - v * v * r) / (mu * mu * r));
    }

    float calculateR(float a, float epsilon, float theta)
    {
        return (a * (1 - Mathf.Pow(epsilon, 2f))) / (1 - epsilon * Mathf.Cos(theta));
    }

    float calculateOrbitalPeriod(float m, float M, float a)
    {
        return Mathf.Sqrt(4 * Mathf.Pow(Mathf.PI, 2f) * Mathf.Pow(a, 3f) / (G * (M + m))) / (365 * 24 * 60 * 60);
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
        return semiMajor;
    }

    public float getRadius()
    {
        return radius;
    }

    public float getOrbitalRadius()
    {
        return orbitalRadius;
    }

    public float getOrbitalVelocity()
    {
        return orbitalVelocity;
    }

    public float getInclinationAngle()
    {
        return inclinationAngle;
    }

    public void changeGameName(string newGameName)
    {
        solarSystemName = newGameName;
    }
    public void changePlanet(string newPlanet)
    {
        planet = newPlanet;
        planets[index] = planet;
        clearTrails();
    }

    public void changeMass(float newMass)
    {
        mass = newMass * 5.972e24f;
        StarScript.masses[index] = mass;
        eccentricity = calculateEccentricity(mass, starMass, orbitalRadius, orbitalVelocity);
        semiMajor = calculateSemiMajor(mass, starMass, orbitalRadius, orbitalVelocity) / 1.496e11f;
        Debug.Log($"newMass: {mass}, newEccentricity: {eccentricity}, newSemiMajor={semiMajor}");
        clearTrails();
    }

    public void changeRadius(float newRadius)
    {
        radius = newRadius;
        StarScript.radii[index] = radius;
        eccentricity = calculateEccentricity(mass, starMass, orbitalRadius, orbitalVelocity);
        semiMajor = calculateSemiMajor(mass, starMass, orbitalRadius, orbitalVelocity) / 1.496e11f;
        Debug.Log($"newMass: {mass}, newEccentricity: {eccentricity}, newSemiMajor={semiMajor}");
        clearTrails();
    }

    public void changeOrbitalRadius(float newOrbitalRadius)
    {
        orbitalRadius = newOrbitalRadius * 1.496e11f;
        StarScript.orbitalRadii[index] = orbitalRadius;
        eccentricity = calculateEccentricity(mass, starMass, orbitalRadius, orbitalVelocity);
        semiMajor = calculateSemiMajor(mass, starMass, orbitalRadius, orbitalVelocity) / 1.496e11f;
        Debug.Log($"newMass: {mass}, newEccentricity: {eccentricity}, newSemiMajor={semiMajor}");
        clearTrails();
    }

    public void changeOrbitalVelocity(float newOrbitalVelocity)
    {
        orbitalVelocity = newOrbitalVelocity;
        StarScript.orbitalVelocities[index] = orbitalVelocity;
        clearTrails();
    }

    public void changeInclinationAngle(float newInclinationAngle)
    {
        inclinationAngle = newInclinationAngle;
        StarScript.inclinationAngles[index] = inclinationAngle;
        clearTrails();
    }


}
