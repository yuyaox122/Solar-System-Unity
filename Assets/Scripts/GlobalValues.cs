using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.Serialization;

public class EventController : MonoBehaviour
{
    float timeScale = 1f;
    [SerializeField] GameObject Sun;
    [SerializeField] TMP_InputField GameName;

    
    public void AdjustTimeScale(float newTimeScale)
    {
        timeScale = newTimeScale;
    }
    public float ReturnTimeScale()
    {
        return timeScale;
    }

    public void LoadMainMenu()
    {
        PlanetOrbit planetOrbit = Sun.GetComponent<PlanetOrbit>();
        Debug.Log("Current Name: " + planetOrbit.solarSystemName);
        SaveAndLoad.SaveSolarSystem(new SolarSystem(planetOrbit.solarSystemName, planetOrbit.planets, planetOrbit.masses, planetOrbit.radii, planetOrbit.orbitalRadii, planetOrbit.orbitalVelocities, 
            planetOrbit.inclinationAngles, planetOrbit.trailRed, planetOrbit.trailGreen, planetOrbit.trailBlue));
        SaveAndLoad.DestroySolarSystem(PlayerPrefs.GetString("!ActiveSolarSystem!"));
        Debug.Log("The names: " + PlayerPrefs.GetString("!SolarSystemNames!"));
        SceneManager.LoadScene(0);
    }

    
}

[Serializable()] 
public struct SolarSystem
    {
        public string name;
        public List<string> planets;
        public List<float> masses;
        public List<float> radii;
        public List<float> orbitalRadii;
        public List<float> orbitalVelocities;
        public List<float> inclinationAngles;
        public List<float> trailRed;
        public List<float> trailGreen;
        public List<float> trailBlue;

        public SolarSystem(string name, List<string> planets, List<float> masses, List<float> radii, List<float> orbitalRadii,
        List<float> orbitalVelocities, List<float> inclinationAngles, List<float> trailRed, List<float> trailGreen, List<float> trailBlue) {
            this.name = name;
            this.planets = planets;
            this.masses = masses;
            this.radii = radii;
            this.orbitalRadii = orbitalRadii;
            this.orbitalVelocities = orbitalVelocities;
            this.inclinationAngles = inclinationAngles;
            this.trailRed = trailRed;
            this.trailGreen = trailGreen;
            this.trailBlue = trailBlue;
        }
    }