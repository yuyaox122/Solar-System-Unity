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
    [SerializeField] TMP_InputField RadiusStat;
    [SerializeField] TMP_InputField RotationalPeriodStat;
    [SerializeField] TMP_InputField GravityStat;
    [SerializeField] TMP_InputField EccentricityStat;
    [SerializeField] TMP_InputField InclinationAngleStat;
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

        SemiMajorStat.onValueChanged.AddListener(delegate{OnDeselectedMassInput(GetValue(SemiMajorValue.text));});
        SemiMajorStat.onValueChanged.AddListener((b) => {
            SemiMajorValue.text = "Semi Major / AU: " + b.ToString("0.000");
        });
       
        // SemiMajorStat.onEndEdit.AddListener(delegate{OnDeselectedSemiMajor(SemiMajorStat.text);});
        RadiusStat.onEndEdit.AddListener(delegate{OnDeselectedRadius(RadiusStat.text);});
        RotationalPeriodStat.onEndEdit.AddListener(delegate{OnDeselectedRotationalPeriod(RotationalPeriodStat.text);});
        GravityStat.onEndEdit.AddListener(delegate{OnDeselectedGravity(GravityStat.text);});
        EccentricityStat.onEndEdit.AddListener(delegate{OnDeselectedEccentricity(EccentricityStat.text);});
        InclinationAngleStat.onEndEdit.AddListener(delegate{OnDeselectedInclinationAngle(InclinationAngleStat.text);});
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
                        RadiusStat.text = Radius;
                        string RotationalPeriod = currentPlanetScript.getRotationalPeriod().ToString();
                        RotationalPeriodStat.text = RotationalPeriod;
                        string Gravity = currentPlanetScript.getGravity().ToString();
                        GravityStat.text = Gravity;
                        string Eccentricity = currentPlanetScript.getEccentricity().ToString();
                        EccentricityStat.text = Eccentricity;
                        string InclinationAngle = currentPlanetScript.getInclinationAngle().ToString();
                        InclinationAngleStat.text = InclinationAngle;
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
        string Radius = currentPlanetScript.getRadius().ToString();
        RadiusStat.text = Radius;
        string RotationalPeriod = currentPlanetScript.getRotationalPeriod().ToString();
        RotationalPeriodStat.text = RotationalPeriod;
        string Gravity = currentPlanetScript.getGravity().ToString();
        GravityStat.text = Gravity;
        string Eccentricity = currentPlanetScript.getEccentricity().ToString();
        EccentricityStat.text = Eccentricity;
        string InclinationAngle = currentPlanetScript.getInclinationAngle().ToString();
        InclinationAngleStat.text = InclinationAngle;
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

    public void OnDeselectedSemiMajor(string input) {
        currentPlanetScript.changeSemiMajor(float.Parse(input));
    }

    public void OnDeselectedRadius(string input) {
        currentPlanetScript.changeRadius(float.Parse(input));
    }

    public void OnDeselectedRotationalPeriod(string input) {
        currentPlanetScript.changeRotationalPeriod(float.Parse(input));
    }

    public void OnDeselectedGravity(string input) {
        currentPlanetScript.changeGravity(float.Parse(input));
    }

    public void OnDeselectedEccentricity(string input) {
        currentPlanetScript.changeEccentricity(float.Parse(input));
    }

    public void OnDeselectedInclinationAngle(string input) {
        currentPlanetScript.changeInclinationAngle(float.Parse(input));
    }
    public void Close() {
        StatBar.SetActive(false);
    }
}
