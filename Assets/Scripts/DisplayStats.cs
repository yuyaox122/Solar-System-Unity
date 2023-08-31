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
    [SerializeField] Slider MassSlider;
    [SerializeField] Slider OrbitalRadiusSlider;
    // [SerializeField] Slider OrbitalVelocitySlider;
    [SerializeField] Slider InclinationAngleSlider;
    [SerializeField] Transform Sun;
    [SerializeField] TextMeshProUGUI MassValue;
    [SerializeField] TextMeshProUGUI OrbitalRadiusValue; 
    // [SerializeField] TextMeshProUGUI OrbitalVelocityValue;
    [SerializeField] TextMeshProUGUI InclinationAngleValue;
    PlanetOrbit SunScript;
    PlanetOrbit currentPlanetScript;

    void Start()
    {
        SunScript = Sun.GetComponent<PlanetOrbit>();
        StatBar.SetActive(false);

        MassSlider.onValueChanged.AddListener((a) =>
        {
            MassValue.text = "Mass / M: " + a.ToString("0.00");
        });
        MassSlider.onValueChanged.AddListener(delegate { OnDeselectedMassInput(GetValue(MassValue.text)); });


        OrbitalRadiusSlider.onValueChanged.AddListener((c) =>
        {
            OrbitalRadiusValue.text = "Orbital Radius / (AU): " + c.ToString("0.00");
        });
        OrbitalRadiusSlider.onValueChanged.AddListener(delegate { OnDeselectedOrbitalRadiusInput(GetValue(OrbitalRadiusValue.text)); });


        // OrbitalVelocitySlider.onValueChanged.AddListener((b) =>
        // {
        //     OrbitalVelocityValue.text = "Orbital Velocity / (km/s): " + b.ToString("0.00");
        // });
        // OrbitalVelocitySlider.onValueChanged.AddListener(delegate { OnDeselectedOrbitalVelocityInput(GetValue(OrbitalVelocityValue.text)); });


        InclinationAngleSlider.onValueChanged.AddListener((d) =>
        {
            InclinationAngleValue.text = "Inclination Angle / (°): " + d.ToString("0");
        });
        InclinationAngleSlider.onValueChanged.AddListener(delegate { OnDeselectedInclinationAngleInput(GetValue(InclinationAngleValue.text)); });

        MassSlider.minValue = 0.05f;
        MassSlider.maxValue = 500;

        float mu = currentPlanetScript.G * (currentPlanetScript.mass + currentPlanetScript.starMass);
        float vel = currentPlanetScript.orbitalVelocity;
        OrbitalRadiusSlider.minValue = mu / (vel * vel) / 1.496e11f;
        OrbitalRadiusSlider.maxValue = 2 * mu / (vel * vel) / 1.496e11f;

        InclinationAngleSlider.minValue = -180f;
        InclinationAngleSlider.maxValue = 180f;
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log(hitInfo.collider.gameObject.name);
                if (hitInfo.collider.GetComponent<PlanetOrbit>())
                {
                    StatBar.SetActive(true);
                    currentPlanetScript = hitInfo.collider.GetComponent<PlanetOrbit>();
                    if (currentPlanetScript.planet != "Sun")
                    {
                        MassSlider.minValue = 0.05f;
                        MassSlider.maxValue = 500;

                        float mu = currentPlanetScript.G * (currentPlanetScript.mass + currentPlanetScript.starMass);
                        float vel = currentPlanetScript.orbitalVelocity;
                        OrbitalRadiusSlider.minValue = mu / (vel * vel) / 1.496e11f;
                        OrbitalRadiusSlider.maxValue = 1.8f * mu / (vel * vel) / 1.496e11f;

                        InclinationAngleSlider.minValue = -180f;
                        InclinationAngleSlider.maxValue = 180f;
                        string PlanetName = currentPlanetScript.getPlanet();
                        Title.text = PlanetName;

                        float Mass = currentPlanetScript.getMass() / 5.972e24f;
                        MassValue.text = "Mass / M: " + Mass;
                        MassSlider.value = Mass;

                        float OrbitalRadius = currentPlanetScript.getOrbitalRadius() / 1.496e11f;
                        OrbitalRadiusValue.text = "Orbital radius / AU: " + OrbitalRadius;
                        OrbitalRadiusSlider.value = OrbitalRadius;

                        // float OrbitalVelocity = currentPlanetScript.getOrbitalVelocity();
                        // OrbitalVelocityValue.text = "Orbital velocity / km/s: " + OrbitalVelocity;
                        // OrbitalVelocitySlider.value = OrbitalVelocity;

                        float InclinationAngle = currentPlanetScript.getInclinationAngle();
                        InclinationAngleValue.text = "Inclination Angle / °: " + InclinationAngle;
                        InclinationAngleSlider.value = InclinationAngle;

                        string Radius = currentPlanetScript.getRadius().ToString();
                    }
                }
            }
        }
    }

    public void PlanetSelected(string planet)
    {
        currentPlanetScript = GameObject.Find(planet).GetComponent<PlanetOrbit>();
        MassSlider.minValue = 0.05f;
        MassSlider.maxValue = 500;

        string PlanetName = currentPlanetScript.getPlanet();
        Title.text = PlanetName;
        float Mass = currentPlanetScript.getMass()  / 5.972e24f;
        MassValue.text = "Mass / M: " + Mass;
        MassSlider.value = Mass;
        float OrbitalRadius = currentPlanetScript.getOrbitalRadius() / 1.496e11f;
        OrbitalRadiusValue.text = "Orbital Radius / (AU): " + OrbitalRadius;
        OrbitalRadiusSlider.value = OrbitalRadius;
        float InclinationAngle = currentPlanetScript.getInclinationAngle();
        InclinationAngleValue.text = "Inclination Angle / (°): " + InclinationAngle;
        InclinationAngleSlider.value = InclinationAngle;

        float mu = currentPlanetScript.G * (currentPlanetScript.mass + currentPlanetScript.starMass);
        float vel = currentPlanetScript.orbitalVelocity;
        // Debug.Log("Velocity: " + vel);
        // Debug.Log("Mu: " + mu);
        Debug.Log("Current Orbital Radius: " + currentPlanetScript.getOrbitalRadius() / 1.496e11f);
        OrbitalRadiusSlider.minValue = mu / (vel * vel) / 1.496e11f;
        Debug.Log("Current Orbital Radius: " + currentPlanetScript.getOrbitalRadius() / 1.496e11f);
        Debug.Log(OrbitalRadiusSlider.value);
        OrbitalRadiusSlider.maxValue = 1.8f * mu / (vel * vel) / 1.496e11f;
        Debug.Log(OrbitalRadiusSlider.minValue);  
        Debug.Log(OrbitalRadiusSlider.maxValue);
        Debug.Log(currentPlanetScript.getOrbitalRadius() / 1.496e11f);
        InclinationAngleSlider.minValue = -180f;
        InclinationAngleSlider.maxValue = 180f;
        
        StatBar.SetActive(true);
    }

    public string GetValue(string value)
    {
        int index = value.IndexOf(": ");
        string newValue = value.Substring(index + 2);
        return newValue;
    }
    public void OnDeselectedMassInput(string input)
    {
        // Debug.Log(input);
        currentPlanetScript.
            changeMass(float.Parse(input));
    }

    public void OnDeselectedOrbitalRadiusInput(string input)
    {
        currentPlanetScript.changeOrbitalRadius(float.Parse(input));
    }

    public void OnDeselectedOrbitalVelocityInput(string input)
    {
        currentPlanetScript.changeOrbitalVelocity(float.Parse(input));
    }

    public void OnDeselectedInclinationAngleInput(string input)
    {
        currentPlanetScript.changeInclinationAngle(float.Parse(input));
    }

    public void Close()
    {
        StatBar.SetActive(false);
    }
}
