using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System;
using System.Collections.Generic;

namespace SlimUI.ModernMenu
{

	public class UIMenuManager : MonoBehaviour
	{
		public List<string> TheSolarSystemPlanets = new List<string> { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
		public List<float> TheSolarSystemMasses = new List<float> { 0.0553f, 0.815f, 1.000f, 0.107f, 317.83f, 95.16f, 14.54f, 17.15f, 0.0022f }; // Earth masses
		public List<float> TheSolarSystemRadii = new List<float> { 0.383f, 0.949f, 1.000f, 0.533f, 11.209f, 9.449f, 4.007f, 3.883f, 0.187f }; // Earth radii
		public List<float> TheSolarSystemOrbitalRadii = new List<float> { 0.307f, 0.718f, 0.983f, 1.381f, 4.951f, 9.075f, 18.267f, 29.887f, 29.646f };
		public List<float> TheSolarSystemOrbitalVelocities = new List<float> { 58.97f, 35.26f, 30.29f, 26.50f, 13.72f, 10.14f, 7.13f, 5.47f, 6.10f };
		public List<float> TheSolarSystemInclinationAngles = new List<float> { 7.00f, 3.39f, 0.00f, 1.85f, 1.31f, 2.49f, 0.77f, 1.77f, 17.5f };
		public List<string> TheSolarSystemPlanetPresets = new List<string> { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
		public List<string> presets = new List<string> { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
		public List<int> indexes = new List<int>();
		// {
		// 	{new Color (0.639f, 0.416f, 0.078f), new Color (0.839f, 0.616f, 0.278f)},
		// 	{new Color (0.678f, 0.329f, 0.000f), new Color (0.878f, 0.529f, 0.176f)},
		// 	{new Color (0.000f, 0.278f, 0.522f), new Color (0.157f, 0.478f, 0.722f)},
		// 	{new Color (0.557f, 0.067f, 0.000f), new Color (0.757f, 0.267f, 0.055f)},
		// 	{new Color (0.588f, 0.365f, 0.024f), new Color (0.788f, 0.565f, 0.224f)},
		// 	{new Color (0.443f, 0.408f, 0.247f), new Color (0.643f, 0.608f, 0.447f)},
		// 	{new Color (0.376f, 0.522f, 0.545f), new Color (0.576f, 0.722f, 0.745f)},
		// 	{new Color (0.127f, 0.179f, 0.429f), new Color (0.247f, 0.329f, 0.729f)},
		// 	{new Color (0.761f, 0.643f, 0.576f), new Color (0.961f, 0.843f, 0.776f)},
		// };
		private Animator CameraObject;

		// campaign button sub menu

		[Header("MENUS")]
		[Tooltip("The Menu for when the MAIN menu buttons")]
		public GameObject mainMenu;
		[Tooltip("THe first list of buttons")]
		public GameObject firstMenu;
		[Tooltip("The Menu for when the PLAY button is clicked")]
		public GameObject playMenu;
		[Tooltip("The Menu for when the EXIT button is clicked")]
		public GameObject exitMenu;
		[Tooltip("Optional 4th Menu")]
		public GameObject extrasMenu;

		public enum Theme { custom1, custom2, custom3 };
		[Header("THEME SETTINGS")]
		public Theme theme;
		private int themeIndex;
		public ThemedUIData themeController;

		[Header("PANELS")]
		[Tooltip("The UI Panel parenting all sub menus")]
		public GameObject mainCanvas;
		[Tooltip("The UI Panel that holds the CONTROLS window tab")]
		public GameObject PanelControls;
		[Tooltip("The UI Panel that holds the VIDEO window tab")]
		public GameObject PanelVideo;
		[Tooltip("The UI Panel that holds the GAME window tab")]
		public GameObject PanelGame;
		// [Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
		// public GameObject PanelKeyBindings;
		// [Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
		// public GameObject PanelMovement;
		// [Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
		// public GameObject PanelCombat;
		// [Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
		public GameObject PanelGeneral;
		public GameObject NewGame;
		public GameObject LoadGame;
		public GameObject LoadedGamesList;
		public GameObject LoadedGame;
		public GameObject PlanetsList;
		public GameObject PlanetConfig;
		public GameObject EditMenu;
   		public TMP_InputField SolarSystemName;	

		// highlights in settings screen
		[Header("SETTINGS SCREEN")]
		[Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
		public GameObject lineGame;
		[Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
		public GameObject lineVideo;
		[Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
		public GameObject lineControls;
		[Tooltip("Highlight Image for when KEY BINDINGS Tab is selected in Settings")]
		public GameObject lineKeyBindings;
		[Tooltip("Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS")]
		public GameObject lineMovement;
		[Tooltip("Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS")]
		public GameObject lineCombat;
		[Tooltip("Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS")]
		public GameObject lineGeneral;

		[Header("LOADING SCREEN")]
		[Tooltip("If this is true, the loaded scene won't load until receiving user input")]
		public bool waitForInput = true;
		public GameObject loadingMenu;
		[Tooltip("The loading bar Slider UI element in the Loading Screen")]
		public Slider loadingBar;
		public TMP_Text loadPromptText;
		public KeyCode userPromptKey;

		[Header("SFX")]
		[Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
		public AudioSource hoverSound;
		[Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
		public AudioSource sliderSound;
		[Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
		public AudioSource swooshSound;

		public SolarSystem newGame;


		void Start()
		{
			CameraObject = transform.GetComponent<Animator>();
			playMenu.SetActive(false);
			exitMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			firstMenu.SetActive(true);
			mainMenu.SetActive(true);
			NewGame.SetActive(false);
			EditMenu.SetActive(false);
			LoadGame.SetActive(false);
			SetThemeColors();
			
			PlayerPrefs.SetString("!ActiveSolarSystem!", "");
        	PlayerPrefs.SetString("!Explore!", "");
			if (!PlayerPrefs.HasKey("!DefaultSolarSystem!"))
			{
				Debug.Log(PlayerPrefs.GetString("!SolarSystemNames!"));
				CreateDefaultSolarSystem();
			}
		}

		public void DeleteSolarSystemConfigs() {
			PlayerPrefs.DeleteAll();
			CreateDefaultSolarSystem();
			LoadGameMenu();
		}

		void CreateDefaultSolarSystem() {
			PlayerPrefs.SetString("!DefaultSolarSystem!", "1");
				SaveAndLoad.SaveSolarSystem(new SolarSystem("The Solar System", TheSolarSystemPlanets, TheSolarSystemMasses, TheSolarSystemRadii, TheSolarSystemOrbitalRadii,
				TheSolarSystemOrbitalVelocities, TheSolarSystemInclinationAngles, TheSolarSystemPlanetPresets));
				// Debug.Log($"[{string.Join(",", TheSolarSystemPlanets)}]");
				// Debug.Log($"[{string.Join(",", TheSolarSystemPlanetPresets)}]");
		}

		void SetThemeColors()
		{
			switch (theme)
			{
				case Theme.custom1:
					themeController.currentColor = themeController.custom1.graphic1;
					themeController.textColor = themeController.custom1.text1;
					themeIndex = 0;
					break;
				case Theme.custom2:
					themeController.currentColor = themeController.custom2.graphic2;
					themeController.textColor = themeController.custom2.text2;
					themeIndex = 1;
					break;
				case Theme.custom3:
					themeController.currentColor = themeController.custom3.graphic3;
					themeController.textColor = themeController.custom3.text3;
					themeIndex = 2;
					break;
				default:
					Debug.Log("Invalid theme selected.");
					break;
			}
		}

		public void PlayCampaign()
		{
			exitMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			playMenu.SetActive(true);
			LoadGame.SetActive(false);
			EditMenu.SetActive(false);
			NewGame.SetActive(false);
			DestroyGameConfigs();
			DestroyPlanetConfigs();
			Debug.Log($"[{string.Join(",", TheSolarSystemPlanetPresets)}]");
		}

		public void PlayCampaignMobile()
		{
			exitMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			playMenu.SetActive(true);
			mainMenu.SetActive(false);
			EditMenu.SetActive(false);
		}

		public void ReturnMenu()
		{
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			exitMenu.SetActive(false);
			mainMenu.SetActive(true);
			LoadGame.SetActive(false);
			NewGame.SetActive(false);
			EditMenu.SetActive(false);
		}

		// public void LoadScene(string scene){
		// 	if(scene != ""){
		// 		StartCoroutine(LoadAsynchronously(scene));
		// 	}
		// }

		public void DisablePlayCampaign()
		{
			playMenu.SetActive(false);
		}

		public void Position3()
		{
			CameraObject.SetFloat("Animate", 2);
		}
		public void Position2()
		{
			DisablePlayCampaign();
			CameraObject.SetFloat("Animate", 1);
		}

		public void Position1()
		{
			CameraObject.SetFloat("Animate", 0);
		}

		void DisablePanels()
		{
			PanelControls.SetActive(false);
			PanelVideo.SetActive(false);
			PanelGame.SetActive(false);
			// PanelKeyBindings.SetActive(false);

			lineGame.SetActive(false);
			lineControls.SetActive(false);
			lineVideo.SetActive(false);
			// lineKeyBindings.SetActive(false);

			// PanelMovement.SetActive(false);
			// lineMovement.SetActive(false);
			// PanelCombat.SetActive(false);
			// lineCombat.SetActive(false);
			// PanelGeneral.SetActive(false);
			// lineGeneral.SetActive(false);
		}

		public void GamePanel()
		{
			DisablePanels();
			PanelGame.SetActive(true);
			lineGame.SetActive(true);
		}

		// public void VideoPanel()
		// {
		// 	DisablePanels();
		// 	PanelVideo.SetActive(true);
		// 	lineVideo.SetActive(true);
		// }

		public void ControlsPanel()
		{
			DisablePanels();
			PanelControls.SetActive(true);
			lineControls.SetActive(true);
		}

		// public void KeyBindingsPanel()
		// {
		// 	DisablePanels();
		// 	MovementPanel();
		// 	PanelKeyBindings.SetActive(true);
		// 	lineKeyBindings.SetActive(true);
		// }

		// public void MovementPanel()
		// {
		// 	DisablePanels();
		// 	PanelKeyBindings.SetActive(true);
		// 	PanelMovement.SetActive(true);
		// 	lineMovement.SetActive(true);
		// }

		// public void CombatPanel()
		// {
		// 	DisablePanels();
		// 	PanelKeyBindings.SetActive(true);
		// 	PanelCombat.SetActive(true);
		// 	lineCombat.SetActive(true);
		// }

		// public void GeneralPanel()
		// {
		// 	DisablePanels();
		// 	PanelKeyBindings.SetActive(true);
		// 	PanelGeneral.SetActive(true);
		// 	lineGeneral.SetActive(true);
		// }

		public void PlayHover()
		{
			hoverSound.Play();
		}

		public void PlaySFXHover()
		{
			sliderSound.Play();
		}

		public void PlaySwoosh()
		{
			swooshSound.Play();
		}

		// Are You Sure - Quit Panel Pop Up
		public void AreYouSure()
		{
			exitMenu.SetActive(true);
			if (extrasMenu) extrasMenu.SetActive(false);
			LoadGame.SetActive(false);
			NewGame.SetActive(false);
			EditMenu.SetActive(false);
			DisablePlayCampaign();
			DestroyGameConfigs();
			DestroyPlanetConfigs();
		}

		public void AreYouSureMobile()
		{
			exitMenu.SetActive(true);
			if (extrasMenu) extrasMenu.SetActive(false);
			mainMenu.SetActive(false);
			LoadGame.SetActive(false);
			EditMenu.SetActive(false);
			NewGame.SetActive(false);
			DisablePlayCampaign();
		}

		public void ExtrasMenu()
		{
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(true);
			exitMenu.SetActive(false);
			LoadGame.SetActive(false);
			NewGame.SetActive(false);
			EditMenu.SetActive(false);
			DestroyGameConfigs();
			DestroyPlanetConfigs();
		}

		public void NewGameMenu()
		{
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			exitMenu.SetActive(false);
			LoadGame.SetActive(false);
			NewGame.SetActive(true);
			EditMenu.SetActive(false);
			DestroyGameConfigs();
			DestroyPlanetConfigs();
			indexes = new List<int>();
			SolarSystemName.text = "Solar System";
			SolarSystemName.onValueChanged.AddListener((e) =>
			{
				SolarSystemName.text = e.ToString();
			});
			SolarSystemName.onValueChanged.AddListener(delegate { ChangeSolarSystemName(SolarSystemName.text); });
		}


		public void ChangeSolarSystemName(string newSolarSystemName)
		{
			newGame.name = newSolarSystemName;
		}

		public void ChangePlanetName(int index, string newName)
		{
			Debug.Log(newName);
			newGame.planets[index] = newName;
			Debug.Log($"[{string.Join(",", newGame.planets)}]");
			Debug.Log(index);
			GameObject current = PlanetsList.transform.GetChild(index + 1).gameObject;
			current.GetComponent<LoadEditPanel>().ChangeName(newName);

		}

		public void ChangePlanetPreset(int index, int val)
		{
			Debug.Log(index);
			Debug.Log(val);
			newGame.planetPresets[index] = presets[index];
		}

		public void ChangeMass(int index, float newMass)
		{
			newGame.masses[index] = newMass;
			Debug.Log($"[{string.Join(",", newGame.masses)}]");
		}

		public void ChangeOrbitalVelocity(int index, float newOrbitalVelocity)
		{
			newGame.orbitalVelocities[index] = newOrbitalVelocity;
			Debug.Log($"[{string.Join(",", newGame.orbitalVelocities)}]");
		}

		public void ChangeOrbitalRadius(int index, float newOrbitalRadius)
		{
			newGame.orbitalRadii[index] = newOrbitalRadius;
			Debug.Log($"[{string.Join(",", newGame.orbitalRadii)}]");
		}

		public void ChangeInclinationAngle(int index, float newInclinationAngle)
		{
			newGame.inclinationAngles[index] = newInclinationAngle;
			Debug.Log($"[{string.Join(",", newGame.inclinationAngles)}]");
		}

		public void DestroyPlanetConfigs()
		{
			foreach (Transform child in PlanetsList.transform)
			{
				if (child.name != "AddPlanet")
				{
					Destroy(child.gameObject);
				}
			}
			newGame = new SolarSystem("Solar System", new List<string>(), new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>(),
			 new List<string>());
		}

		public void DestroyPlanetConfig(int destroyIndex)
		{
			Debug.Log("Destroy: " + destroyIndex);
			GameObject child = PlanetsList.transform.GetChild(destroyIndex + 1).gameObject;
			newGame.planets.RemoveAt(destroyIndex);
			newGame.masses.RemoveAt(destroyIndex);
			newGame.radii.RemoveAt(destroyIndex);
			newGame.orbitalRadii.RemoveAt(destroyIndex);
			newGame.orbitalVelocities.RemoveAt(destroyIndex);
			newGame.inclinationAngles.RemoveAt(destroyIndex);
			// newGame.trailRed.RemoveAt(destroyIndex);
			// newGame.trailGreen.RemoveAt(destroyIndex);
			// newGame.trailBlue.RemoveAt(destroyIndex);
			newGame.planetPresets.RemoveAt(destroyIndex);
			Destroy(child);
			indexes.Remove(destroyIndex);
			foreach (var x in indexes)
			{
				Debug.Log(x.ToString());
			}
			for (int i = 0; i < indexes.Count; i++)
			{
				if (indexes[i] > destroyIndex)
				{
					Debug.Log("Value: " + i);
					GameObject z = PlanetsList.transform.GetChild(indexes[i] + 1).gameObject;
					z.GetComponent<LoadEditPanel>().index -= 1;
					indexes[i] = indexes[i] - 1;
				}
			}
			foreach (var x in indexes)
			{
				Debug.Log(x.ToString());
			}
			Debug.Log($"[{string.Join(",", indexes)}]");
		}
		public void AddPlanet()
		{
			GameObject planetPanel = Instantiate(PlanetConfig, PlanetsList.transform) as GameObject;
			planetPanel.GetComponent<LoadEditPanel>().MenuManager = transform.GetComponent<UIMenuManager>();
			if (indexes.Count != 0)
			{
				foreach (var x in indexes)
				{
					// Debug.Log( x.ToString());
				}
				planetPanel.GetComponent<LoadEditPanel>().SetIndex(indexes.Count);
				// Debug.Log(indexes[indexes.Count - 1]);
				indexes.Add(indexes.Count);
			}
			else
			{
				indexes.Add(0);
				planetPanel.GetComponent<LoadEditPanel>().SetIndex(0);
				// Debug.Log(indexes[0]);
			}
			string newName = "Planet";
			while (newGame.planets.Contains(newName))
			{
				int i = 0;
				bool result = int.TryParse(newName[newName.Length - 1].ToString(), out i);
				if (result)
				{
					newName = newName.Substring(0, newName.Length - 1);
					newName = newName + (i + 1).ToString();
				}
				else
				{
					newName = newName + "2";
				}
			}
			newGame.planets.Add(newName);
			planetPanel.GetComponent<LoadEditPanel>().ChangeName(newName);
			newGame.masses.Add(0.05f);
			newGame.radii.Add(1f);
			newGame.orbitalRadii.Add(0.5f);
			newGame.orbitalVelocities.Add(5f);
			newGame.inclinationAngles.Add(0f);
			// newGame.trailRed.Add(0.5f);
			// newGame.trailGreen.Add(0.5f);
			// newGame.trailBlue.Add(0.5f);
			newGame.planetPresets.Add("Mercury");
			Debug.Log($"[{string.Join(",", indexes)}]");
		}
		public void EditPlanet(int newIndex)
		{
			Debug.Log(newIndex);
			EditMenu.SetActive(true);
			EditPlanetConfig editPlanetConfig = EditMenu.GetComponent<EditPlanetConfig>();
			editPlanetConfig.index = newIndex;
			Debug.Log($"[{string.Join(",", newGame.masses)}]");
			editPlanetConfig.PlanetName.text = newGame.planets[newIndex];
			editPlanetConfig.MassSlider.value = newGame.masses[newIndex];
			editPlanetConfig.OrbitalRadiusSlider.value = newGame.radii[newIndex];
			editPlanetConfig.OrbitalVelocitySlider.value = newGame.orbitalVelocities[newIndex];
			editPlanetConfig.InclinationAngleSlider.value = newGame.inclinationAngles[newIndex];

			editPlanetConfig.MassValue.text = "Mass / (Earth M): " + newGame.masses[newIndex].ToString();
			editPlanetConfig.OrbitalRadiusValue.text = "Orbital Radius / (AU): " + newGame.orbitalRadii[newIndex].ToString();
			editPlanetConfig.OrbitalVelocityValue.text = "Orbital Velocity / (km/s): " + newGame.orbitalVelocities[newIndex].ToString();
			editPlanetConfig.InclinationAngleValue.text = "Inclination Angle / (°): " + newGame.inclinationAngles[newIndex].ToString();
			editPlanetConfig.PlanetPresetDropdown.value = 0;
			NewGame.SetActive(false);
			Debug.Log("Edit called");
			Debug.Log(newIndex);
		}

		public void CloseEditPlanet()
		{
			EditMenu.SetActive(false);
			NewGame.SetActive(true);
		}

		public void LoadGameMenu()
		{
			DestroyGameConfigs();
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			exitMenu.SetActive(false);
			LoadGame.SetActive(true);
			NewGame.SetActive(false);
			string[] names = SaveAndLoad.GetNames();
			// Debug.Log(names[0]);
			if (names.Length != 0)
			{
				foreach (string name in names)
				{
					Debug.Log("Name: " + name);
					GameObject game = Instantiate(LoadedGame, LoadedGamesList.transform) as GameObject;
					game.GetComponent<LoadGamePanel>().SetupGamePanel(name);
					// game.transform.parent = LoadedGamesList.transform;
					// game.transform.position = new Vector3(0, 0, 0);
					// game.transform.localScale = new Vector3(1, 1, 1);
					// game.transform.rotation = Quaternion.Euler();
				}
			}
		}

		public void CreateButtonClicked()
		{
			SaveAndLoad.SaveSolarSystem(newGame);
			Debug.Log(newGame.name);
			Debug.Log($"[{string.Join(",", newGame.planetPresets)}]");
			// Debug.Log(PlayerPrefs.GetString("!SolarSystemNames!"));
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			exitMenu.SetActive(false);
			LoadGame.SetActive(false);
			NewGame.SetActive(false);
			EditMenu.SetActive(false);
			DestroyGameConfigs();
			DestroyPlanetConfigs();
			PlayerPrefs.Save();

		}

		public void TestSaveAndLoad()
		{
			SolarSystem savedSolarSystem = SaveAndLoad.LoadSolarSystem("The Solar System");
			Debug.Log(savedSolarSystem.masses[0]);
			Debug.Log(savedSolarSystem.planets[0]);
			Debug.Log(savedSolarSystem.name);
		}

		public void DestroyGameConfigs()
		{
			foreach (Transform child in LoadedGamesList.transform)
			{
				Destroy(child.gameObject);
			}
		}

		public void QuitGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		// Load Bar synching animation
		IEnumerator LoadAsynchronously(string sceneName)
		{ // scene name is just the name of the current scene being loaded
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			mainCanvas.SetActive(false);
			loadingMenu.SetActive(true);

			while (!operation.isDone)
			{
				float progress = Mathf.Clamp01(operation.progress / .95f);
				loadingBar.value = progress;

				if (operation.progress >= 0.9f && waitForInput)
				{
					loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
					loadingBar.value = 1;

					if (Input.GetKeyDown(userPromptKey))
					{
						operation.allowSceneActivation = true;
					}
				}
				else if (operation.progress >= 0.9f && !waitForInput)
				{
					operation.allowSceneActivation = true;
				}

				yield return null;
			}
		}
	}
}