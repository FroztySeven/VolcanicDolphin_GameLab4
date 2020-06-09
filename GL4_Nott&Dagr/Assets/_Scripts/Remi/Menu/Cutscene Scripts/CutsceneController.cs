using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public enum CutsceneLoadToLevel
    {
        None,
        Helheim,
        Jotunheim,
        Muspelheim,
        Vanaheim,
        ToBeContinued
    }
    public CutsceneLoadToLevel cutsceneLoadToLevel;

    public float cutsceneTimer = 0f;
    public Text loadingText;

    public GameObject skipBG;
    public GameObject skipImage;


    
    void Start()
    {
        _animator = GetComponent<Animator>();
        skipBG.SetActive(false);
        skipImage.SetActive(false);
        loadingText.gameObject.SetActive(false);
    }

    //Defined in the inspector
    public string mainMenu;
    public string firstLevelInChapter1;
    public string firstLevelInChapter2;
    public string firstLevelInChapter3;
    public string firstLevelInChapter4;
    private Animator _animator;
    
    
    void Update()
    {
        cutsceneTimer += Time.deltaTime;

        // Cutscene 1
        if (cutsceneLoadToLevel.ToString() == "Helheim")
        {
            //When the cutscene timer is bigger than 1 second, show the skip image in the corner of the scene
            if (cutsceneTimer >= 1.5f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            //Start fading out the cutscene after 5 seconds
            if (cutsceneTimer >= 5f)
            {
                _animator.SetTrigger("SetFadeOut");
            }

            /*//While the fading animation happens, disable the skip image in the corner of the scene
            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }*/

            //Change scene when passing 7 seconds
            if (cutsceneTimer >= 7f)
            {
                /*loadingText.gameObject.SetActive(true);*/
                SceneManager.LoadScene(firstLevelInChapter1);
            }
            
            //Enable either one of the players to press the "A" button to skip straight to the first defined level of the chapter
            if (Input.GetButtonDown("JumpP1") || Input.GetButtonDown("JumpP2") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter1);
            }
        }

        // Cutscene 2
        if (cutsceneLoadToLevel.ToString() == "Jotunheim")
        {
            //When the cutscene timer is bigger than 1 second, show the skip image in the corner of the scene
            if (cutsceneTimer >= 1.5f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            //Start fading out the cutscene after 5 seconds
            if (cutsceneTimer >= 5f)
            {
                _animator.SetTrigger("SetFadeOut");
            }

            /*//While the fading animation happens, disable the skip image in the corner of the scene
            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }*/

            //Change scene when passing 7 seconds
            if (cutsceneTimer >= 7f)
            {
                loadingText.gameObject.SetActive(true);
                SceneManager.LoadScene(firstLevelInChapter2);
            }

            //Enable either one of the players to press the "A" button to skip straight to the first defined level of the chapter
            if (Input.GetButtonDown("JumpP1") || Input.GetButtonDown("JumpP2") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter2);
            }
        }


        // Cutscene 3
        if (cutsceneLoadToLevel.ToString() == "Muspelheim")
        {
            //When the cutscene timer is bigger than 1 second, show the skip image in the corner of the scene
            if (cutsceneTimer >= 1.5f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            //Start fading out the cutscene after 5 seconds
            if (cutsceneTimer >= 5f)
            {
                _animator.SetTrigger("SetFadeOut");
            }

            /*//While the fading animation happens, disable the skip image in the corner of the scene
            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }*/

            //Change scene when passing 7 seconds
            if (cutsceneTimer >= 7f)
            {
                loadingText.gameObject.SetActive(true);
                SceneManager.LoadScene(firstLevelInChapter3);
            }
            
            //Enable either one of the players to press the "A" button to skip straight to the first defined level of the chapter
            if (Input.GetButtonDown("JumpP1") || Input.GetButtonDown("JumpP2") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter3);
            }
        }
        
        // Cutscene 3
        if (cutsceneLoadToLevel.ToString() == "Vanaheim")
        {
            //When the cutscene timer is bigger than 1 second, show the skip image in the corner of the scene
            if (cutsceneTimer >= 1.5f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            //Start fading out the cutscene after 5 seconds
            if (cutsceneTimer >= 5f)
            {
                _animator.SetTrigger("SetFadeOut");
            }

            /*//While the fading animation happens, disable the skip image in the corner of the scene
            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }*/

            //Change scene when passing 7 seconds
            if (cutsceneTimer >= 7f)
            {
                loadingText.gameObject.SetActive(true);
                SceneManager.LoadScene(firstLevelInChapter4);
            }
            
            //Enable either one of the players to press the "A" button to skip straight to the first defined level of the chapter
            if (Input.GetButtonDown("JumpP1") || Input.GetButtonDown("JumpP2") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter4);
            }
        }
        
        // Cutscene TBC
        if (cutsceneLoadToLevel.ToString() == "ToBeContinued")
        {
            loadingText.gameObject.SetActive(true);
            /*//When the cutscene timer is bigger than 1 second, show the loading text
            if (cutsceneTimer >= 1.5f)
            {
                loadingText.gameObject.SetActive(true);
            }*/
            //Start fading out the cutscene after 5 seconds
            if (cutsceneTimer >= 3f)
            {
                _animator.SetTrigger("SetFadeOut");
            }
            //Change scene when passing 5 seconds
            if (cutsceneTimer >= 5f)
            {
                SceneManager.LoadScene(mainMenu);
            }
        }
    }
}
