using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class EventController : MonoBehaviour
{
    float time_scale = 1f;
    [SerializeField] GameObject Sun;
    [SerializeField] TMP_InputField GameName;

    
    public void AdjustTimeScale(float newTimeScale)
    {
        time_scale = newTimeScale;
    }
    public float ReturnTimeScale()
    {
        return time_scale;
    }

    public void LoadMainMenu()
    {
        PlanetOrbit planetOrbit = Sun.GetComponent<PlanetOrbit>();
        Debug.Log("Current Name: " + planetOrbit.solarSystemName);
        SaveAndLoad.SaveSolarSystem(new SolarSystem(planetOrbit.solarSystemName, planetOrbit.planets, planetOrbit.masses, planetOrbit.semiMajor, planetOrbit.radii, planetOrbit.rotational_periods, planetOrbit.orbital_periods,
        planetOrbit.gravities, planetOrbit.eccentricities, planetOrbit.inclination_angles, planetOrbit.trail_red, planetOrbit.trail_green, planetOrbit.trail_blue));
        SaveAndLoad.DestroySolarSystem(PlayerPrefs.GetString("!ActiveSolarSystem!"));
        Debug.Log("The names: " + PlayerPrefs.GetString("!SolarSystemNames!"));
        SceneManager.LoadScene(0);
    }

    
}

[Serializable()] 
public struct SolarSystem
    {
        public string name;
        public string[] planets;
        public float[] masses;
        public float[] semiMajor;
        public float[] radii;
        public float[] rotational_periods;
        public float[] orbital_periods;
        public float[] gravities;
        public float[] eccentricities;
        public float[] inclination_angles;
        public float[] trail_red;
        public float[] trail_green;
        public float[] trail_blue;

        public SolarSystem(string name, string[] planets, float[] masses, float[] semiMajor, float[] radii, float[] rotational_periods,
        float[] orbital_periods, float[] gravities, float[] eccentricities, float[] inclination_angles, float[] trail_red, float[] trail_green, float[] trail_blue) {
            this.name = name;
            this.planets = planets;
            this.masses = masses;
            this.semiMajor = semiMajor;
            this.radii = radii;
            this.rotational_periods = rotational_periods;
            this.orbital_periods = orbital_periods;
            this.gravities = gravities;
            this.eccentricities = eccentricities;
            this.inclination_angles = inclination_angles;
            this.trail_red = trail_red;
            this.trail_green = trail_green;
            this.trail_blue = trail_blue;
        }
    }