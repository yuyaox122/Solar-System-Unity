using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] GameObject StatBar; 
    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] TMPro.TextMeshProUGUI MassStat;
    [SerializeField] TMPro.TextMeshProUGUI SemiMajorStat;
    [SerializeField] TMPro.TextMeshProUGUI RadiusStat;
    [SerializeField] TMPro.TextMeshProUGUI RotationalPeriodStat;
    [SerializeField] TMPro.TextMeshProUGUI OrbitalPeriodStat;
    [SerializeField] TMPro.TextMeshProUGUI GravityStat;
    [SerializeField] TMPro.TextMeshProUGUI EccentricityStat;
    [SerializeField] TMPro.TextMeshProUGUI InclinationAngleStat;

    void Start()
    {
        StatBar.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo)) {
                if (hitInfo.collider.GetComponent<Planet2DOrbit>()) {
                    StatBar.SetActive(true);
                    Planet2DOrbit PlanetScript = hitInfo.collider.GetComponent<Planet2DOrbit>();
                    string PlanetName = PlanetScript.getPlanet();
                    Title.text = PlanetName;
                    string Mass = PlanetScript.getMass().ToString();
                    MassStat.text = Mass;
                }
            }
        }
    }

    public void Close() {
        StatBar.SetActive(false);
    }
}
