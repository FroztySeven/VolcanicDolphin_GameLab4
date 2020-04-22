using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlidingObjectController : MonoBehaviour
{
    public bool audioPlaying, isGrounded, wasGrounded, isFalling;

    private float height, oldHeight, heightDiff, distToGround;

    private Rigidbody2D _boxRB;

    private Transform _gemTrans;

    private FMOD.Studio.EventInstance pushSound;

    void Start()
    {
        _boxRB = gameObject.GetComponent<Rigidbody2D>();
        _gemTrans = gameObject.GetComponent<Transform>();
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        pushSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/PushSlide");
    }

    void FixedUpdate()
    {
        
        wasGrounded = isGrounded;

        CheckFalling();

        if (wasGrounded == false)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    void Update()
    {
        if (_boxRB.velocity.y >= 0.1f || _boxRB.velocity.y <= -0.1f)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        if (isGrounded)
        {
            if (_boxRB.velocity.magnitude > 0.1f)
            {
                audioPlaying = true;

                if (!IsPlaying(pushSound))
                {
                    if (audioPlaying)
                    {
                        pushSound.start();
                    }
                }
            }
            else
            {
                audioPlaying = false;
                pushSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else
        {
            pushSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    void CheckFalling()
    {
        oldHeight = height;

        height = _gemTrans.position.y;

        heightDiff = height - oldHeight;

        if (heightDiff > 0)
        {
            heightDiff = heightDiff * -1;
        }
    }

    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    private void OnDestroy()
    {
        pushSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
