using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGemController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string freezeGem;

    [FMODUnity.EventRef]
    public string unfreezeGem;

    public GameObject dagrTrigger, nottTrigger;

    public bool freeze, unfreeze;

    private void Awake()
    {
        dagrTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
        nottTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        freeze = true;
        unfreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == dagrTrigger && freeze == true)
        {
            freeze = false;
            unfreeze = true;
            FMODUnity.RuntimeManager.PlayOneShot(unfreezeGem);
        }

        if (other.gameObject == nottTrigger && unfreeze == true)
        {
            freeze = true;
            unfreeze = false;
            FMODUnity.RuntimeManager.PlayOneShot(freezeGem);
        }
    }
}
