using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject mainPanel, settingsPanel, howToPlayPanel;

    public GameObject contButton;

    public Button[] nottAbilities;
    public Button[] dagrAbilities;

    public static PauseMenu instance;
    public EventSystem eventSystem;

    private GameObject dagr, nott;

    private int currentLevelIndex;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetInt("ChaptersUnlocked", 1);

        for (int i = 0; i > 4; i++)
        {
            nottAbilities[i].interactable = false;
            dagrAbilities[i].interactable = false;
        }
    }

    private void FixedUpdate()
    {
        dagr = GameObject.Find("Player1");
        nott = GameObject.Find("Player2");

        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Chapters&Levels")
        {
            eventSystem.gameObject.SetActive(true);
        }
        
        if (PlayerPrefs.GetInt("LevelPrefs") > 6)
        {
            PlayerPrefs.SetInt("ChaptersUnlocked", 2);
        }

        if (PlayerPrefs.GetInt("LevelPrefs") > 11)
        {
            PlayerPrefs.SetInt("ChaptersUnlocked", 3);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PauseGame") && SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Chapters&Levels")
        {
            if (mainPanel.activeSelf) // turn off the pause menu if it is open
            {
                mainPanel.SetActive(false);
                //howToPlayPanel.SetActive(false);
                Time.timeScale = 1f;
                dagr.GetComponent<PlayerController>().enabled = true;
                nott.GetComponent<PlayerController>().enabled = true;
            }
            else if (!mainPanel.activeSelf) // turn on the pause menu
            {
                mainPanel.SetActive(true);
                //howToPlayPanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(contButton);
                Time.timeScale = 0f;
                dagr.GetComponent<PlayerController>().enabled = false;
                nott.GetComponent<PlayerController>().enabled = false;
            }
        }

        if (PlayerPrefs.GetInt("ChaptersUnlocked") >= 1) // chapter 2, this has to be changes manually if we update how many levels are in chapter 1
        {
            nottAbilities[0].interactable = true;
            dagrAbilities[0].interactable = true;
        }

        if (PlayerPrefs.GetInt("ChaptersUnlocked") >= 2)
        {
            //nottAbilities[1].interactable = true;
            dagrAbilities[1].interactable = true;
        }
        else
        {
            //nottAbilities[1].interactable = false;
            dagrAbilities[1].interactable = false;
        }
        
        // if (PlayerPrefs.GetInt("ChaptersUnlocked") >= 3)
        // {
        //     nottAbilities[2].interactable = true;
        //     dagrAbilities[2].interactable = true;
        // }
    }

    public void continueButton()
    {
        mainPanel.SetActive(false);
        Time.timeScale = 1f;
        
        dagr.GetComponent<PlayerController>().enabled = true;
        nott.GetComponent<PlayerController>().enabled = true;
    }

    public void returnToMenuButton()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedPrefs", currentLevelIndex);
        mainPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        eventSystem.gameObject.SetActive(false);
        
    }

    //public void settingsButton()
    //{
    //    mainPanel.SetActive(false);
    //    settingsPanel.SetActive(true);
    //    howToPlayPanel.SetActive(false);
    //    EventSystem.current.SetSelectedGameObject(settingsPanel.GetComponentInChildren<Button>().gameObject);
    //}

    //public void howToPlayButton()
    //{
    //    mainPanel.SetActive(false);
    //    settingsPanel.SetActive(false);
    //    howToPlayPanel.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(howToPlayPanel.GetComponentInChildren<Button>().gameObject);
    //}

    //public void backButton()
    //{
    //    mainPanel.SetActive(true);
    //    settingsPanel.SetActive(false);
    //    howToPlayPanel.SetActive(false);
    //    EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    //}
}

