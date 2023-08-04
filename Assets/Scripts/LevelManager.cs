using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(int SceneBuildIndex) {
        SceneManager.LoadScene(SceneBuildIndex);
    }
}
