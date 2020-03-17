using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioWaterController : MonoBehaviour
{
    public Sprite waterSprite, frozenSprite;

    public int typeOfSprite;

    public bool isWater, isFrozen;

    [FMODUnity.EventRef]                //... find the path to the fmod event.
    public string waterFreezes;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
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
        if (other.gameObject == GameObject.FindGameObjectWithTag("NottAudioTrigger") && isWater == true)
        {
            // Debug.Log("Hit Water");
            CallWaterFreezes();
            isWater = false;
        }
    }

    void CallWaterFreezes()
    {
        FMODUnity.RuntimeManager.PlayOneShot(waterFreezes);
    }
}
