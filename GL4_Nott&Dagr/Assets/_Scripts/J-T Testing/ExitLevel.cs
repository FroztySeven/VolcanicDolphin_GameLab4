using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameObject finished, portalSwirl, loadingScreen;

    public ParticleSystem portalParticle, fireNigth, fireDay;
    private ParticleSystem sparksNight, sparksDay;
    private ParticleSystem.MainModule fireNightMain, fireDayMain;
    private ParticleSystem.MainModule sparksNightMain, sparksDayMain;

    //public Transform firePosNight, firePosDay;

    [HideInInspector] //--Changed private to public to let audiotrigger know level is over-- Gunnar
    public bool nightEnter, dayEnter, bothEntered, levelFinished, loadingScreenIsActive;

    [HideInInspector] public bool playerPixelate;

    private GameObject night, day, tempLoadingScreen;

    [Header("Which level scene should load and unlock next?")]
    public string nextLevelSceneName;
    public int unlockLevelNumber;

    private void Awake()
    {
        name = "Door";
    }

    private void Start()
    {
        portalSwirl.SetActive(false);
        night = GameObject.Find("Player2");
        day = GameObject.Find("Player1");

        fireNightMain = fireNigth.main;
        fireDayMain = fireDay.main;

        sparksNight = fireNigth.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        sparksDay = fireDay.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();

        sparksNightMain = sparksNight.main;
        sparksDayMain = sparksDay.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Aura")
        {
            other.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }

        if (other.gameObject == night || other.gameObject == day)
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                nightEnter = true;
                //other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
                fireNigth.Play();
                fireNightMain.loop = true;
                sparksNight.Play();
                sparksNightMain.loop = true;
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                dayEnter = true;
                //other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
                fireDay.Play();
                fireDayMain.loop = true;
                sparksDay.Play();
                sparksDayMain.loop = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Aura")
        {
            other.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }

        if (other.gameObject == night || other.gameObject == day)
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                nightEnter = false;
                //other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
                fireNightMain.loop = false;
                sparksNightMain.loop = false;
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                dayEnter = false;
                //other.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
                fireDayMain.loop = false;
                sparksDayMain.loop = false;
            }
        }
    }

    private void Update()
    {
        if (nightEnter && dayEnter && night.GetComponent<PlayerController>().isGrounded && day.GetComponent<PlayerController>().isGrounded)
        {
            night.GetComponent<PlayerController>().playerCanMove = false;
            night.GetComponent<PlayerController>().playerCanJump = false;
            day.GetComponent<PlayerController>().playerCanMove = false;
            day.GetComponent<PlayerController>().playerCanJump = false;
            night.GetComponent<CapsuleCollider2D>().isTrigger = true;
            day.GetComponent<CapsuleCollider2D>().isTrigger = true;
            night.GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
            night.GetComponent<PlayerController>().theRB.gravityScale = 0f;
            day.GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
            day.GetComponent<PlayerController>().theRB.gravityScale = 0f;
            night.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            day.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            //night.GetComponent<PlayerController>().theRB.velocity = new Vector2(0f, -10f);
            //day.GetComponent<PlayerController>().theRB.velocity = new Vector2(0f, -10f);
            playerPixelate = true;
            Invoke("LevelFinished", 7f); //Origanl 5f GB
            StartCoroutine(instantiateLoadingScreen());
            bothEntered = true;
            nightEnter = false;
            dayEnter = false;
        }

        if (bothEntered)
        {
            night.transform.position = Vector3.Lerp(night.transform.position, transform.position - new Vector3(0.5f, 0.15f, 0f), 2f * Time.deltaTime);
            day.transform.position = Vector3.Lerp(day.transform.position, transform.position + new Vector3(0.5f, -0.15f, 0f), 2f * Time.deltaTime);
        }

        //Cheat complete level
        if (Input.GetKeyDown(KeyCode.V))
        {
            tempLoadingScreen = Instantiate(loadingScreen);
            StartCoroutine(nextLevel());
            loadingScreenIsActive = true;
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
        yield return new WaitForSeconds(6); //Origanl 4f GB
        if (!loadingScreenIsActive)
        {
            tempLoadingScreen = Instantiate(loadingScreen);
            StartCoroutine(nextLevel());
            loadingScreenIsActive = true;
        }

    }

    private IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(2f); //Origanl 2f GB

        if (unlockLevelNumber > PlayerPrefs.GetInt("LevelPrefs", 1))
        {
            PlayerPrefs.SetInt("LevelPrefs", unlockLevelNumber);
        }

        SceneManager.LoadScene(nextLevelSceneName);
    }
}
