using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEditPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    string planetName;
    public UIMenuManager MenuManager;
    public void ChangeName(string planetName) {
        this.planetName = planetName;
        Title.text = planetName;
    }

    public void EditButtonClicked() {
        FindObjectOfType<UIMenuManager>().EditPlanet();
    }
}
