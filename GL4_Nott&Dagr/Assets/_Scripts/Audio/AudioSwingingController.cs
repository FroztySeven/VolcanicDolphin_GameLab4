using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwingingController : MonoBehaviour
{
    public bool ropeSwinging, ropeTwisting;

    public Rigidbody2D objectRB;

    private FMOD.Studio.EventInstance ropeSwing;
    private FMOD.Studio.EventInstance ropeTwist;

    // Start is called before the first frame update
    void Start()
    {
        objectRB = gameObject.GetComponent<Rigidbody2D>();
        
        ropeSwing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/RopeSwing");
        ropeTwist = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/RopeTwist");
    }

    // Update is called once per frame
    void Update()
    {
        //---- Rope Swing ----//

        if (objectRB.velocity.magnitude > 6f)
        {
            ropeSwinging = true;

            if (!IsPlaying(ropeSwing))
            {
                if (ropeSwinging)
                {
                    ropeSwing.start();
                }
            }
        }
        else
        {
            ropeSwinging = false;
        }

        if (objectRB.velocity.magnitude > 7f)
        {
            ropeSwinging = false;
        }

        //---- Rope Twist ----//

        if (objectRB.velocity.magnitude > 3f)
        {
            ropeTwisting = true;

            if (!IsPlaying(ropeTwist))
            {
                if (ropeTwisting)
                {
                    ropeTwist.start();
                }
            }
        }
        else
        {
            ropeTwisting = false;
        }
    }

    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
