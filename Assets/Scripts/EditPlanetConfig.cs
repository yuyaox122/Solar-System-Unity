using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Linq;
using SlimUI.ModernMenu;

public class EditPlanetConfig : MonoBehaviour
{
    [SerializeField] TMP_Text PlanetName;
    [SerializeField] GameObject StatBar;
    [SerializeField] Slider MassSlider;
    [SerializeField] Slider OrbitalVelocitySlider;
    [SerializeField] Slider OrbitalRadiusSlider;
    [SerializeField] Slider InclinationAngleSlider;
    // [SerializeField] Slider RadiusSlider;
    [SerializeField] TextMeshProUGUI MassValue;
    [SerializeField] TextMeshProUGUI OrbitalVelocityValue;
    [SerializeField] TextMeshProUGUI OrbitalRadiusValue;
    [SerializeField] TextMeshProUGUI InclinationAngleValue;
    // [SerializeField] TextMeshProUGUI RadiusValue;

    public UIMenuManager MenuManager;
    public int index;

    void Start()
    {
        MassSlider.onValueChanged.AddListener(delegate { OnDeselectedMassInput(GetValue(MassValue.text)); });
        MassSlider.onValueChanged.AddListener((a) =>
        {
            MassValue.text = "Mass / M: " + a.ToString("0.00");
        });

        // SemiMajorStat.onValueChanged.AddListener(delegate { OnDeselectedMassInput(GetValue(SemiMajorValue.text)); });
        // SemiMajorStat.onValueChanged.AddListener((b) =>
        // {
        //     SemiMajorValue.text = "Semi Major / AU: " + b.ToString("0.00");
        // });

        // SemiMajorStat.onEndEdit.AddListener(delegate{OnDeselectedSemiMajor(SemiMajorStat.text);});
    }

    void Update()
    {
        // MassStat.minValue = 0.01f;
        // MassStat.maxValue = 500;
    }


    // public void PlanetSelected(string planet)
    // {
    //     currentPlanetScript = GameObject.Find(planet).GetComponent<PlanetOrbit>();
    //     StatBar.SetActive(true);
    //     string PlanetName = currentPlanetScript.getPlanet();
    //     Title.text = PlanetName;
    //     string Mass = currentPlanetScript.getMass().ToString();
    //     MassValue.text = "Mass / M: " + Mass;
    //     string SemiMajor = currentPlanetScript.getSemiMajor().ToString();
    //     SemiMajorValue.text = "Semi Major / AU: " + SemiMajor;
    //     string Radius = currentPlanetScript.getRadius().ToString();
    //     RadiusStat.text = Radius;
    //     string RotationalPeriod = currentPlanetScript.getRotationalPeriod().ToString();
    //     RotationalPeriodStat.text = RotationalPeriod;
    //     string Gravity = currentPlanetScript.getGravity().ToString();
    //     GravityStat.text = Gravity;
    //     string Eccentricity = currentPlanetScript.getEccentricity().ToString();
    //     EccentricityStat.text = Eccentricity;
    //     string InclinationAngle = currentPlanetScript.getInclinationAngle().ToString();
    //     InclinationAngleStat.text = InclinationAngle;
    // }

    public string GetValue(string value)
    {
        int index = value.IndexOf(": ");
        string newValue = value.Substring(index + 2);
        return newValue;
    }
    public void OnDeselectedMassInput(string input)
    {
        // Debug.Log(input);
       
    }

    // public void OnDeselectedSemiMajor(string input)
    // {
    //     currentPlanetScript.changeSemiMajor(float.Parse(input));
    // }

    // public void OnDeselectedRadius(string input)
    // {
    //     currentPlanetScript.changeRadius(float.Parse(input));
    // }

    // public void OnDeselectedRotationalPeriod(string input)
    // {
    //     currentPlanetScript.changeRotationalPeriod(float.Parse(input));
    // }

    // public void OnDeselectedGravity(string input)
    // {
    //     currentPlanetScript.changeGravity(float.Parse(input));
    // }

    // public void OnDeselectedEccentricity(string input)
    // {
    //     currentPlanetScript.changeEccentricity(float.Parse(input));
    // }

    // public void OnDeselectedInclinationAngle(string input)
    // {
    //     currentPlanetScript.changeInclinationAngle(float.Parse(input));
    // }
    public void Close()
    {
        StatBar.SetActive(false);
    }
}
