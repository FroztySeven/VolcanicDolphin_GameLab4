using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChapterBackButton : MonoBehaviour
{
    public GameObject Canvas_ChapterSelection;

    //When the player presses the B button on the xbox controller, go back to Chapters&Levels scene
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            this.gameObject.SetActive(false);
            Canvas_ChapterSelection.SetActive(true);

            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            SceneManager.LoadScene("Chapters&Levels");
        }
    }
}
