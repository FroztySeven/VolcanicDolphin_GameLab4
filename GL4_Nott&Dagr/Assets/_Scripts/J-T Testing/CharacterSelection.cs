using System.Collections;
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

    private bool changedLocation = false;
    private bool characterSelected = false;
    private bool playersSelected = true;

    //private void Start()
    //{
    //    levelSelect.SetActive(false);
    //}

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
                        //Destroy(P1.GetComponent<CharacterSelection>().controllerNight.gameObject);
                        //Destroy(P2.GetComponent<CharacterSelection>().controllerNight.gameObject);
                        P1.GetComponent<CharacterSelection>().controllerNight.gameObject.SetActive(false);
                        P2.GetComponent<CharacterSelection>().controllerNight.gameObject.SetActive(false);
                        P1.GetComponent<CharacterSelection>().positionMin++;
                        P2.GetComponent<CharacterSelection>().positionMin++;
                        //enabled = false;
                        CharacterStoredInfo.instance.night = playerId;
                        checkmark.SetActive(true);
                        characterSelected = true;

                        if (P1.transform.position.x == P2.transform.position.x)
                        {
                            if (!P1.GetComponent<CharacterSelection>().characterSelected)
                            {
                                //P1.transform.position = controllerStart.position;
                                P1.GetComponent<CharacterSelection>().position++;
                            }

                            if (!P2.GetComponent<CharacterSelection>().characterSelected)
                            {
                                //P2.transform.position = controllerStart.position;
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
                        //enabled = false;
                        CharacterStoredInfo.instance.day = playerId;
                        checkmark.SetActive(true);
                        characterSelected = true;

                        if (P1.transform.position.x == P2.transform.position.x)
                        {
                            if (!P1.GetComponent<CharacterSelection>().characterSelected)
                            {
                                //P1.transform.position = controllerStart.position;
                                P1.GetComponent<CharacterSelection>().position--;
                                Debug.Log("P1");
                            }

                            if (!P2.GetComponent<CharacterSelection>().characterSelected)
                            {
                                //P2.transform.position = controllerStart.position;
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
                levelSelect.SetActive(true);
                EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
                es.SetSelectedGameObject(null);
                es.SetSelectedGameObject(levelSelectFirstButton);
                //pressAToPlay.SetActive(true);
                //selectedCounter++;
            }

            //if (Input.GetButtonUp("JumpP" + playerId))
            //{
            //    selectedCounter++;
            //}

            //if (Input.GetButtonDown("JumpP" + playerId) && playersSelected && selectedCounter >= 2)
            //{
            //    SceneManager.LoadScene("Test");
            //}
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
        SceneManager.LoadScene("Camera Static Size 5");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Camera Static Size 10");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Camera Static Size 15");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("Camera Zoom Test");
    }

    public void LevelFive()
    {
        SceneManager.LoadScene("Camera Split Screen Test");
    }

    public void LevelSix()
    {
        SceneManager.LoadScene("Prototype Level");
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
