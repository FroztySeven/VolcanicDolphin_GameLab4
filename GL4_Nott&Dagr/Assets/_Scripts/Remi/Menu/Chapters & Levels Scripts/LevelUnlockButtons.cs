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

    public Text progressPrefsText;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelPrefs", 3);
        progressPrefsText.text = "Unlock prefs int: " + levelReached.ToString();

        //Disable all chapter 1 buttons except the first one, where the PlayerPrefs integer is bigger than "2"
        for (int i = 0; i < C1.Length; i++)
        {
            if (i + 3 > levelReached)
            {
                C1[i].interactable = false;
            }
        }
        //Disable all chapter 2 buttons except the first one, where the PlayerPrefs integer is bigger than "6"
        for (int i = 0; i < C2.Length; i++)
        {
            if (i + 7 > levelReached)
            {
                C2[0].interactable = true;
            }
            if (i + 8 > levelReached)
            {
                C2[i].interactable = false;
            }
        }

        //Disable all chapter 2 buttons except the first one, where the PlayerPrefs integer is bigger than "6"
        for (int i = 0; i < C3.Length; i++)
        {
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

    void Update()
    {
        //When the integer is bigger than 5, unlock chapter button 2
        if (PlayerPrefs.GetInt("LevelPrefs") > 6)
        {
            allChapterButtons[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("LevelPrefs") > 11)
        {
            allChapterButtons[2].gameObject.SetActive(true);
        }
    }
}
