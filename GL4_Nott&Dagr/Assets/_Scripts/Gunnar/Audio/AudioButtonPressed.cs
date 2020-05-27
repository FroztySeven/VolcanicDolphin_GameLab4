using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButtonPressed : MonoBehaviour
{
    //... this script is used to play sounds when interacting with UI, it should only play in the main menu and the pause menu.
    //--------------------------------------------------------------------------------------//

    [FMODUnity.EventRef]
    public string buttonPressed;

    private void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(buttonPressed);
        }

        if (Input.GetButtonDown("Submit"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(buttonPressed);
        }
    }
}
