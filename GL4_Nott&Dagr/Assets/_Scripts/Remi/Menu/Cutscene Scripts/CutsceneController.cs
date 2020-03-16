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
        ToBeContinued
    }


    public float cutsceneTimer = 0f;
    public Text loadingText;

    public GameObject skipBG;
    public GameObject skipImage;

    public CutsceneLoadToLevel cutsceneLoadToLevel;

    // Start is called before the first frame update
    void Start()
    {
        skipBG.SetActive(false);
        loadingText.gameObject.SetActive(false);
    }

    public string mainMenu;
    public string firstLevelInChapter1;
    public string firstLevelInChapter2;
    public string firstLevelInChapter3;

    // Update is called once per frame
    void Update()
    {
        cutsceneTimer += Time.deltaTime;
        skipImage.SetActive(false);

        // Cutscene 1
        if (cutsceneLoadToLevel.ToString() == "Helheim")
        {
            if (cutsceneTimer >= 1f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            if (cutsceneTimer >= 5f)
            {
                GetComponent<Animator>().SetTrigger("SetFadeOut");
            }

            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }

            if (cutsceneTimer >= 7f)
            {
                loadingText.gameObject.SetActive(true);
                SceneManager.LoadScene(firstLevelInChapter1);
            }


            if (Input.GetButtonDown("JumpP1") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter1);
            }
        }

        // Cutscene 2
        if (cutsceneLoadToLevel.ToString() == "Jotunheim")
        {
            if (cutsceneTimer >= 1f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            if (cutsceneTimer >= 5f)
            {
                GetComponent<Animator>().SetTrigger("SetFadeOut");
            }

            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }

            if (cutsceneTimer >= 7f)
            {
                loadingText.gameObject.SetActive(true);
                SceneManager.LoadScene(firstLevelInChapter2);
            }

            if (Input.GetButtonDown("JumpP1") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter2);
            }
        }


        // Cutscene 3
        if (cutsceneLoadToLevel.ToString() == "Muspelheim")
        {
            if (cutsceneTimer >= 1f)
            {
                skipBG.SetActive(true);
                skipImage.SetActive(true);
            }

            if (cutsceneTimer >= 5f)
            {
                GetComponent<Animator>().SetTrigger("SetFadeOut");
            }

            if (cutsceneTimer >= 6.5f)
            {
                skipBG.SetActive(false);
                skipImage.SetActive(false);
            }

            if (cutsceneTimer >= 7f)
            {
                loadingText.gameObject.SetActive(true);
                SceneManager.LoadScene(firstLevelInChapter3);
            }
            
            if (Input.GetButtonDown("JumpP1") || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(firstLevelInChapter3);
            }
        }
        
        // Cutscene TBC
        if (cutsceneLoadToLevel.ToString() == "ToBeContinued")
        {
            if (cutsceneTimer >= 1f)
            {
                loadingText.gameObject.SetActive(true);
            }
            if (cutsceneTimer >= 3f)
            {
                GetComponent<Animator>().SetTrigger("SetFadeOut");
            }
            if (cutsceneTimer >= 5f)
            {
                SceneManager.LoadScene(mainMenu);
            }
        }
    }
}
