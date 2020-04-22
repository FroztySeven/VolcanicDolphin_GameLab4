using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMusicController : MonoBehaviour
{
    public GameObject mainMenu, cutScene, chpt1, chpt2, chpt3, chpt4;

    public int musicTrack, sceneNr;

    public bool menu, cutscene, chapter1, chapter2, chapter3;

    public static AudioMusicController instance;

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

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("Menu");
        cutScene = GameObject.Find("Cutscene");
        chpt1 = GameObject.Find("Chapter 1");
        chpt2 = GameObject.Find("Chapter 2");
        chpt3 = GameObject.Find("Chapter 3");
        chpt4 = GameObject.Find("Chapter 4");

        mainMenu.SetActive(false);
        cutScene.SetActive(false);
        chpt1.SetActive(false);
        chpt2.SetActive(false);
        chpt3.SetActive(false);
        chpt4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneNr = SceneManager.GetActiveScene().buildIndex;

        //Debug.Log("Active Scene is '" + scene.name + "'.");
        //Debug.Log("Active Scene Number is '" + sceneNr + "'.");
        //Debug.Log(musicTrack);

        MusicTracker();

        if (menu == true)
        {
            mainMenu.SetActive(true);
            
            cutScene.SetActive(false);
            chpt1.SetActive(false);
            chpt2.SetActive(false);
            chpt3.SetActive(false);
            chpt4.SetActive(false);
        }
        else if(cutscene == true)
        {
            cutScene.SetActive(true);

            mainMenu.SetActive(false);
            
            chpt1.SetActive(false);
            chpt2.SetActive(false);
            chpt3.SetActive(false);
            chpt4.SetActive(false);
        }
        else if (chapter1 == true)
        {
            chpt1.SetActive(true);

            mainMenu.SetActive(false);
            cutScene.SetActive(false);
            
            chpt2.SetActive(false);
            chpt3.SetActive(false);
            chpt4.SetActive(false);
        }
        else if (chapter2 == true)
        {
            chpt2.SetActive(true);

            mainMenu.SetActive(false);
            cutScene.SetActive(false);
            chpt1.SetActive(false);
            
            chpt3.SetActive(false);
            chpt4.SetActive(false);
        }
        else if (chapter3 == true)
        {
            chpt3.SetActive(true);

            mainMenu.SetActive(false);
            cutScene.SetActive(false);
            chpt1.SetActive(false);
            chpt2.SetActive(false);
            
            chpt4.SetActive(false);
        }

        /*
        else if (chapter4 == true)
        {
            chpt4.SetActive(true);

            mainMenu.SetActive(false);
            cutScene.SetActive(false);
            chpt1.SetActive(false);
            chpt2.SetActive(false);
            chpt3.SetActive(false);
        
        }
        */
    }

    void MusicTracker()
    {
        if (sceneNr == -1 || sceneNr == 0 || sceneNr == 1)
        {
            menu = true;
            cutscene = false;
            chapter1 = false;
            chapter2 = false;
            chapter3 = false;
            musicTrack = 2;
        }

        if (sceneNr == 2 || sceneNr == 7 || sceneNr == 12)
        {
            menu = false;
            cutscene = true;
            chapter1 = false;
            chapter2 = false;
            chapter3 = false;
            musicTrack = 1;
        }

        if (sceneNr == 3 || sceneNr == 4 || sceneNr == 5 || sceneNr == 6)
        {
            menu = false;
            cutscene = false;
            chapter1 = true;
            chapter2 = false;
            chapter3 = false;
            musicTrack = 3;
        }

        if (sceneNr == 8 || sceneNr == 9 || sceneNr == 10 || sceneNr == 11)
        {
            menu = false;
            cutscene = false;
            chapter1 = false;
            chapter2 = true;
            chapter3 = false;
            musicTrack = 4;
        }

        if (sceneNr == 13 || sceneNr == 14 || sceneNr == 15 || sceneNr == 16)
        {
            menu = false;
            cutscene = false;
            chapter1 = false;
            chapter2 = false;
            chapter3 = true;
            musicTrack = 5;
        }
    }
}    

