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
    public TMP_InputField PlanetName;
    public TMP_Dropdown PlanetPresetDropdown;
    
    public GameObject StatBar;
    public Slider MassSlider;
    public Slider OrbitalVelocitySlider;
    public Slider OrbitalRadiusSlider;
    public Slider InclinationAngleSlider;
    // public Slider RadiusSlider;
    public TextMeshProUGUI MassValue;
    public TextMeshProUGUI OrbitalVelocityValue;
    public TextMeshProUGUI OrbitalRadiusValue;
    public TextMeshProUGUI InclinationAngleValue;
    // public TextMeshProUGUI RadiusValue;

    public UIMenuManager MenuManager;
    public int index;

    void Start()
    {
        PlanetPresetDropdown.onValueChanged.AddListener(delegate { HandleInputData(PlanetPresetDropdown.value); });
       
        MenuManager = (UIMenuManager)FindObjectOfType(typeof(UIMenuManager));
        
        PlanetName.text = "Planet";
        PlanetName.onValueChanged.AddListener((a) =>
        {
            PlanetName.text = a.ToString();
        });
        PlanetName.onValueChanged.AddListener(delegate { OnDeselectedNameInput(PlanetName.text); });

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

    public float GetValue(string value)
    {
        int index = value.IndexOf(": ");
        string newValue = value.Substring(index + 2);
        return float.Parse(newValue);
    }


    public void HandleInputData(int val)
    {
        MenuManager.ChangePlanetPreset(index, val);
    }
    // public void OnDeselectedSolarSystemNameInput(string input)
    // {
    //     Debug.Log(true);
    //     MenuManager.ChangeSolarSystemName(index, input);
    //     Debug.Log(input);   

    // }
    public void OnDeselectedNameInput(string input)
    {
        MenuManager.ChangePlanetName(index, input);
    }
    public void OnDeselectedMassInput(float input)
    {
        MenuManager.ChangeMass(index, input);
        Debug.Log(input);
    }

    public void OnDeselectedOrbitalVelocityInput(float input)
    {
        MenuManager.ChangeOrbitalVelocity(index, input);
    }

    public void OnDeselectedOrbitalRadiusInput(float input)
    {
        MenuManager.ChangeOrbitalRadius(index, input);
    }

    public void OnDeselectedInclinationAngleInput(float input)
    {
        MenuManager.ChangeInclinationAngle(index, input);
    }

    public void Close()
    {
        StatBar.SetActive(false);
    }
}
