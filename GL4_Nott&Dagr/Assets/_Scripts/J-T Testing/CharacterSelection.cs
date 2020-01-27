using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject P1, P2, checkmark, pressAToPlay, levelSelect, playButton;

    public int playerId;

    public Transform controllerStart, controllerNight, controllerDay;

    private int position = 0;
    private int positionMin = -1;
    private int positionMax = 1;
    private int selectedCounter = 0;

    public Vector2 moveInput;

    private bool changedLocation = false;
    private bool characterSelected = false;
    private bool playersSelected = false;

    //private void Start()
    //{
    //    levelSelect.SetActive(false);
    //}

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("HorizontalP" + playerId);

        if (!characterSelected)
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
                        Destroy(P1.GetComponent<CharacterSelection>().controllerNight.gameObject);
                        Destroy(P2.GetComponent<CharacterSelection>().controllerNight.gameObject);
                        P1.GetComponent<CharacterSelection>().positionMin++;
                        P2.GetComponent<CharacterSelection>().positionMin++;
                        //enabled = false;
                        CharacterStoredInfo.instance.night = playerId;
                        checkmark.SetActive(true);
                        characterSelected = true;
                    }
                    break;

                case 0:
                    transform.position = controllerStart.position;
                    break;

                case 1:
                    transform.position = controllerDay.position;
                    if (Input.GetButtonDown("JumpP" + playerId))
                    {
                        Destroy(P1.GetComponent<CharacterSelection>().controllerDay.gameObject);
                        Destroy(P2.GetComponent<CharacterSelection>().controllerDay.gameObject);
                        P1.GetComponent<CharacterSelection>().positionMax--;
                        P2.GetComponent<CharacterSelection>().positionMax--;
                        //enabled = false;
                        CharacterStoredInfo.instance.day = playerId;
                        checkmark.SetActive(true);
                        characterSelected = true;
                    }
                    break;
            }
        }

        if (P1.GetComponent<CharacterSelection>().characterSelected && P2.GetComponent<CharacterSelection>().characterSelected)
        {

            if (Input.GetButtonDown("JumpP" + playerId))
            {
                playersSelected = true;
                levelSelect.SetActive(true);
                EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
                es.SetSelectedGameObject(null);
                es.SetSelectedGameObject(es.firstSelectedGameObject);
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
}
