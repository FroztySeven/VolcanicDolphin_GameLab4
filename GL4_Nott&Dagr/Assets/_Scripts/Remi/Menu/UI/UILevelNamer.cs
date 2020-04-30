using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelNamer : MonoBehaviour
{
    public static UILevelNamer instance;
    private Text childText;

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
    }
    
    private void Start()
    {
        childText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        childText.text = SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "MainMenu" ||
            SceneManager.GetActiveScene().name == "Chapters&Levels" ||
            SceneManager.GetActiveScene().name == "Cutscene1" ||
            SceneManager.GetActiveScene().name == "Cutscene2" ||
            SceneManager.GetActiveScene().name == "Cutscene3" ||
            SceneManager.GetActiveScene().name == "Cutscene4" ||
            SceneManager.GetActiveScene().name == "Cutscene5" ||
            SceneManager.GetActiveScene().name == "Cutscene6" ||
            SceneManager.GetActiveScene().name == "Cutscene7" ||
            SceneManager.GetActiveScene().name == "Cutscene8" ||
            SceneManager.GetActiveScene().name == "Cutscene9" ||
            SceneManager.GetActiveScene().name == "CutsceneTBC")
        {
            childText.text = "";
        }
    }
}
