using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuBackButton : MonoBehaviour
{

    //When the player presses the B button on the xbox controller, go back to MainMenu scene
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {

            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            es.SetSelectedGameObject(null);
            es.SetSelectedGameObject(es.firstSelectedGameObject);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
