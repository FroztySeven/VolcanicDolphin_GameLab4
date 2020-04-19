using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBatteringRamBoomController : MonoBehaviour
{
    private FMOD.Studio.EventInstance wallBoom;
    // Start is called before the first frame update
    void Start()
    {
        wallBoom = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/BatRamBoom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Hit Wall Boom");
            wallBoom.start();
            wallBoom.release();
        }
    }
}
