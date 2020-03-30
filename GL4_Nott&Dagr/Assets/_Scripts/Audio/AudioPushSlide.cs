using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPushSlide : MonoBehaviour
{
    [FMODUnity.EventRef] public string slideDrag;

    public Rigidbody2D rbVel;

    public bool isPushingD, isPushingN, isSliding;

    private FMOD.Studio.EventInstance slidInstance;

    // Start is called before the first frame update
    void Start()
    {
        rbVel = this.gameObject.GetComponent<Rigidbody2D>();
        isPushingN = false;
        slidInstance = FMODUnity.RuntimeManager.CreateInstance(slideDrag);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushingN == true)
        {
            if (rbVel.velocity.x <= -8.00f && rbVel.velocity.x >= -8.05f || rbVel.velocity.x >= 8.00f && rbVel.velocity.x <= 8.05f)
            {
                Debug.Log("Sliding");
                slidInstance.start();
            }
            else
            {
                slidInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Debug.Log("Stopped Sliding");
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.Find("Player1"))
        {
            isPushingD = true;
        }
        if (other.gameObject == GameObject.Find("Player2"))
        {
            isPushingN = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == GameObject.Find("Player1"))
        {
            isPushingD = true;
        }
        if (other.gameObject == GameObject.Find("Player2"))
        {
            isPushingN = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == GameObject.Find("Player1"))
        {
            isPushingD = false;
        }
        if (other.gameObject == GameObject.Find("Player2"))
        {
            isPushingN = false;
        }
    }
}
