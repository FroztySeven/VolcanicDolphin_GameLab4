using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;

public class AudioWaterController : MonoBehaviour
{
    // This script is used on the water gameobject, it is used to play two audio events. It will play a water splash when Dagr falls into the water and it
    // will play water freezing sfx when Nótt hits the water.

    [HideInInspector]
    public Water _water;
    [HideInInspector]
    public GameObject dagrTrigger, nottTrigger;
    [HideInInspector]
    
    [FMODUnity.EventRef]
    public string waterFreezes, waterSplash;

    void Start()
    {
          dagrTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
          nottTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;

          _water = gameObject.GetComponent<Water>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) // Checks which audio trigger hit the water.
    {
        if (other.gameObject == nottTrigger && !_water.isFrozen)
        {
            CallWaterFreezes();
        }

        if (other.gameObject == dagrTrigger && !_water.isFrozen)
        {
            CallWaterSplash();
        }
    }

    void CallWaterFreezes()
    {
        FMODUnity.RuntimeManager.PlayOneShot(waterFreezes);
    }

    void CallWaterSplash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(waterSplash);
    }
}
