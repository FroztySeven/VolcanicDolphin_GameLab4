using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public GameObject
        P1,
        P2,
        Canvas_Start,
        Canvas_Options,
        Canvas_HowToPlay,
        Canvas_CharacterSelection;

    // Start is called before the first frame update
    void Start()
    {
        Canvas_Start.SetActive(true);
        Canvas_Options.SetActive(false);
        Canvas_HowToPlay.SetActive(false);
        Canvas_CharacterSelection.SetActive(false);
    }

    public void Select_Options()
    {
        Canvas_Options.SetActive(true);

        Canvas_Start.SetActive(false);
        Canvas_HowToPlay.SetActive(false);
    }

    public void Select_HowToPlay()
    {
        Canvas_HowToPlay.SetActive(true);

        Canvas_Start.SetActive(false);
        Canvas_Options.SetActive(false);
    }

    public void Select_SinglePlayer()
    {
        P1.GetComponent<CharacterSelection>().playersSelected = true;
        P2.GetComponent<CharacterSelection>().playersSelected = true;
        CharacterStoredInfo.instance.singlePlayer = true;
        Canvas_Start.SetActive(false);
        Canvas_Options.SetActive(false);
        SceneManager.LoadScene("Chapters&Levels");
    }

    public void MultiPlayer()
    {
        CharacterStoredInfo.instance.singlePlayer = false;
        Invoke("SetCharSelectionActive", 0.5f);
        Canvas_Start.SetActive(false);
        Canvas_Options.SetActive(false);
        Canvas_CharacterSelection.SetActive(true);
    }
    private void SetCharSelectionActive()
    {
        P1.GetComponent<CharacterSelection>().playersSelected = false;
        P2.GetComponent<CharacterSelection>().playersSelected = false;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
         Application.Quit();
        #endif
    }
}
