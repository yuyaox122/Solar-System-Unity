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
    [SerializeField] TMP_InputField MassStat;
    [SerializeField] TMP_InputField SemiMajorStat;
    [SerializeField] TMP_InputField RadiusStat;
    [SerializeField] TMP_InputField RotationalPeriodStat;
    [SerializeField] TMP_InputField GravityStat;
    [SerializeField] TMP_InputField EccentricityStat;
    [SerializeField] TMP_InputField InclinationAngleStat;
    [SerializeField] Transform Sun;
    PlanetOrbit SunScript;
    PlanetOrbit currentPlanetScript;

    void Start()
    {
        SunScript = Sun.GetComponent<PlanetOrbit>();
        StatBar.SetActive(false);
        MassStat.onEndEdit.AddListener(delegate{OnDeselectedMassInput(MassStat.text);});
        SemiMajorStat.onEndEdit.AddListener(delegate{OnDeselectedSemiMajor(SemiMajorStat.text);});
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
            if (Physics.Raycast(ray, out hitInfo)) {
                Debug.Log(hitInfo.collider.gameObject.name);
                if (hitInfo.collider.GetComponent<PlanetOrbit>()) {
                    StatBar.SetActive(true);
                    currentPlanetScript = hitInfo.collider.GetComponent<PlanetOrbit>();
                    string PlanetName = currentPlanetScript.getPlanet();
                    Title.text = PlanetName;
                    string Mass = currentPlanetScript.getMass().ToString();
                    MassStat.text = Mass;
                    string SemiMajor = currentPlanetScript.getSemiMajor().ToString();
                    SemiMajorStat.text = SemiMajor;
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

    public void PlanetSelected(string planet) {
        StatBar.SetActive(true);
        int index = Array.IndexOf(SunScript.planets, planet);
        currentPlanetScript = GameObject.Find(planet).GetComponent<PlanetOrbit>();
        string PlanetName = planet;
        Title.text = PlanetName;
        string Mass = SunScript.masses[index].ToString();
        MassStat.text = Mass;
        string SemiMajor = SunScript.semiMajor[index].ToString();
        SemiMajorStat.text = SemiMajor;
        string Radius = SunScript.radii[index].ToString();
        RadiusStat.text = Radius;
        string RotationalPeriod = SunScript.rotational_periods[index].ToString();
        RotationalPeriodStat.text = RotationalPeriod;
        string Gravity = SunScript.gravities[index].ToString();
        GravityStat.text = Gravity;
        string Eccentricity = SunScript.eccentricities[index].ToString();
        EccentricityStat.text = Eccentricity;
        string InclinationAngle = SunScript.inclination_angles[index].ToString();
        InclinationAngleStat.text = InclinationAngle;
    }
    public void OnDeselectedMassInput(string input) {
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
