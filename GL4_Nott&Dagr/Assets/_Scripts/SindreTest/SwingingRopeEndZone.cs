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
    private bool isSwinging, onRopeDSFX, onRopeNSFX;
    private Rigidbody2D parentRB;
    //---Audio Addon---//

    // Start is called before the first frame update
    void Start()
    {
        dagrGO = GameObject.Find("Player1");
        nottGO = GameObject.Find("Player2");
        
        _sr = gameObject.GetComponentInParent<SwingingRope>();

        //----Audio Addon-----//
        parentRB = this.gameObject.GetComponentInParent<Rigidbody2D>();
        //----Audio Addon-----//
    }


    private void Update()
    {
        if (dagrOnRope)
        {
            if (_sr.dagr || _sr.both)
            {
                dagrGO.transform.SetParent(this.transform);
                dagrGO.transform.position = transform.position;
                
                if (Input.GetButtonDown("JumpP" + dagrGO.GetComponent<PlayerController>().playerId))
                {
                    dagrOnRope = false;
                    dagrGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    dagrGO.transform.SetParent(null);
                    StartCoroutine(turnOnTriggerAgain());
                }
                //----Audio Addon-----//
                if (parentRB.velocity.x >= -7.55f && parentRB.velocity.x <= -7.45f || parentRB.velocity.x <= 7.55f && parentRB.velocity.x >= 7.45f)
                {
                    isSwinging = true;
                    if (isSwinging)
                    {
                        StartCoroutine(SwingWooshSound());
                    }
                }
                //----Audio Addon-----//
            }
        }

        if (nottOnRope)
        {
            if (_sr.nott || _sr.both)
            {
                nottGO.transform.SetParent(this.transform);
                nottGO.transform.position = transform.position;
                
                if (Input.GetButtonDown("JumpP" + nottGO.GetComponent<PlayerController>().playerId))
                {
                    nottOnRope = false;
                    nottGO.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    nottGO.transform.SetParent(null);
                    StartCoroutine(turnOnTriggerAgain());
                }
                //----Audio Addon-----//
                if (parentRB.velocity.x >= -7.55f && parentRB.velocity.x <= -7.45f || parentRB.velocity.x <= 7.55f && parentRB.velocity.x >= 7.45f)
                {
                    isSwinging = true;
                    if (isSwinging)
                    {
                        StartCoroutine(SwingWooshSound());
                    }
                }
                //----Audio Addon-----//
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                {
                    dagrOnRope = true;
                    Debug.Log("DAGR ON ROPE");
                    
                    //----Audio Addon-----//
                    onRopeDSFX = true;
                    if (dagrOnRope && onRopeDSFX)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(_sr.ropeTwist);
                        onRopeDSFX = false;
                    }

                    //----Audio Addon-----//
                }

                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    nottOnRope = true;

                    //----Audio Addon-----//
                    onRopeNSFX = true;
                    if (nottOnRope && onRopeNSFX)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(_sr.ropeTwist);
                        onRopeNSFX = false;
                    }

                    //----Audio Addon-----//
                }
            }
        }

        isTriggered = true;
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_sr.dagr)
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
                {
                    other.gameObject.transform.SetParent(this.transform);
                    other.transform.position = this.transform.position;

                    if (Input.GetButtonDown("JumpP" + other.gameObject.GetComponent<PlayerMovementTest>().playerId))
                    {
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        other.transform.SetParent(null);
                        StartCoroutine(turnOnTriggerAgain());
                    }
                }
            }
            if (_sr.nott)
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
                {
                    other.gameObject.transform.SetParent(this.transform);
                    other.transform.position = this.transform.position;

                    if (Input.GetButtonDown("JumpP" + other.gameObject.GetComponent<PlayerMovementTest>().playerId))
                    {
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        other.transform.SetParent(null);
                        StartCoroutine(turnOnTriggerAgain());
                    }
                }
            }
            
            if (_sr.both)
            { 
                other.gameObject.transform.SetParent(this.transform);
                other.transform.position = transform.position;

                if (Input.GetButtonDown("JumpP" + other.gameObject.GetComponent<PlayerMovementTest>().playerId)) 
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    other.transform.SetParent(null);
                    StartCoroutine(turnOnTriggerAgain());
                } 
            }
        }
    }*/


    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(turnOnTriggerAgain());

            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dagrOnRope = false;
            }
            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                nottOnRope = false;
            }
        }
    }*/

    public IEnumerator turnOnTriggerAgain()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        isTriggered = false;
    }

    //---Audio Addon---//
    private IEnumerator SwingWooshSound()
    {
        isSwinging = false;
        yield return new WaitForSeconds(0.1f);
        FMODUnity.RuntimeManager.PlayOneShot(_sr.ropeWoosh);
        yield return new WaitForSeconds(0.5f);
        FMODUnity.RuntimeManager.PlayOneShot(_sr.ropeTwist);
    }
    //---Audio Addon---//
}
