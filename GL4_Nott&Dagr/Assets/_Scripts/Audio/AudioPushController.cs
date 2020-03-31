using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioPushController : MonoBehaviour
{
    public StudioEventEmitter _see;

    private Rigidbody2D rbVel;

    public bool isGrounded, isSliding;

    // Start is called before the first frame update
    void Start()
    {
        _see = this.gameObject.GetComponent<StudioEventEmitter>();
        rbVel = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbVel.velocity.y < -0.1 || rbVel.velocity.y > 0.1)
        {
            isGrounded = false;
        }
        else if (rbVel.velocity.x > -0.1 || rbVel.velocity.x < 0.1)
        {
            isGrounded = true;
        }

        if (isGrounded == true)
        {
            if (rbVel.velocity.x < -0.1 || rbVel.velocity.x > 0.1)
            {
                isSliding = true;
            }
            else if (rbVel.velocity.x > -0.1 || rbVel.velocity.x < 0.1)
            {
                isSliding = false;
            }
        }

        if (isGrounded == false)
        {
            _see.SendMessage("Stop");
        }

        if (!isSliding)
        {
            _see.SendMessage("Play");
        }

        _see.StopEvent = EmitterGameEvent.ObjectDestroy;

        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            _see.SendMessage("Stop");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            _see.SendMessage("Stop");
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button6))
        {
            _see.SendMessage("Stop");
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button7))
        {
            _see.SendMessage("Stop");
        }
    }
}