using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeGameName : MonoBehaviour
{
    [SerializeField] TMP_InputField Title;
    [SerializeField] GameObject Sun;
    PlanetOrbit PlanetScript;
    bool isEditing;
    void Start()
    {
        PlanetScript = Sun.GetComponent<PlanetOrbit>();
        // Title.text = PlanetScript.solarSystemName;
        Title.text = SaveAndLoad.LoadSolarSystem(PlayerPrefs.GetString("!ActiveSolarSystem!")).name;
        // Debug.Log(PlanetScript.solarSystemName);
        Title.onEndEdit.AddListener(delegate{OnDeselectedGameName(Title.text);});
        // Title.onValueChanged.AddListener(delegate{Editing();});
        // isEditing = false;
    }

    // Update is called once per frame
    void OnDeselectedGameName(string gameName)
    {
        // if (isEditing) {
            
        // }
        PlanetScript.changeGameName(gameName);
        // Debug.Log(PlanetScript.solarSystemName);
    }

    // void Editing() {
    //     isEditing = true;
    // }
}
