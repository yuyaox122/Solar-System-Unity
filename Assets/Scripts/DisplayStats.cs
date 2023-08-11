using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Linq;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] GameObject StatBar; 
    [SerializeField] TMP_Text Title;
    [SerializeField] Slider MassStat;
    [SerializeField] Slider SemiMajorStat;
    [SerializeField] Transform Sun;
    [SerializeField] TextMeshProUGUI MassValue;
    [SerializeField] TextMeshProUGUI SemiMajorValue;
    PlanetOrbit SunScript;
    PlanetOrbit currentPlanetScript;

    void Start()
    {
        SunScript = Sun.GetComponent<PlanetOrbit>();
        StatBar.SetActive(false);
        MassStat.onValueChanged.AddListener(delegate{OnDeselectedMassInput(GetValue(MassValue.text));});
        MassStat.onValueChanged.AddListener((a) => {
            MassValue.text = "Mass / M: " + a.ToString("0.000");
        });

        SemiMajorStat.onValueChanged.AddListener(delegate{OnDeselectedSemiMajorInput(GetValue(SemiMajorValue.text));});
        SemiMajorStat.onValueChanged.AddListener((b) => {
            SemiMajorValue.text = "Semi Major / AU: " + b.ToString("0.000");
        });
       
        // SemiMajorStat.onEndEdit.AddListener(delegate{OnDeselectedSemiMajor(SemiMajorStat.text);});
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            MassStat.minValue = 0.01f;
            MassStat.maxValue = 500;
            SemiMajorStat.minValue = 0.01f;
            SemiMajorStat.maxValue = 500;
            if (Physics.Raycast(ray, out hitInfo)) {
                // Debug.Log(hitInfo.collider.gameObject.name);
                if (hitInfo.collider.GetComponent<PlanetOrbit>()) {
                    StatBar.SetActive(true);
                    currentPlanetScript = hitInfo.collider.GetComponent<PlanetOrbit>();
                    if (currentPlanetScript.planet != "Sun") {
                        string PlanetName = currentPlanetScript.getPlanet();
                        Title.text = PlanetName;
                        float Mass = currentPlanetScript.getMass();
                        MassValue.text = "Mass / M: " + Mass.ToString();
                        MassStat.value = Mass;
                        float SemiMajor = currentPlanetScript.getSemiMajor();
                        SemiMajorValue.text = "Semi Major / AU: " + SemiMajor.ToString();;
                        SemiMajorStat.value = SemiMajor;
                        string Radius = currentPlanetScript.getRadius().ToString();
                    }
                }
            }
        }
    }

    public void PlanetSelected(string planet) {
        currentPlanetScript = GameObject.Find(planet).GetComponent<PlanetOrbit>();
        StatBar.SetActive(true);
        string PlanetName = currentPlanetScript.getPlanet();
        Title.text = PlanetName;
        string Mass = currentPlanetScript.getMass().ToString();
        MassValue.text = "Mass / M: " + Mass;
        // string SemiMajor = currentPlanetScript.getSemiMajor().ToString();
        // SemiMajorValue.text = SemiMajor;
        string SemiMajor = currentPlanetScript.getSemiMajor().ToString();
        SemiMajorValue.text = "Semi Major / AU: " + SemiMajor;
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

    public void OnDeselectedSemiMajorInput(string input) {
        currentPlanetScript.changeSemiMajor(float.Parse(input));
    }
    public void Close() {
        StatBar.SetActive(false);
    }
}
