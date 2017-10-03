using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour {

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Button applyButton;

    public AudioSource ClickSound;
    public AudioSource mainSound;
    public Resolution[] resolutions;
    public GameSettings gameSettings;


     void Start()
    {
        ClickSound.Stop();                               // Sound will not play.
    }

     void OnEnable()
        {
            gameSettings = new GameSettings();          // Game setting will update values changed on each component.
        
            fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
            resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
            textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
            antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
            vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
            musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
            applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;               // Resolution will update to screen resolution.
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        LoadSettings();                                // When application is opened the game will load recent settings to the game. 
     }
    public void OnFullscreenToggle()                  //  Updates toggle when on or off.
    {
       gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
        ClickSound.Play();
    }
    public void OnResolutionChange()                 // Changes resolution as set by the player.
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height,Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
        ClickSound.Play();
    }
    public void OnTextureQualityChange()            // Changes the texture quality in the game.
    {
      QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
        ClickSound.Play();
    }
    public void OnAntialiasingChange()              // Changes the Antialising setting. 
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2f, antialiasingDropdown.value);
        ClickSound.Play();
    }
    public void OnVSyncChange()                   //  Changes the VSync setting.
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
        ClickSound.Play();
    }
    public void OnMusicVolumeChange()            // Changes the music volume setting.
    {
        mainSound.volume = gameSettings.musicVolume = musicVolumeSlider.value;
       
    }
    public void OnApplyButtonClick()            // Saves changes when player hits the apply button.
    {
        SaveSettings();
        ClickSound.Play();
    }
    public void SaveSettings()                 // Saves settings to application file and will not allow sound to play when the game loads.
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        ClickSound.Stop();
    }
    public void LoadSettings()               // Loads settings when player opens up the application file. Sound will not play when game loads. 
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        musicVolumeSlider.value = gameSettings.musicVolume;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
        ClickSound.Stop();
    }
}

