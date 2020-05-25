using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMusicController : MonoBehaviour
{
    private FMOD.Studio.EventInstance music, ambiance;

    public static AudioMusicController instance;

    public bool menu, cutScene, levelWon, chapter1, chapter1Loop, chapter2, chapter2Loop, chapter3, chapter3Loop, chapter4, chapter4Loop;

    public int sceneNr;

    public int[] menuNr, cutSceneNr;

    private int menuVal = 0,
        menuLoopVal = 1,
        cutSceVal = 2,
        chapt1Val = 3,
        chapt1LoopVal = 4,
        chapt2Val = 5,
        chapt2LoopVal = 6,
        chapt3Val = 7,
        chapt3LoopVal = 8,
        chapt4Val = 9,
        chapt4LoopVal = 10,
        levelWonVal = 11;

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
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music");
        ambiance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Enviroment/Ambience");
        music.setParameterByName("Music Controller", 0);
        music.start();
        ambiance.setParameterByName("Ambience", 0);
        ambiance.start();
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneNr = SceneManager.GetActiveScene().buildIndex;

        SceneCheck();

        PlayTrack();
    }

    void SceneCheck()
    {
        for (int i = 0; i < menuNr.Length; i++)
        {
            if (sceneNr == menuNr[i])
            {
                menu = true;
                cutScene = false;
                levelWon = false;
                chapter1 = false;
                chapter1Loop = false;
                chapter2 = false;
                chapter2Loop = false;
                chapter3 = false;
                chapter3Loop = false;
                chapter4 = false;
                chapter4Loop = false;
            }
        }

        for (int i = 0; i < cutSceneNr.Length; i++)
        {
            if (sceneNr == cutSceneNr[i])
            {
                menu = false;
                cutScene = true;
                levelWon = false;
                chapter1 = false;
                chapter1Loop = false;
                chapter2 = false;
                chapter2Loop = false;
                chapter3 = false;
                chapter3Loop = false;
                chapter4 = false;
                chapter4Loop = false;
            }
        }
        
        //-----Chapter 1 - Level 1 -----//
        if (sceneNr == 7)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = true;
            chapter1Loop = false;
            chapter2 = false;
            chapter2Loop = false;
            chapter3 = false;
            chapter3Loop = false;
            chapter4 = false;
            chapter4Loop = false;
        }
        //-----Chapter 1 - Level 2, 3, 4 -----//
        if (sceneNr == 8 || sceneNr == 9 || sceneNr == 10 || sceneNr == 11)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = true;
            chapter2 = false;
            chapter2Loop = false;
            chapter3 = false;
            chapter3Loop = false;
            chapter4 = false;
            chapter4Loop = false;
        }
        //-----Chapter 2 - Level 1 -----//
        if (sceneNr == 12)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = false;
            chapter2 = true;
            chapter2Loop = false;
            chapter3 = false;
            chapter3Loop = false;
            chapter4 = false;
            chapter4Loop = false;

        }
        //-----Chapter 2 - Level 2, 3, 4 -----//
        if (sceneNr == 13 || sceneNr == 14 || sceneNr == 15 || sceneNr == 16 || sceneNr == 17 || sceneNr == 18 || sceneNr == 19 || sceneNr == 20 || sceneNr == 21)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = false;
            chapter2 = false;
            chapter2Loop = true;
            chapter3 = false;
            chapter3Loop = false;
            chapter4 = false;
            chapter4Loop = false;
        }

        //-----Chapter 3 - Level 1 -----//
        if (sceneNr == 22)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = false;
            chapter2 = false;
            chapter2Loop = false;
            chapter3 = true;
            chapter3Loop = false;
            chapter4 = false;
            chapter4Loop = false;

        }
        //-----Chapter 3 - Level 2, 3, 4 -----//
        if (sceneNr == 23 || sceneNr == 24 || sceneNr == 25 || sceneNr == 26 || sceneNr == 27 || sceneNr == 28 || sceneNr == 29 || sceneNr == 30 || sceneNr == 31)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = false;
            chapter2 = false;
            chapter2Loop = false;
            chapter3 = false;
            chapter3Loop = true;
            chapter4 = false;
            chapter4Loop = false;
        }
        //-----Chapter 4 - Level 1 -----//
        if (sceneNr == 32)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = false;
            chapter2 = false;
            chapter2Loop = false;
            chapter3 = false;
            chapter3Loop = false;
            chapter4 = true;
            chapter4Loop = false;

            
        }
        
        //-----Chapter 4 - Level 2, 3, 4 -----//
        if (sceneNr == 33 || sceneNr == 34 || sceneNr == 35 || sceneNr == 36 || sceneNr == 37 || sceneNr == 38 || sceneNr == 39 || sceneNr == 40 || sceneNr == 41)
        {
            menu = false;
            cutScene = false;
            levelWon = false;
            chapter1 = false;
            chapter1Loop = false;
            chapter2 = false;
            chapter2Loop = false;
            chapter3 = false;
            chapter3Loop = false;
            chapter4 = false;
            chapter4Loop = true;
        }
        
    }

    void PlayTrack()
    {
        if (menu)
        {
            music.setParameterByName("Music Controller", menuVal);
            ambiance.setParameterByName("Ambience", 0);
        }

        if (cutScene)
        {
            music.setParameterByName("Music Controller", cutSceVal);
            ambiance.setParameterByName("Ambience", 0);
        }

        //---- Chapter 1 Levels after second, loop ----//
        if (chapter1)
        {
            music.setParameterByName("Music Controller", chapt1Val);
            ambiance.setParameterByName("Ambience", 1);
            

            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true; 
                if (levelWon)
                {
                    chapter1 = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }

        }

        if (chapter1Loop)
        {
            music.setParameterByName("Music Controller", chapt1LoopVal);
            ambiance.setParameterByName("Ambience", 1);
            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter1Loop = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }
        }

        //---- Chapter 2 Levels after second, loop ----//
        if (chapter2)
        {
            music.setParameterByName("Music Controller", chapt2Val);
            ambiance.setParameterByName("Ambience", 2);

            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter2 = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }
        }

        if (chapter2Loop)
        {
            music.setParameterByName("Music Controller", chapt2LoopVal);
            ambiance.setParameterByName("Ambience", 2);
            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter2Loop = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }
        }

        //---- Chapter 3 Levels after second, loop ----//
        if (chapter3)
        {
            music.setParameterByName("Music Controller", chapt3Val);
            ambiance.setParameterByName("Ambience", 3);
            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter3 = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }

        }

        if (chapter3Loop)
        {
            music.setParameterByName("Music Controller", chapt3LoopVal);
            ambiance.setParameterByName("Ambience", 3);
            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter3Loop = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }
        }

        //---- Chapter 4 Levels after second, loop ----//
        if (chapter4)
        {
            music.setParameterByName("Music Controller", chapt4Val);
            ambiance.setParameterByName("Ambience", 4);
            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter4 = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }

        }

        if (chapter4Loop)
        {
            music.setParameterByName("Music Controller", chapt4LoopVal);
            ambiance.setParameterByName("Ambience", 4);
            if (GameObject.Find("Door").GetComponent<ExitLevel>().bothEntered == true)
            {
                levelWon = true;
                if (levelWon)
                {
                    chapter4Loop = false;
                    music.setParameterByName("Music Controller", levelWonVal);
                }
            }
            else
            {
                levelWon = false;
            }
        }
    }
}
