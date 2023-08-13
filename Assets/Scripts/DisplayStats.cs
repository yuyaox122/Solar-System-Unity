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
    [SerializeField] Slider OrbitalVelocitySlider;
    [SerializeField] Slider InclinationAngleSlider;
    [SerializeField] Transform Sun;
    [SerializeField] TextMeshProUGUI MassValue;
    [SerializeField] TextMeshProUGUI OrbitalRadiusValue;
    [SerializeField] TextMeshProUGUI OrbitalVelocityValue;
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


        OrbitalVelocitySlider.onValueChanged.AddListener((b) =>
        {
            OrbitalVelocityValue.text = "Orbital Velocity / (km/s): " + b.ToString("0.00");
        });
        OrbitalVelocitySlider.onValueChanged.AddListener(delegate { OnDeselectedOrbitalVelocityInput(GetValue(OrbitalVelocityValue.text)); });


        InclinationAngleSlider.onValueChanged.AddListener((d) =>
        {
            InclinationAngleValue.text = "Inclination Angle / (°): " + d.ToString("0");
        });
        InclinationAngleSlider.onValueChanged.AddListener(delegate { OnDeselectedInclinationAngleInput(GetValue(InclinationAngleValue.text)); });

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            MassSlider.minValue = 0.05f;
            MassSlider.maxValue = 500;

            OrbitalRadiusSlider.minValue = 0.5f;
            OrbitalRadiusSlider.maxValue = 50;

            OrbitalVelocitySlider.minValue = 5f;
            OrbitalVelocitySlider.maxValue = 100f;

            InclinationAngleSlider.minValue = -180f;
            InclinationAngleSlider.maxValue = 180f;

            if (Physics.Raycast(ray, out hitInfo))
            {
                // Debug.Log(hitInfo.collider.gameObject.name);
                if (hitInfo.collider.GetComponent<PlanetOrbit>())
                {
                    StatBar.SetActive(true);
                    currentPlanetScript = hitInfo.collider.GetComponent<PlanetOrbit>();
                    if (currentPlanetScript.planet != "Sun")
                    {
                        string PlanetName = currentPlanetScript.getPlanet();
                        Title.text = PlanetName;

                        float Mass = currentPlanetScript.getMass();
                        MassValue.text = "Mass / M: " + Mass;
                        MassSlider.value = Mass;

                        float OrbitalRadius = currentPlanetScript.getOrbitalRadius();
                        OrbitalRadiusValue.text = "Orbital radius / AU: " + OrbitalRadius;
                        OrbitalRadiusSlider.value = OrbitalRadius;

                        float OrbitalVelocity = currentPlanetScript.getOrbitalVelocity();
                        OrbitalVelocityValue.text = "Orbital velocity / km/s: " + OrbitalVelocity;
                        OrbitalVelocitySlider.value = OrbitalVelocity;

                        float InclinationAngle = currentPlanetScript.getOrbitalVelocity();
                        InclinationAngleValue.text = "Orbital velocity / km/s: " + OrbitalVelocity;
                        InclinationAngleSlider.value = OrbitalVelocity;

                        string Radius = currentPlanetScript.getRadius().ToString();
                    }
                }
            }
        }
    }

    public void PlanetSelected(string planet)
    {
        currentPlanetScript = GameObject.Find(planet).GetComponent<PlanetOrbit>();
        StatBar.SetActive(true);
        string PlanetName = currentPlanetScript.getPlanet();
        Title.text = PlanetName;
        float Mass = currentPlanetScript.getMass();
        MassValue.text = "Mass / M: " + Mass.ToString();
        MassSlider.value = Mass;
        float OrbitalRadius = currentPlanetScript.getOrbitalRadius();
        OrbitalRadiusValue.text = "Orbital Radius / (AU): " + OrbitalRadius.ToString();
        OrbitalRadiusSlider.value = OrbitalRadius;
        float OrbitalVelocity = currentPlanetScript.getOrbitalVelocity();
        OrbitalVelocityValue.text = "Orbital Velocity / (km/s): " + OrbitalVelocity.ToString();
        OrbitalVelocitySlider.value = OrbitalVelocity;
        float InclinationAngle = currentPlanetScript.getInclinationAngle();
        InclinationAngleValue.text = "Inclination Angle / (°): " + InclinationAngle.ToString();
        InclinationAngleSlider.value = InclinationAngle;
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
        currentPlanetScript.changeMass(float.Parse(input));
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
