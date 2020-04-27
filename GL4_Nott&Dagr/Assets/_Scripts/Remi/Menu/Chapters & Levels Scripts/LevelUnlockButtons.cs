using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelUnlockButtons : MonoBehaviour
{
    public GameObject debuggingInfo;
    public GameObject[] allChapterCanvases;
    public Button[] allChapterButtons;

    //Array element 0
    [Space(20)]
    public Button[] C1;
    [FormerlySerializedAs("lastLevelNumberOfC1")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C2")] 
    public int lastNumberOfC1;

    //Array element 1
    [Space(20)] 
    public Button[] C2;
    [FormerlySerializedAs("lastLevelNumberOfC2")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C3")] 
    public int lastNumberOfC2;
    
    //Array element 2
    [Space(20)]
    public Button[] C3;
    [FormerlySerializedAs("lastLevelNumberOfC3")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C4")] 
    public int lastNumberOfC3;
    
    //Array element 3
    [Space(20)]
    public Button[] C4;
    [FormerlySerializedAs("lastLevelNumberOfC3")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C5")] 
    public int lastNumberOfC4;
    
    //Array element 4
    [Space(20)]
    public Button[] C5;
    [FormerlySerializedAs("lastLevelNumberOfC3")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C6")] 
    public int lastNumberOfC5;
    
    //Array element 5
    [Space(20)]
    public Button[] C6;
    [FormerlySerializedAs("lastLevelNumberOfC3")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C7")] 
    public int lastNumberOfC6;
    
    //Array element 6
    [Space(20)]
    public Button[] C7;
    [FormerlySerializedAs("lastLevelNumberOfC3")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C8")] 
    public int lastNumberOfC7;
    
    //Array element 7
    [Space(20)]
    public Button[] C8;
    [FormerlySerializedAs("lastLevelNumberOfC3")] [Tooltip("If the LevelPrefs unlock number is bigger than this variable, then unlock C9")] 
    public int lastNumberOfC8;
    
    //Array element 8
    [Space(20)]
    public Button[] C9;
    

    // Method is being used to check how big the LevelPrefs int is, and unlocks every the chapter if it is higher thana defined
    void Update()
    {
        //Can chapter 1 be unlocked? It is already unlocked by default
            //allChapterButtons[0].gameObject.SetActive(true);
        
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC1)
        {
            //If Array isn't empty in the Inspector
            if (C2.Length > 1)
            {
                //Can chapter 2 be unlocked?
                allChapterButtons[1].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC2)
        {
            //If Array isn't empty in the Inspector
            if (C3.Length > 1)
            {
                //Can chapter 3 be unlocked?
                allChapterButtons[2].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC3)
        {
            //If Array isn't empty in the Inspector
            if (C4.Length > 1)
            {
                //Can chapter 4 be unlocked?
                allChapterButtons[3].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC4)
        {
            //If Array isn't empty in the Inspector
            if (C5.Length > 1)
            {
                //Can chapter 5 be unlocked?
                allChapterButtons[4].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC4)
        {
            //If Array isn't empty in the Inspector
            if (C6.Length > 1)
            {
                //Can chapter 5 be unlocked?
                allChapterButtons[5].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC4)
        {
            //If Array isn't empty in the Inspector
            if (C7.Length > 1)
            {
                //Can chapter 5 be unlocked?
                allChapterButtons[6].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC4)
        {
            //If Array isn't empty in the Inspector
            if (C8.Length > 1)
            {
                //Can chapter 5 be unlocked?
                allChapterButtons[7].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > lastNumberOfC4)
        {
            //If Array isn't empty in the Inspector
            if (C9.Length > 1)
            {
                //Can chapter 5 be unlocked?
                allChapterButtons[8].gameObject.SetActive(true);
            }
        }



        //Show debug text when holding the "View/Back" button on the controller or "V" on the keyboard
        if (Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick2Button6) || Input.GetKey(KeyCode.V))
        {
            debuggingInfo.SetActive(true);
        }
        else
        {
            debuggingInfo.SetActive(false);
        }
    }
    
    
    void Start()
    {
        //Disable all canvases on start
        for (int i = 0; i < allChapterCanvases.Length; i++)
        {
            allChapterCanvases[i].SetActive(false);
        }

        //Assign the LevelPrefs to a default when there's no user progress
        int levelReached = PlayerPrefs.GetInt("LevelPrefs", 1);
        
        //Debug levelprefs to text
        debuggingInfo.GetComponentInChildren<Text>().text = "LevelPrefs unlock number is currently:  " + levelReached;

        //Disable all level buttons in chapter 1 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        //The first level button is also enabled on start
        for (int i = 0; i < C1.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                C1[i].interactable = false;
            }
        }
        //Disable all level buttons in chapter 2 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        for (int i = 0; i < C2.Length; i++)
        {
            //The first level button is enabled when chapter 2 is unlocked
            if (i + lastNumberOfC1 >= levelReached)
            {
                C2[i].interactable = false;
            }
        }

        //Disable all level buttons in chapter 3 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        for (int i = 0; i < C3.Length; i++)
        {
            //The first level button is enabled when chapter 3 is unlocked
            if (i + lastNumberOfC2 >= levelReached)
            {
                C3[i].interactable = false;
            }
        }
        
        //Disable all level buttons in chapter 4 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        for (int i = 0; i < C4.Length; i++)
        {
            //The first level button is enabled when chapter 3 is unlocked
            if (i + lastNumberOfC3 >= levelReached)
            {
                C4[i].interactable = false;
            }
        }
        
        //Skip locking Chapter 5 levels
        
        //Skip locking Chapter 6 levels
        
        //Skip locking Chapter 7 levels
        
        //Skip locking Chapter 8 levels
        
        //Skip locking Chapter 9 levels
    }
}
