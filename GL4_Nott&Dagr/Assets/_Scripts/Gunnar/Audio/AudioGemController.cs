using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGemController : MonoBehaviour
{
    // This controls the audio events for the gem gameobject, when the players interact with it, when Nótt touches the gem it will play the freeze sounds
    // and when Dagr touches it it should play the thaw/melt sounds.

    [FMODUnity.EventRef]
    public string freezeGem;

    [FMODUnity.EventRef]
    public string unfreezeGem;

    [HideInInspector]
    public GameObject dagrTrigger, nottTrigger;

    [HideInInspector]
    public bool freeze, unfreeze;

    private void Awake()
    {
        dagrTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
        nottTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;

        freeze = true;
        unfreeze = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == dagrTrigger && freeze)
        {
            freeze = false;
            unfreeze = true;
            FMODUnity.RuntimeManager.PlayOneShot(unfreezeGem);
        }

        if (other.gameObject == nottTrigger && unfreeze)
        {
            freeze = true;
            unfreeze = false;
            FMODUnity.RuntimeManager.PlayOneShot(freezeGem);
        }
    }
}
