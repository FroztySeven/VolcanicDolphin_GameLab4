﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject P1, P2, checkmark, pressAToPlay, levelSelect, levelStart, howToPlay, startSelectFirstButton, backButton, levelSelectFirstButton;

    public int playerId;

    public Transform controllerStart, controllerNight, controllerDay;

    private int position = 0;
    private int positionMin = -1;
    private int positionMax = 1;
    private int selectedCounter = 0;

    public Vector2 moveInput;

    [HideInInspector] public bool changedLocation = false;
    [HideInInspector] public bool characterSelected = false;
    [HideInInspector] public bool playersSelected = true;

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("HorizontalP" + playerId);

        if (!characterSelected && !playersSelected)
        {
            if (moveInput.x < 0 && position > positionMin && !changedLocation)
            {
                position--;
                changedLocation = true;
            }

            if (moveInput.x > 0 && position < positionMax && !changedLocation)
            {
                position++;
                changedLocation = true;
            }

            if (moveInput == Vector2.zero)
            {
                changedLocation = false;
            }

            switch (position)
            {
                case -1:
                    transform.position = controllerNight.position;
                    if (Input.GetButtonDown("JumpP" + playerId))
                    {
                        P1.GetComponent<CharacterSelection>().controllerNight.gameObject.SetActive(false);
                        P2.GetComponent<CharacterSelection>().controllerNight.gameObject.SetActive(false);
                        P1.GetComponent<CharacterSelection>().positionMin++;
                        P2.GetComponent<CharacterSelection>().positionMin++;
                        CharacterStoredInfo.instance.night = playerId;
                        checkmark.SetActive(true);
                        characterSelected = true;

                        if (P1.transform.position.x == P2.transform.position.x)
                        {
                            if (!P1.GetComponent<CharacterSelection>().characterSelected)
                            {
                                P1.GetComponent<CharacterSelection>().position++;
                            }

                            if (!P2.GetComponent<CharacterSelection>().characterSelected)
                            {
                                P2.GetComponent<CharacterSelection>().position++;
                            }
                        }
                    }
                    break;

                case 0:
                    transform.position = controllerStart.position;
                    break;

                case 1:
                    transform.position = controllerDay.position;
                    if (Input.GetButtonDown("JumpP" + playerId))
                    {
                        P1.GetComponent<CharacterSelection>().controllerDay.gameObject.SetActive(false);
                        P2.GetComponent<CharacterSelection>().controllerDay.gameObject.SetActive(false);
                        P1.GetComponent<CharacterSelection>().positionMax--;
                        P2.GetComponent<CharacterSelection>().positionMax--;
                        CharacterStoredInfo.instance.day = playerId;
                        checkmark.SetActive(true);
                        characterSelected = true;

                        if (P1.transform.position.x == P2.transform.position.x)
                        {
                            if (!P1.GetComponent<CharacterSelection>().characterSelected)
                            {
                                P1.GetComponent<CharacterSelection>().position--;
                                Debug.Log("P1");
                            }

                            if (!P2.GetComponent<CharacterSelection>().characterSelected)
                            {
                                P2.GetComponent<CharacterSelection>().position--;
                                Debug.Log("P2");
                            }
                        }
                    }
                    break;
            }
        }

        if (P1.GetComponent<CharacterSelection>().characterSelected && P2.GetComponent<CharacterSelection>().characterSelected)
        {

            if (Input.GetButtonDown("JumpP" + playerId))
            {
                P1.GetComponent<CharacterSelection>().playersSelected = true;
                P2.GetComponent<CharacterSelection>().playersSelected = true;
                SceneManager.LoadScene("Chapters&Levels");
            }
        }
    }

    public void SinglePlayer()
    {
        P1.GetComponent<CharacterSelection>().playersSelected = true;
        P2.GetComponent<CharacterSelection>().playersSelected = true;
        CharacterStoredInfo.instance.singlePlayer = true;
        levelStart.SetActive(false);
        levelSelect.SetActive(true);
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(levelSelectFirstButton);
    }

    public void MultiPlayer()
    {
        CharacterStoredInfo.instance.singlePlayer = false;
        Invoke("SetCharSelectionActive", 0.5f);
        levelStart.SetActive(false);
    }

    private void SetCharSelectionActive()
    {
        P1.GetComponent<CharacterSelection>().playersSelected = false;
        P2.GetComponent<CharacterSelection>().playersSelected = false;
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Prologue Level 01");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Prologue Level 02");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Prologue Level 03");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("Prologue Level 04");
    }

    public void LevelFive()
    {
        SceneManager.LoadScene("Burny-burny Swing-Swing");
    }

    public void LevelSix()
    {
        SceneManager.LoadScene("Gunnar - Level 03");
    }

    public void LevelSeven()
    {
        SceneManager.LoadScene("J-T Level 01");
    }

    public void LevelEight()
    {
        SceneManager.LoadScene("J-T Level 02");
    }

    public void LevelNine()
    {
        SceneManager.LoadScene("J-T Level 03");

    }

    public void LevelTen()
    {
        SceneManager.LoadScene("Jane Level 01");

    }

    public void LevelEleven()
    {
        SceneManager.LoadScene("Remi Level 01");

    }

    public void LevelTwelve()
    {
        SceneManager.LoadScene("Remi Level 02");

    }

    public void LevelThirteen()
    {
        SceneManager.LoadScene("Sindre Level 01");

    }

    public void LevelFourteen()
    {
        SceneManager.LoadScene("Sindre Level 02");

    }

    public void LevelFifteen()
    {
        SceneManager.LoadScene("Sindre Level 03");

    }

    public void LevelSixteen()
    {
        SceneManager.LoadScene("Sindre Level 04");

    }

    public void LevelSeventeen()
    {
        SceneManager.LoadScene("Gunnar - Level 01");

    }

    public void LevelEighteen()
    {
        SceneManager.LoadScene("Gunnar - Level 02");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        levelStart.SetActive(false);

        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(backButton);
    }

    public void BackButton()
    {
        levelStart.SetActive(true);
        howToPlay.SetActive(false);

        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }
}
