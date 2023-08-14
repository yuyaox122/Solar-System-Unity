using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlimUI.ModernMenu;

public class LoadEditPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    public string planetName;
    public UIMenuManager MenuManager;
    public int index;

    public void SetIndex(int newIndex) {
        index = newIndex;
    }
    public void ChangeName(string planetName) {
        this.planetName = planetName;
        Title.text = planetName;
    }

    public void Destroy() {
        MenuManager.DestroyPlanetConfig(index);
    }

    public void EditButtonClicked() {
        MenuManager.EditPlanet(index);
        Debug.Log("Hello");
    }


}
