using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum MainMenuState
{
    INTRO,
    MAIN,
    SETTINGS,
    IN_GAME
}

public class MenuUI : MonoBehaviour
{

    private List<string> allSceneNames;

    [Header("LoadingScreen")]
    public Slider loadingProgress;
    public GameObject loadingScreen;

    [Header("SettingsMenu")]
    public GameObject settingsMenu;
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public TextMeshProUGUI VolumeText;
    public TMP_Dropdown resolutionDropdown;
    public Resolution[] resolutions;
    public Image FullscreenToggleImage;

    [Header("PauseMenu")]
    public GameObject PauseMenu;

    [Header("DebugOptions")]
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private TMP_Dropdown mapDropdown;

    [Header("Main Menu Appearance")]
    [SerializeField] private Animator logoAnim;
    [SerializeField] private MainMenuState menuState;   // put serializefield on it for debugging reasons
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject mainMenuCam;
    [SerializeField] private GameObject settingsCam;
    [SerializeField] private GameObject settingsTransition;
    [SerializeField] private GameObject menuTransition;
    [SerializeField] private GameObject pressStartLabel;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        menuState = MainMenuState.INTRO;
        if (mapDropdown != null)
        mapDropdown.ClearOptions();
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        allSceneNames = new List<string>();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        // get scene names
        for (int i = 0; i < sceneCount; i++)
        {
            allSceneNames.Add(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        }
        if (mapDropdown != null)
            mapDropdown.AddOptions(allSceneNames);

        settingsMenu.SetActive(false);
        volumeSlider.maxValue = 1f;
        volumeSlider.minValue = 0.0001f;
        volumeSlider.value = 1f;

       List<string> ResolutionsOptions = new List<string>();


        int currentResolutionSettingIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOptions = resolutions[i].width + "x" + resolutions[i].height;
            ResolutionsOptions.Add(resolutionOptions);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionSettingIndex = i;
            }
        }
        resolutionDropdown.AddOptions(ResolutionsOptions);
        resolutionDropdown.value = currentResolutionSettingIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuState != MainMenuState.SETTINGS)
            {
                PauseGame();
                PauseMenu.SetActive(true);
            }
        }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // main menu specific
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                debugMenu.SetActive(true);
            }
        }

        if (Input.anyKeyDown)
        {
            if (menuState == MainMenuState.INTRO)
            {
                StartCoroutine(EndTitleIntro());
            }
        }

        if (menuState == MainMenuState.SETTINGS)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseSettingsMenu();
            }
        }
    }

    // on start button
    public void LoadSelectedScene()
    {
        if (mapDropdown != null)
        {
            string sceneName = mapDropdown.options[mapDropdown.value].text;
            StartCoroutine(LoadingManager(sceneName));
        }
    }

    private IEnumerator LoadingManager(string scene)
    {
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while (!loadOperation.isDone)
        {
            // loading bar
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingProgress.value = progress;

            yield return null;
        }
    }

    // Is currently set to the DDR testing scene until the first scene of the game is provided
    public void StartNewGameButton()
    {
       StartCoroutine(LoadingManager("IntroDialogue"));
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    // calls when pressing any key on the initial menu
    private IEnumerator EndTitleIntro()
    {
        logoAnim.SetBool("Active", true);
        pressStartLabel.SetActive(false);

        yield return new WaitForSeconds(logoAnim.GetCurrentAnimatorStateInfo(0).length);

        menuState = MainMenuState.MAIN;
        menuButtons.SetActive(true);
    }

    public void SettingsButton()
    {
        StartCoroutine(OpenSettingsScreen());
    }

    // entering settings
    private IEnumerator OpenSettingsScreen()
    {
        settingsTransition.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        mainMenuCam.SetActive(false);
        settingsCam.SetActive(true);
        settingsMenu.SetActive(true);
        menuButtons.SetActive(false);
        logoAnim.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        menuState = MainMenuState.SETTINGS;
        settingsTransition.SetActive(false);
    }

    // returning to the menu from settings
    private IEnumerator OpenMenuScreen()
    {
        menuTransition.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        settingsCam.SetActive(false);
        mainMenuCam.SetActive(true);
        settingsMenu.SetActive(false);
        logoAnim.gameObject.SetActive(true);
        logoAnim.SetBool("Active", true);

        yield return new WaitForSeconds(0.5f);

        menuState = MainMenuState.MAIN;
        menuButtons.SetActive(true);
        menuTransition.SetActive(false);
    }

    // pressing back button on settings to return to menu
    public void CloseSettingsMenu()
    {
        StartCoroutine(OpenMenuScreen());
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        if (isFullscreen == true)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            FullscreenToggleImage.GetComponent<Image>().color = new Color32(20, 200, 20, 100);
            Debug.Log(isFullscreen);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            FullscreenToggleImage.GetComponent<Image>().color = new Color32(200, 20, 20, 100);
            Debug.Log(isFullscreen);
            
        }
    }

    public void ChangeVolume(float volume)
    {
        //audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

        float theVolume = volumeSlider.value;
        Debug.Log("volume = " +  theVolume);
        // hope this is how you're supposed to do it cus i dont wanna learn wwise
        // it does not work and you know what i can't be bothered to bother the audio guys about wwise so i'm just gonna leave it be.
        AkSoundEngine.SetRTPCValue("Volume", theVolume);
    }

    public void PauseGame()
    {
        
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        
        Time.timeScale = 1f;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        StartCoroutine(LoadingManager("MainMenu"));
    }
}
