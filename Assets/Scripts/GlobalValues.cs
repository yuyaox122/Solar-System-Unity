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
using SpaceGraphicsToolkit;

public class EventController : MonoBehaviour
{
    float timeScale = 1f;
    [SerializeField] GameObject Sun;
    [SerializeField] TMP_InputField GameName;
    string explore;
    public GameObject SolarSystemGameObject;
    float radiusScale;
    void Start()
    {
        Debug.Log("Start");
        explore = PlayerPrefs.GetString("!Explore!");
        SpawnPlanets();
        Debug.Log("Spawned");
    }

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
            planetOrbit.inclinationAngles, planetOrbit.trailRed, planetOrbit.trailGreen, planetOrbit.trailBlue, planetOrbit.planetPresets));
        SaveAndLoad.DestroySolarSystem(PlayerPrefs.GetString("!ActiveSolarSystem!"));
        Debug.Log("The names: " + PlayerPrefs.GetString("!SolarSystemNames!"));
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void SpawnPlanets()
    {
        string solarSystemName = PlayerPrefs.GetString("!ActiveSolarSystem!");
        SolarSystem currentSolarSystem = SaveAndLoad.LoadSolarSystem(solarSystemName);
        List<string> planets = currentSolarSystem.planets;
        List<float> masses = currentSolarSystem.masses; // Earth masses
        List<float> radii = currentSolarSystem.radii; // Earth radii
        List<float> orbitalRadii = currentSolarSystem.orbitalRadii; // AU
        List<float> orbitalVelocities = currentSolarSystem.orbitalVelocities; // km/s
        List<float> inclinationAngles = currentSolarSystem.inclinationAngles;
        List<float> trailRed = currentSolarSystem.trailRed;
        List<float> trailGreen = currentSolarSystem.trailGreen;
        List<float> trailBlue = currentSolarSystem.trailBlue;
        List<string> planetPresets = currentSolarSystem.planetPresets;
        Debug.Log($"[{string.Join(",", planetPresets)}]");
        Debug.Log($"[{string.Join(",", planets)}]");

        if (explore == "1")
        {
            for (int i = 0; i < planetPresets.Count; i++)
            {
                GameObject newPlanetPreset = Resources.Load<GameObject>("ExplorePresets/" + planetPresets[i]);
                Debug.Log(i);
                GameObject newPlanetObject = (GameObject)GameObject.Instantiate(newPlanetPreset, Vector3.zero, Quaternion.identity);
                newPlanetObject.transform.parent = SolarSystemGameObject.transform;
                newPlanetObject.name = planets[i];
                newPlanetObject.GetComponent<SgtFloatingOrbit>().Radius = orbitalRadii[i] * 1e+4;
                newPlanetObject.GetComponent<SgtFloatingOrbit>().ParentPoint = Sun.GetComponent<SgtFloatingObject>();
                newPlanetObject.GetComponent<SgtFloatingOrbit>().DegreesPerSecond = orbitalVelocities[i] / 100;
                newPlanetObject.GetComponent<SgtFloatingTarget>().WarpName = planets[i];
                newPlanetObject.GetComponent<SgtFloatingTarget>().WarpDistance = radii[i] * 2000;
                radiusScale = radii[i];
                newPlanetObject.transform.localScale = new Vector3(radiusScale, radiusScale, radiusScale);
            }
        }
        else
        {
            for (int i = 0; i < planetPresets.Count; i++)
            {
                // GameObject newPlanetPreset = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Plugins/CW/SpaceGraphicsToolkit/Packs/Solar System Pack/Prefabs/" + planetPresets[i], typeof(GameObject));
                GameObject newPlanetPreset = Resources.Load<GameObject>("PlanetPresets/" + planetPresets[i]);
                Debug.Log(i);
                GameObject newPlanetObject = (GameObject)GameObject.Instantiate(newPlanetPreset, Vector3.zero, Quaternion.identity);
                newPlanetObject.transform.parent = SolarSystemGameObject.transform;
                newPlanetObject.name = planets[i];
                newPlanetObject.AddComponent<PlanetOrbit>();
                PlanetOrbit PlanetOrbitScript = newPlanetObject.GetComponent<PlanetOrbit>();
                PlanetOrbitScript.planet = planets[i];
                PlanetOrbitScript.GameController = this.gameObject;
                PlanetOrbitScript.Star = Sun.transform;
            }
        }
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
    public List<string> planetPresets;

    public SolarSystem(string name, List<string> planets, List<float> masses, List<float> radii, List<float> orbitalRadii,
    List<float> orbitalVelocities, List<float> inclinationAngles, List<float> trailRed, List<float> trailGreen, List<float> trailBlue, List<string> planetPresets)
    {
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
        this.planetPresets = planetPresets;
    }
}