using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMusicController2 : MonoBehaviour
{
    private FMOD.Studio.EventInstance music;

    public static AudioMusicController2 instance;

    public bool menu, cutScene, levelWon, chapter1, chapter1Loop, chapter2, chapter2Loop, chapter3, chapter3Loop, chapter4, chapter4Loop;

    public int sceneNr;

    public int[] menuNr, cutSceneNr/*, chapter1Nr, chapter2Nr, chapter3Nr, chapter4Nr*/;

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
        music.setParameterByName("Music Controller", 0);
        music.start();
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
        /*
        for (int i = 0; i < chapter1Nr.Length; i++)
        {
            if (sceneNr == chapter1Nr[i])
            {
                menu = false;
                cutScene = false;
                levelWon = false;
                chapter1 = true;
                chapter2 = false;
                chapter3 = false;
                chapter4 = false;
            }
        }

        for (int i = 0; i < chapter2Nr.Length; i++)
        {
            if (sceneNr == chapter2Nr[i])
            {
                menu = false;
                cutScene = false;
                levelWon = false;
                chapter1 = false;
                chapter2 = true;
                chapter3 = false;
                chapter4 = false;
            }
        }

        for (int i = 0; i < chapter3Nr.Length; i++)
        {
            if (sceneNr == chapter3Nr[i])
            {
                menu = false;
                cutScene = false;
                levelWon = false;
                chapter1 = false;
                chapter2 = false;
                chapter3 = true;
                chapter4 = false;
            }
        }

        for (int i = 0; i < chapter4Nr.Length; i++)
        {
            if (sceneNr == chapter4Nr[i])
            {
                menu = false;
                cutScene = false;
                levelWon = false;
                chapter1 = false;
                chapter2 = false;
                chapter3 = false;
                chapter4 = true;
            }
        }
        */
        //-----Chapter 1 - Level 1 -----//
        if (sceneNr == 6)
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
        if (sceneNr == 7 || sceneNr == 8 || sceneNr == 9)
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
        if (sceneNr == 10)
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
        if (sceneNr == 11 || sceneNr == 12 || sceneNr == 13)
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
        if (sceneNr == 14)
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
        if (sceneNr == 15 || sceneNr == 16 || sceneNr == 17)
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
        if (sceneNr == 18)
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
        /*
        //-----Chapter 4 - Level 2, 3, 4 -----//
        if (sceneNr == 19 || sceneNr == 20 || sceneNr == 21)
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
        */
    }

    void PlayTrack()
    {
        if (menu)
        {
            music.setParameterByName("Music Controller", menuVal);
        }

        if (cutScene)
        {
            music.setParameterByName("Music Controller", cutSceVal);
        }

        /*
        if (levelWon)
        {
            music.setParameterByName("Music Controller", levelWonVal);
        }
        */

        //---- Chapter 1 Levels after second, loop ----//
        if (chapter1)
        {
            music.setParameterByName("Music Controller", chapt1Val);

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
        /*
        if (chapter1)
        {
            music.setParameterByName("Music Controller", chapt1Val);
        }

        if (chapter1Loop)
        {
            music.setParameterByName("Music Controller", chapt1LoopVal);
        }

        if (chapter2)
        {
            music.setParameterByName("Music Controller", chapt2Val);
        }

        if (chapter2Loop)
        {
            music.setParameterByName("Music Controller", chapt2LoopVal);
        }

        if (chapter3)
        {
            music.setParameterByName("Music Controller", chapt3Val);
        }

        if (chapter3Loop)
        {
            music.setParameterByName("Music Controller", chapt3LoopVal);
        }

        if (chapter4)
        {
            music.setParameterByName("Music Controller", chapt4Val);
        }

        if (chapter4Loop)
        {
            music.setParameterByName("Music Controller", chapt4LoopVal);
        }
        */
    }
}
