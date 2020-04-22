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

    private void OnTriggerEnter2D(Collider2D other)
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
