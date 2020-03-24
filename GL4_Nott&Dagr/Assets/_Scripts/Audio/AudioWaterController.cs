using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioWaterController : MonoBehaviour
{
    [HideInInspector]
    public GameObject nottTrigger;
    [HideInInspector]
    public Sprite waterSprite, frozenSprite;
    [HideInInspector]
    public int typeOfSprite;
    [HideInInspector]
    public bool isWater, isFrozen;

    [FMODUnity.EventRef]
    public string waterFreezes;

    void Start()
    {
          nottTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;
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
    }

    void CallWaterFreezes()
    {
        FMODUnity.RuntimeManager.PlayOneShot(waterFreezes);
    }
}
