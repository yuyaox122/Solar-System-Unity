using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class LoadPlanetPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] TMPro.TextMeshProUGUI FollowText;
    Camera MainCamera;
    string planet;
    bool following;
    FollowPlanet follow;
    Vector3 savedCameraPos;

    public void Start() {
        MainCamera = Camera.main;
        following = false;
        follow = MainCamera.GetComponent<FollowPlanet>();
    }
    
    public void SetupPlanetPanel(string planet) {
        this.planet = planet;
        Title.text = planet;
        FollowText.text = "Follow";

    }

    public void EditButtonClicked() {
        FindObjectOfType<DisplayStats>().PlanetSelected(planet);
    }

    public void FollowButtonClicked() {
        if (!following && follow.activePlanet != planet)
        {
            savedCameraPos = MainCamera.transform.position;
            following = true;
            follow.activePlanet = planet;
            FollowText.text = "Following";
            follow.GoToPlanet(GameObject.Find(planet).transform);
        }
        else
        {
            follow.active = false;
            following = false;
            follow.activePlanet = "";
            FollowText.text = "Follow";
            MainCamera.transform.position = savedCameraPos;
        }
    }

    public void Update()
    {
        if (follow.activePlanet != planet)
        {
            following = false;
            FollowText.text = "Follow";
        }
    }
}
