using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlanetMenu : MonoBehaviour
{
    [SerializeField] GameObject OpenPlanetsButton;
    [SerializeField] Transform Sun;
    [SerializeField] GameObject PlanetsList;
    [SerializeField] GameObject Planet;
    [SerializeField] GameObject PlanetPanel;

    public void Start() {
        PlanetPanel.SetActive(false);
    }
    public void LoadPlanetMenu()
    {
        PlanetPanel.SetActive(true);
        PlanetOrbit SunScript = Sun.GetComponent<PlanetOrbit>();
        foreach (Transform child in PlanetsList.transform)
        {
            Destroy(child.gameObject);
        }
        OpenPlanetsButton.SetActive(false);
        foreach (string planetName in SunScript.planets)
        {
            GameObject planetPanel = Instantiate(Planet, PlanetsList.transform) as GameObject;
            planetPanel.GetComponent<LoadPlanetPanel>().SetupPlanetPanel(planetName);
        }
    }

    public void Close() {
        PlanetPanel.SetActive(false);
        OpenPlanetsButton.SetActive(true);
    }
}

