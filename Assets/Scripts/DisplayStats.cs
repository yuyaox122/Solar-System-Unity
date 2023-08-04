using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] GameObject StatBar; 
    [SerializeField] TMP_Text Title;
    [SerializeField] TMP_InputField MassStat;
    [SerializeField] TMP_InputField SemiMajorStat;
    [SerializeField] TMP_InputField RadiusStat;
    [SerializeField] TMP_InputField RotationalPeriodStat;
    [SerializeField] TMP_InputField OrbitalPeriodStat;
    [SerializeField] TMP_InputField GravityStat;
    [SerializeField] TMP_InputField EccentricityStat;
    [SerializeField] TMP_InputField InclinationAngleStat;
    Planet2DOrbit currentPlanetScript;

    void Start()
    {
        StatBar.SetActive(false);
        MassStat.onEndEdit.AddListener(delegate{OnDeselectedMassInput(MassStat.text);});
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo)) {
                Debug.Log(hitInfo.collider.gameObject.name);
                if (hitInfo.collider.GetComponent<Planet2DOrbit>()) {
                    StatBar.SetActive(true);
                    currentPlanetScript = hitInfo.collider.GetComponent<Planet2DOrbit>();
                    string PlanetName = currentPlanetScript.getPlanet();
                    Title.text = PlanetName;
                    string Mass = currentPlanetScript.getMass().ToString();
                    MassStat.text = Mass;
                    Debug.Log(Mass);
                }
            }
        }
    }

    public void OnDeselectedMassInput(string input) {
        Debug.Log(input);
        currentPlanetScript.changeMass(float.Parse(input));
        Debug.Log(MassStat.text);
    }

    public void Close() {
        StatBar.SetActive(false);
    }
}
