using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SwingingRopeEndZone : MonoBehaviour
{
    private SwingingRope _sr;

    private GameObject dagrGO, nottGO;

    //[HideInInspector]
    public bool dagrOnRope = false, nottOnRope = false, isTriggered;

    //---Audio Addon---//
    public Rigidbody2D parentRB;
    public bool ropeSwinging, ropeTwisting;
    private FMOD.Studio.EventInstance ropeSwing;
    private FMOD.Studio.EventInstance ropeTwist;
    //---Audio Addon---//

    // Start is called before the first frame update
    void Start()
    {
        dagrGO = GameObject.Find("Player1");
        nottGO = GameObject.Find("Player2");
        
        _sr = gameObject.GetComponentInParent<SwingingRope>();

        //----Audio Addon-----//
        parentRB = this.gameObject.GetComponentInParent<Rigidbody2D>();
        ropeSwing = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/RopeSwing");
        ropeTwist = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/RopeTwist");
        //----Audio Addon-----//
    }


    private void Update()
    {
        if (dagrOnRope)
        {
            if (_sr.dagr || _sr.both) //check if the bool in the swinging rope scripts allow dagr on the rope
            {
                dagrGO.transform.SetParent(this.transform);
                dagrGO.transform.position = transform.position;
                
                if (Input.GetButtonDown("JumpP" + dagrGO.GetComponent<PlayerController>().playerId)) // if dagr jumps, disconnect from the rope
                {
                    dagrOnRope = false;
                    dagrGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    dagrGO.transform.SetParent(null);
                    StartCoroutine(turnOnTriggerAgain());
                }
            }
        }

        if (nottOnRope)
        {
            if (_sr.nott || _sr.both) //check if the bool in the swinging rope scripts allow Nott on the rope
            {
                nottGO.transform.SetParent(this.transform);
                nottGO.transform.position = transform.position;
                
                if (Input.GetButtonDown("JumpP" + nottGO.GetComponent<PlayerController>().playerId)) //if nott jumps, disconnect from the rope
                {
                    nottOnRope = false;
                    nottGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    nottGO.transform.SetParent(null);
                    StartCoroutine(turnOnTriggerAgain());
                }
            }
        }

        //---- Audio Addon ----//
        RopeAudio();
        //---- Audio Addon ----//
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day") //if dagr enters the trigger on the bottom of the rope
                {
                    dagrOnRope = true;
                }

                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night") //if nott enters the trigger on the bottom of the rope
                {
                    nottOnRope = true;
                }
            }
        }

        isTriggered = true;
    }
    

    public IEnumerator turnOnTriggerAgain() //the trigger is disabled becuase of issues when jumping off the rope. So here we enable it again
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        isTriggered = false;
    }

    //---- Audio Addon ----//
    void RopeAudio()
    {
        //---- Rope Swing ----//

        if (parentRB.velocity.magnitude > 6f)
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

        if (parentRB.velocity.magnitude > 7f)
        {
            ropeSwinging = false;
        }

        //---- Rope Twist ----//

        if (parentRB.velocity.magnitude > 3f)
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
    //---- Audio Addon ----//
}
