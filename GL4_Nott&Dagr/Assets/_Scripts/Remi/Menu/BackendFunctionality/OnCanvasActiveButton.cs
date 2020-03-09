using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnCanvasActiveButton : MonoBehaviour
{
    public GameObject whichButtonIsFirstSelected;

    // Start is called before the first frame update
    public void Start()
    {
        //Lock mouse cursor so only controller input can be used
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //When the canvas is enabled, return focus back to the first canvas button
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(whichButtonIsFirstSelected);
    }

    private void Update()
    {
        // If the user intentionally or accidentally mouse clicks, then return focus back to the first selected canvas button 
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            es.SetSelectedGameObject(whichButtonIsFirstSelected);
        }
    }
}
