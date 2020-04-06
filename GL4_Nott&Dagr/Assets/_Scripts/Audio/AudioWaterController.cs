using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioWaterController : MonoBehaviour
{
    //[HideInInspector]
    public GameObject dagrTrigger, nottTrigger, gem;
    [HideInInspector]
    public Sprite waterSprite, frozenSprite;
    [HideInInspector]
    public int typeOfSprite;
    [HideInInspector]
    public bool isWater, isFrozen;

    [FMODUnity.EventRef]
    public string waterFreezes, waterSplash;

    void Start()
    {
          dagrTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
          nottTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;
          gem = GameObject.Find("Gem");
    }

    void Update()
    {

        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == waterSprite)
        {
            typeOfSprite = 1;
            isWater = true;
            isFrozen = false;
        }

        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == frozenSprite)
        {
            typeOfSprite = 2;
            isWater = false;
            isFrozen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == nottTrigger && isWater)
        {
            CallWaterFreezes();
            isWater = false;
        }

        if (other.gameObject == dagrTrigger && isWater)
        {
            CallWaterSplash();
        }
        
        if (other.gameObject == gem && isWater)
        {
            CallWaterSplash();
            Debug.Log("Hit water gem");
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
