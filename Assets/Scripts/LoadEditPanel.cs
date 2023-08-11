using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlimUI.ModernMenu;

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
        MenuManager.EditPlanet();
        Debug.Log("Hello");
    }

    public void Close() {
        MenuManager.CloseEditPlanet();
    }
}
    