using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameObject finished, portalSwirl, loadingScreen;

    public ParticleSystem portalParticle;
    [HideInInspector] //--Changed private to public to let audiotrigger know level is over-- Gunnar
    public bool nightEnter, dayEnter, levelFinished, loadingScreenIsActive;
    [HideInInspector]
    public bool playerPixelate;

    private GameObject night, day, tempLoadingScreen;

    private int nextSceneLoad;

    private void Awake()
    {
        name = "Door";
    }

    private void Start()
    {
        portalSwirl.SetActive(false);
        night = GameObject.Find("Player2");
        day = GameObject.Find("Player1");

        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == night || other.gameObject == day)
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                nightEnter = true;
                other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dayEnter = true;
                other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
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
                other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dayEnter = false;
                other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
            }
        }
    }

    private void Update()
    {
        if (nightEnter && dayEnter)
        {
            night.GetComponent<PlayerMovementTest>().canMove = false;
            day.GetComponent<PlayerMovementTest>().canMove = false;
            night.GetComponent<PlayerMovementTest>().theRB.velocity = new Vector2(0f, -10f);
            day.GetComponent<PlayerMovementTest>().theRB.velocity = new Vector2(0f, -10f);
            //levelFinished = true;
            playerPixelate = true;
            Invoke("LevelFinished", 5f);
            StartCoroutine(instantiateLoadingScreen());
        }

        if (levelFinished)
        {
            //finished.SetActive(true);
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            es.SetSelectedGameObject(null);
            es.SetSelectedGameObject(es.firstSelectedGameObject);
            //night.GetComponent<PlayerMovementTest>().canMove = false;
            //day.GetComponent<PlayerMovementTest>().canMove = false;
            nightEnter = false;
            dayEnter = false;
            levelFinished = false;


            //Move to next level
            // SceneManager.LoadScene(nextSceneLoad);
            //Setting Int for Index
            // if (nextSceneLoad > PlayerPrefs.GetInt("LevelPrefs"))
            // {
            //     PlayerPrefs.SetInt("LevelPrefs", nextSceneLoad);
            // }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DoorOpen()
    {
        portalSwirl.SetActive(true);
        portalParticle.Play();
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void LevelFinished()
    {
        levelFinished = true;
        playerPixelate = false;
    }

    private IEnumerator instantiateLoadingScreen()
    {
        yield return new WaitForSeconds(4);
        if (!loadingScreenIsActive)
        {
            tempLoadingScreen = Instantiate(loadingScreen);
            StartCoroutine(nextLevel());
            loadingScreenIsActive = true;
        }
        
    }
    
    private IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextSceneLoad);
        //Setting Int for Index
        if (nextSceneLoad > PlayerPrefs.GetInt("LevelPrefs"))
        {
            PlayerPrefs.SetInt("LevelPrefs", nextSceneLoad);
        }
    }
}
