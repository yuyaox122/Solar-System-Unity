using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Linq;
using UnityEngine.Serialization;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] GameObject StatBar; 
    [SerializeField] TMP_Text Title;
    [SerializeField] Slider MassStat;
    [SerializeField] Slider OrbitalRadiusStat;
    [SerializeField] Slider OrbitalVelocityStat;
    [SerializeField] Transform Sun;
    [SerializeField] TextMeshProUGUI MassValue;
    [SerializeField] TextMeshProUGUI OrbitalRadiusValue;
    [SerializeField] TextMeshProUGUI OrbitalVelocityValue;
    PlanetOrbit SunScript;
    PlanetOrbit currentPlanetScript;

    // void Start()
    // {
    //     SunScript = Sun.GetComponent<PlanetOrbit>();
    //     StatBar.SetActive(false);
    //     MassStat.onValueChanged.AddListener(delegate{OnDeselectedMassInput(GetValue(MassValue.text));});
    //     MassStat.onValueChanged.AddListener((a) => {
    //         MassValue.text = "Mass / M: " + a.ToString("0.00");
    //     });
    //
    //     OrbitalRadiusStat.onValueChanged.AddListener(delegate{OnDeselectedOrbitalRadiusInput(GetValue(OrbitalRadiusValue.text));});
    //     OrbitalRadiusStat.onValueChanged.AddListener((b) => {
    //         OrbitalRadiusValue.text = "Perihelion distance / AU: " + b.ToString("0.00");
    //     });
    //     
    //     
    //     OrbitalVelocityStat.onValueChanged.AddListener(delegate{OnDeselectedOrbitalVelocityInput(GetValue(OrbitalVelocityValue.text));});
    //     OrbitalVelocityStat.onValueChanged.AddListener((b) => {
    //         OrbitalVelocityValue.text = "Perihelion velocity / km/s: " + b.ToString("0.00");
    //     });
    // }
    //
    // void Update()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hitInfo;
    //         MassStat.minValue = 0.01f;
    //         MassStat.maxValue = 500;
    //         OrbitalRadiusStat.minValue = 0.01f;
    //         OrbitalRadiusStat.maxValue = 500;
    //         if (Physics.Raycast(ray, out hitInfo)) {
    //             // Debug.Log(hitInfo.collider.gameObject.name);
    //             if (hitInfo.collider.GetComponent<PlanetOrbit>()) {
    //                 StatBar.SetActive(true);
    //                 currentPlanetScript = hitInfo.collider.GetComponent<PlanetOrbit>();
    //                 if (currentPlanetScript.planet != "Sun") {
    //                     string PlanetName = currentPlanetScript.getPlanet();
    //                     Title.text = PlanetName;
    //                     float Mass = currentPlanetScript.getMass();
    //                     MassValue.text = "Mass / M: " + Mass.ToString();
    //                     MassStat.value = Mass;
    //                     float OrbitalRadius = currentPlanetScript.getOrbitalRadius();
    //                     OrbitalRadiusValue.text = "Orbital radius / AU: " + OrbitalRadius.ToString();
    //                     OrbitalRadiusStat.value = OrbitalRadius;
    //                     float OrbitalVelocity = currentPlanetScript.getOrbitalVelocity();
    //                     OrbitalVelocityValue.text = "Orbital velocity / km/s: " + OrbitalVelocity.ToString();
    //                     OrbitalVelocityStat.value = OrbitalVelocity;
    //                     string Radius = currentPlanetScript.getRadius().ToString();
    //                 }
    //             }
    //         }
    //     }
    // }

    public void PlanetSelected(string planet) {
        currentPlanetScript = GameObject.Find(planet).GetComponent<PlanetOrbit>();
        StatBar.SetActive(true);
        string PlanetName = currentPlanetScript.getPlanet();
        Title.text = PlanetName;
        string Mass = currentPlanetScript.getMass().ToString();
        MassValue.text = "Mass / M: " + Mass;
    }

    public string GetValue(string value) {
        int index = value.IndexOf(": ");
        string newValue = value.Substring(index + 2);
        return newValue;
    }
    public void OnDeselectedMassInput(string input) {
        // Debug.Log(input);
        currentPlanetScript.changeMass(float.Parse(input));
    }

    public void OnDeselectedOrbitalRadiusInput(string input) {
        currentPlanetScript.changeOrbitalRadius(float.Parse(input));
    }
    
    public void OnDeselectedOrbitalVelocityInput(string input) {
        currentPlanetScript.changeOrbitalVelocity(float.Parse(input));
    }
    
    public void Close() {
        StatBar.SetActive(false);
    }
}
