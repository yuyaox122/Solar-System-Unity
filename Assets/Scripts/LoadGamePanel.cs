using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGamePanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    string gameName;
    public void SetupGamePanel(string gameName) {
        this.gameName = gameName;
        Title.text = gameName;
    }

    public void PlayButtonClicked() {
        PlayerPrefs.SetString("!ActiveSolarSystem!", gameName);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("!Explore!", "0");
        FindObjectOfType<LevelManager>().LoadLevel(1);
    }

    public void ExploreButtonClicked() {
        PlayerPrefs.SetString("!ActiveSolarSystem!", gameName);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("!Explore!", "1");
        FindObjectOfType<LevelManager>().LoadLevel(2);
    }
}
