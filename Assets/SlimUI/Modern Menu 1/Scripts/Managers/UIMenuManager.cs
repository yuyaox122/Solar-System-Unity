using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

namespace SlimUI.ModernMenu
{

	public class UIMenuManager : MonoBehaviour
	{
		public string[] planets = { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };
		public float[] masses = new float[] { 0.055f, 0.815f, 1.000f, 0.107f, 317.85f, 95.159f, 14.500f, 17.204f, 0.003f }; // Earth masses
		public float[] semiMajor = new float[] { 0.387f, 0.723f, 1.000f, 1.523f, 5.202f, 9.576f, 19.293f, 30.246f, 39.509f }; //AU
		public float[] radii = new float[] { 0.383f, 0.949f, 1.000f, 0.533f, 11.209f, 9.449f, 4.007f, 3.883f, 0.187f }; // Earth radii
		public float[] rotational_periods = new float[] { 58.646f, 243.018f, 0.997f, 1.026f, 0.413f, 0.444f, 0.718f, 0.671f, 6.387f }; // days
		public float[] orbital_periods = new float[] { 0.241f, 0.615f, 1.000f, 1.881f, 11.861f, 29.628f, 84.747f, 166.344f, 248.348f }; // years
		public float[] gravities = new float[] { 0.37f, 0.90f, 1.00f, 0.38f, 2.53f, 1.07f, 0.90f, 1.14f, 0.09f }; // in terms of g = 9.81 m/s^2
		public float[] eccentricities = new float[] { 0.21f, 0.01f, 0.02f, 0.09f, 0.05f, 0.06f, 0.05f, 0.01f, 0.25f };
		public float[] inclination_angles = new float[] { 7.00f, 3.39f, 0.00f, 1.85f, 1.31f, 2.49f, 0.77f, 1.77f, 17.5f };
		public float[] red = new float[] {0.639f, 0.678f, 0.000f, 0.557f, 0.588f, 0.443f, 0.376f, 0.127f, 0.761f};
		public float[] green = new float[] {0.416f, 0.329f, 0.278f, 0.067f, 0.365f, 0.408f, 0.522f, 0.179f, 0.643f};
		public float[] blue = new float[] {0.078f, 0.000f, 0.522f, 0.000f, 0.024f, 0.247f, 0.545f, 0.429f, 0.576f};
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
		[Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
		public GameObject PanelKeyBindings;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
		public GameObject PanelMovement;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
		public GameObject PanelCombat;
		[Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
		public GameObject PanelGeneral;
		public GameObject NewGame;
		public GameObject LoadGame;
		public GameObject LoadedGamesList;
		public GameObject LoadedGame;
		public GameObject PlanetsList;
		public GameObject PlanetConfig;
		public GameObject EditMenu;

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
			PlayerPrefs.DeleteAll();
			if (!PlayerPrefs.HasKey("!DefaultSolarSystem!")) {
				Debug.Log(PlayerPrefs.GetString("!SolarSystemNames!"));
				PlayerPrefs.SetString("!DefaultSolarSystem!", "1");
				SaveAndLoad.SaveSolarSystem(new SolarSystem("The Solar System", planets, masses, semiMajor, radii, rotational_periods, orbital_periods, gravities,
				eccentricities, inclination_angles, red, green, blue));
			}
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
			PanelKeyBindings.SetActive(false);

			lineGame.SetActive(false);
			lineControls.SetActive(false);
			lineVideo.SetActive(false);
			lineKeyBindings.SetActive(false);

			PanelMovement.SetActive(false);
			lineMovement.SetActive(false);
			PanelCombat.SetActive(false);
			lineCombat.SetActive(false);
			PanelGeneral.SetActive(false);
			lineGeneral.SetActive(false);
		}

		public void GamePanel()
		{
			DisablePanels();
			PanelGame.SetActive(true);
			lineGame.SetActive(true);
		}

		public void VideoPanel()
		{
			DisablePanels();
			PanelVideo.SetActive(true);
			lineVideo.SetActive(true);
		}

		public void ControlsPanel()
		{
			DisablePanels();
			PanelControls.SetActive(true);
			lineControls.SetActive(true);
		}

		public void KeyBindingsPanel()
		{
			DisablePanels();
			MovementPanel();
			PanelKeyBindings.SetActive(true);
			lineKeyBindings.SetActive(true);
		}

		public void MovementPanel()
		{
			DisablePanels();
			PanelKeyBindings.SetActive(true);
			PanelMovement.SetActive(true);
			lineMovement.SetActive(true);
		}

		public void CombatPanel()
		{
			DisablePanels();
			PanelKeyBindings.SetActive(true);
			PanelCombat.SetActive(true);
			lineCombat.SetActive(true);
		}

		public void GeneralPanel()
		{
			DisablePanels();
			PanelKeyBindings.SetActive(true);
			PanelGeneral.SetActive(true);
			lineGeneral.SetActive(true);
		}

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
			newGame = new SolarSystem();
		}
		
		public void DestroyPlanetConfigs() {
			foreach (Transform child in PlanetsList.transform) {
				if (child.name != "AddPlanet") {
					Destroy(child.gameObject);
				}
			}
		}

		public void DestroyPlanetConfig(int destroyIndex) {
			Debug.Log("Destroy: " + destroyIndex);
			GameObject child = PlanetsList.transform.GetChild(destroyIndex + 1).gameObject;
			Destroy(child);
			indexes.Remove(destroyIndex);
			foreach( var x in indexes) {
				Debug.Log( x.ToString());
			}
			for (int i = 0; i < indexes.Count; i++) {
				if (indexes[i] > destroyIndex) {
					Debug.Log("Value: " + i);
					GameObject z = PlanetsList.transform.GetChild(indexes[i] + 1).gameObject;
					z.GetComponent<LoadEditPanel>().index -= 1;
					indexes[i] = indexes[i] - 1;
				}
			}
			foreach( var x in indexes) {
				Debug.Log( x.ToString());
			}
		}
		
		public void AddPlanet() {
			GameObject planetPanel = Instantiate(PlanetConfig, PlanetsList.transform) as GameObject;
			planetPanel.GetComponent<LoadEditPanel>().MenuManager = transform.GetComponent<UIMenuManager>();
			if (indexes.Count != 0) {
				foreach( var x in indexes) {
					// Debug.Log( x.ToString());
				}
				planetPanel.GetComponent<LoadEditPanel>().SetIndex(indexes.Count);
				// Debug.Log(indexes[indexes.Count - 1]);
				indexes.Add(indexes.Count);
			}
			else {
				indexes.Add(0);
				planetPanel.GetComponent<LoadEditPanel>().SetIndex(0);
				// Debug.Log(indexes[0]);
			}
		}
		public void EditPlanet() {
			EditMenu.SetActive(true);
			NewGame.SetActive(false);
			Debug.Log("Edit called");
		}

		public void CloseEditPlanet() {
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

		
		public void TestSaveAndLoad()
		{
			SolarSystem savedSolarSystem = SaveAndLoad.LoadSolarSystem("The Solar System");
			Debug.Log(savedSolarSystem.masses[0]);
			Debug.Log(savedSolarSystem.planets[0]);
			Debug.Log(savedSolarSystem.name);
		}

		public void DestroyGameConfigs() {
			foreach (Transform child in LoadedGamesList.transform) {
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