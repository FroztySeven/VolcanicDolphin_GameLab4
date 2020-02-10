using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameObject finished;

    private bool nightEnter, dayEnter, levelFinished;

    private GameObject night, day;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
        {
            nightEnter = true;
            night = other.gameObject;
        }

        if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
        {
            dayEnter = true;
            day = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
        {
            nightEnter = false;
        }

        if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
        {
            dayEnter = false;
        }
    }

    private void Update()
    {
        if (nightEnter && dayEnter)
        {
            levelFinished = true;
        }

        if (levelFinished)
        {
            finished.SetActive(true);
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            es.SetSelectedGameObject(null);
            es.SetSelectedGameObject(es.firstSelectedGameObject);
            night.GetComponent<PlayerMovementTest>().canMove = false;
            day.GetComponent<PlayerMovementTest>().canMove = false;
            nightEnter = false;
            dayEnter = false;
            levelFinished = false;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Character Selection");
    }
}
