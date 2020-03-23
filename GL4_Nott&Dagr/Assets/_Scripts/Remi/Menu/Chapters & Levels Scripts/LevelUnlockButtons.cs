using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUnlockButtons : MonoBehaviour
{
    public Button[] allChapterButtons;

    [Space(20)]
    public Button[] C1;
    
    [Space(20)]
    public Button[] C2;
    
    [Space(20)]
    public Button[] C3;
    
    [Space(20)]
    public Button[] C4;
    
    [Space(20)]
    public Button[] C5;
    
    [Space(20)]
    public Button[] C6;
    
    [Space(20)]
    public Button[] C7;
    
    [Space(20)]
    public Button[] C8;
    
    [Space(20)]
    public Button[] C9;

    // Method is being used to check how big the LevelPrefs int is, and unlocks every chapter based on the assigned value
    void Update()
    {
        //Can chapter 2 be unlocked?
        if (PlayerPrefs.GetInt("LevelPrefs") > 6)
        {
            allChapterButtons[1].gameObject.SetActive(true);
        }
        //Can chapter 3 be unlocked?
        if (PlayerPrefs.GetInt("LevelPrefs") > 11)
        {
            allChapterButtons[2].gameObject.SetActive(true);
        }
    }
    
    
    void Start()
    {
        //Assign the LevelPrefs at 3 when progress is null
        int levelReached = PlayerPrefs.GetInt("LevelPrefs", 3);

        //Disable all level buttons in chapter 1 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        //The first level button is also enabled on start
        for (int i = 0; i < C1.Length; i++)
        {
            if (i + 3 > levelReached)
            {
                C1[i].interactable = false;
            }
        }
        //Disable all level buttons in chapter 2 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        for (int i = 0; i < C2.Length; i++)
        {
            //The first level button is enabled when chapter 2 is unlocked
            if (i + 7 > levelReached)
            {
                C2[0].interactable = true;
            }
            if (i + 8 > levelReached)
            {
                C2[i].interactable = false;
            }
        }

        //Disable all level buttons in chapter 3 and the LevelPrefs int increases to unlock every button
        //The integer increases after completing every level within the chapter from the "ExitLevel.cs" script
        for (int i = 0; i < C3.Length; i++)
        {
            //The first level button is enabled when chapter 3 is unlocked
            if (i + 12 > levelReached)
            {
                C3[0].interactable = true;
            }
            if (i + 13 > levelReached)
            {
                C3[i].interactable = false;
            }
        }
    }
}
