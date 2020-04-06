using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPushObjectsController : MonoBehaviour
{
    public bool isSliding;

    private Rigidbody2D _boxRB;

    private FMOD.Studio.EventInstance pushSound;

    // Start is called before the first frame update
    void Start()
    {
        isSliding = false;
        _boxRB = gameObject.GetComponent<Rigidbody2D>();
        pushSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/PushSlide");
    }

    // Update is called once per frame
    void Update()
    {
        float _velMag = _boxRB.velocity.magnitude;

        if (Mathf.Abs(_velMag) < 0.1f)
        {
            isSliding = false;
            StopSound();
        }
        else
        {
            isSliding = true;
            PlaySound();
        }

        Debug.Log(_velMag);
        Debug.Log(IsPlaying(pushSound));
    }

    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    private void PlaySound()
    {
        pushSound.start();
    }

    private void StopSound()
    {
        if (IsPlaying(pushSound))
        {
            //pushSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            pushSound.release();
        }
    }
}
