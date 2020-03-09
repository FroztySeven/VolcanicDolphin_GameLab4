using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionConfigs : MonoBehaviour
{
    public GameObject
        Canvas_Start,
        Canvas_Options,
        Canvas_HowToPlay,
        Canvas_CharacterSelection;


    // Windowed Fullscreen resolutions
    public void SetFullResolution_1920x1080()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    
    // Windowed Screen resolutions
    public void SetResolution_800x600()
    {
        Screen.SetResolution(800, 600, false);
    }
    public void SetResolution_1366x768()
    {
        Screen.SetResolution(1366, 768, false);
    }
    public void SetResolution_1600x900()
    {
        Screen.SetResolution(1600, 900, false);
    }
    public void SetResolution_1920x1080()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("LevelPrefs");
        PlayerPrefs.DeleteKey("SavedPrefs");

        Destroy(GameObject.Find("Canvas_OtherStuff"));
        SceneManager.LoadScene("MainMenu");
    }

    public void SetPlayerPrefsToMax()
    {
        PlayerPrefs.SetInt("LevelPrefs", 100);
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartMenus()
    {
        Canvas_Start.SetActive(false);
        Canvas_Options.SetActive(false);
        Canvas_HowToPlay.SetActive(false);
        Canvas_CharacterSelection.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
