using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class AuraCrash : MonoBehaviour
{
    public GameObject nott, dagr;
    public int auraPushForce = 10;
    
    private Vector3 crashForce;
    private Rigidbody2D nottRB, dagrRB;

    private string[] connectedControllers;


    private void Start()
    {
        nottRB = nott.GetComponent<Rigidbody2D>();
        dagrRB = dagr.GetComponent<Rigidbody2D>();
        connectedControllers = Input.GetJoystickNames();
        Debug.Log(connectedControllers.Length + "  Controllers");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NottAura"))
        {
                crashForce = dagr.transform.position - nott.transform.position; // dagr then nott
                crashForce.Normalize();
                
                if (nottRB.velocity.x == 0)
                {
                    if (connectedControllers.Length == 1) // if it is only one player
                    {
                        if (Input.GetButton("AuraPushP1"))
                        {
                            nottRB.AddForce(crashForce * (-auraPushForce * 3), ForceMode2D.Impulse); //change this when i fix the bug
                            Debug.Log("AuraPushP2");
                        }
                    }

                    if (connectedControllers.Length == 2) //if 2 controllers is connected.
                    {
                        if (Input.GetButton("AuraPushP2"))
                        {
                            nottRB.AddForce(crashForce * -30, ForceMode2D.Impulse);
                        }
                    }
                    
                }

                if (crashForce.y > 0.01f)
                {
                    dagrRB.velocity *= -1.5f;
                }
                else
                {
                    dagr.GetComponent<PlayerMovementTest>().enabled = false;
                    dagrRB.velocity *= -3;
                    StartCoroutine(turnOnMovement());
                }
        }
        
        if (other.gameObject.CompareTag("DagrAura"))
        {
            crashForce = nott.transform.position - dagr.transform.position; // nott then dagr
            crashForce.Normalize();
            
            if (dagrRB.velocity.x == 0)
            {
                if (Input.GetButton("AuraPushP1"))
                {
                    dagrRB.AddForce(crashForce * auraPushForce, ForceMode2D.Impulse);
                    Debug.Log("AuraPushP1");
                }
            }

            if (crashForce.y > 0.01f) 
            { 
                nottRB.velocity *= -1.5f;
            }
            else
            { 
                nott.GetComponent<PlayerMovementTest>().enabled = false;
                nottRB.velocity *= -3;
                StartCoroutine(turnOnMovement());
            }
        }
    }

    private IEnumerator turnOnMovement()
    {
        yield return new WaitForSeconds(0.10f);
        dagr.GetComponent<PlayerMovementTest>().enabled = true;
        nott.GetComponent<PlayerMovementTest>().enabled = true;
    }
    
}
