using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class EventController : MonoBehaviour
{
    float time_scale = 1f;
    [SerializeField] GameObject Sun;
    [SerializeField] TMP_InputField GameName;

    struct SolarSystem
    {
        public string[] planets;
        public float[] masses;
        public float[] semiMajor;
        public float[] radii;
        public float[] rotational_periods;
        public float[] orbital_periods;
        public float[] gravities;
        public float[] eccentricities;
        public float[] inclination_angles;
        public Color[,] trail_colours;

        public SolarSystem(string[] planets, float[] masses, float[] semiMajor, float[] radii, float[] rotational_periods,
        float[] orbital_periods, float[] gravities, float[] eccentricities, float[] inclination_angles, Color[,] trail_colours) {
            this.planets = planets;
            this.masses = masses;
            this.semiMajor = semiMajor;
            this.radii = radii;
            this.rotational_periods = rotational_periods;
            this.orbital_periods = orbital_periods;
            this.gravities = gravities;
            this.eccentricities = eccentricities;
            this.inclination_angles = inclination_angles;
            this.trail_colours = trail_colours;
        }
    }
    public void AdjustTimeScale(float newTimeScale)
    {
        time_scale = newTimeScale;
    }
    public float ReturnTimeScale()
    {
        return time_scale;
    }

    public void SaveSolarSystemConfig()
    {
        PlanetOrbit PlanetScript = Sun.GetComponent<PlanetOrbit>();
        SolarSystem SolarSystemStruct = new SolarSystem();
        BinaryFormatter formatter = new BinaryFormatter();
        name = GameName.text;
        FileStream FS = new FileStream(Application.dataPath + "/Games/" + name + ".txt", FileMode.Create);
        formatter.Serialize(FS, SolarSystemStruct);
        FS.Flush();
        FS.Close();
    }

    public void LoadMainMenu()
    {
        SaveSolarSystemConfig();
        SceneManager.LoadScene(0);
    }
}
