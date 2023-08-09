using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlanetPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    Camera MainCamera;
    string planet;

    public void Start() {
        MainCamera = Camera.main;
    }
    public void SetupPlanetPanel(string planet) {
        this.planet = planet;
        Title.text = planet;
    }

    public void EditButtonClicked() {
        FindObjectOfType<DisplayStats>().PlanetSelected(planet);
    }

    public void FollowButtonClicked() {
        MainCamera.GetComponent<FollowPlanet>().GoToPlanet(GameObject.Find(planet).transform);
    }
}
