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

    public Sprite closedDoor, openDoor;

    private void Start()
    {
        name = "Door";
        GetComponent<SpriteRenderer>().sprite = closedDoor;
        night = GameObject.Find("Player2");
        day = GameObject.Find("Player1");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == night || other.gameObject == day)
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                nightEnter = true;
                other.transform.Find("Aura").GetComponent<CircleCollider2D>().enabled = false;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dayEnter = true;
                other.transform.Find("Aura").GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == night || other.gameObject == day)
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                nightEnter = false;
                other.transform.Find("Aura").GetComponent<CircleCollider2D>().enabled = true;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dayEnter = false;
                other.transform.Find("Aura").GetComponent<CircleCollider2D>().enabled = true;
            }
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

    public void DoorOpen()
    {
        GetComponent<SpriteRenderer>().sprite = openDoor;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
