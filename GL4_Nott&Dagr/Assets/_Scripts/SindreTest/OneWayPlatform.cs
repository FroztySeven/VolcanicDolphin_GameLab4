﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D _platformEffector;

    private float _startJumpforce;

    // Start is called before the first frame update
    void Start()
    {
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) //if the player enters the trigger above the two way platform
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _startJumpforce = other.GetComponent<PlayerController>().jumpForce;
            if (_startJumpforce <= 0)
            {
                _startJumpforce = 16;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetAxis("VerticalP" + other.GetComponent<PlayerController>().playerId) <= -0.9)
            {
                other.GetComponent<PlayerController>().jumpForce = 0; // disable the jumping if the joystick is held downwards

                if (other.GetComponent<PlayerController>().isGrounded && Input.GetAxis("VerticalP" + other.GetComponent<PlayerController>().playerId) <= -0.9 && 
                    Input.GetButton("JumpP" + other.GetComponent<PlayerController>().playerId)) //if the joystick is held downwards and the player presses the jump button
                {
                    _platformEffector.rotationalOffset = 180; //rotate the platform effector 180 degrees so the player can fall through it from the top
                    StartCoroutine(rotateBack());
                }
            }
            else
            {
                other.GetComponent<PlayerController>().jumpForce = _startJumpforce;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().jumpForce = _startJumpforce; // enable the jumping again
        }
    }

    private IEnumerator rotateBack()
    {
        yield return new WaitForSeconds(0.5f);
        _platformEffector.rotationalOffset = 0; // turn the platform effector back so you can jump through it from the bottom
    }
}
