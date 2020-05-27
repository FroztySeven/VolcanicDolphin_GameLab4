using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBatteringRamBoomController : MonoBehaviour
{
    // This script is for the swinging platforms/battering rams. It is used to play an impact sound when platforms hits the breakable walls.

    private FMOD.Studio.EventInstance wallBoom;

    // Start is called before the first frame update
    void Start()
    {
        wallBoom = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/BatRamBoom"); // This is the event from the FMod project
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) // On collision with breakable wall, it plays the  impact sound.
    {
        if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("Hit Wall Boom");
            wallBoom.start();
            wallBoom.release();
        }
    }
}
