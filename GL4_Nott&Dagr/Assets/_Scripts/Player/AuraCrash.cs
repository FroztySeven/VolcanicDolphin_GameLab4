using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class AuraCrash : MonoBehaviour
{
    public GameObject nott, dagr;
    
    private Vector3 crashForce;
    private Rigidbody2D nottRB, dagrRB;


    private void Start()
    {
        nottRB = nott.GetComponent<Rigidbody2D>();
        dagrRB = dagr.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NottAura"))
        {
                crashForce = dagr.transform.position - nott.transform.position; // dagr then nott
                crashForce.Normalize();
                
                if (nottRB.velocity.x <= 0)
                {
                    if (Input.GetButton("AuraPushP1"))
                    {
                       dagrRB.AddForce(crashForce * -30, ForceMode2D.Impulse); //change this when i fix the bug
                       Debug.Log("AuraPushP1");
                    }
                }

                if (crashForce.y > 0.01f)
                {
                    dagrRB.velocity *= -1.5f;
                }
                else
                {
                    dagr.GetComponent<PlayerMovementTest>().enabled = false;
                    dagrRB.velocity *= -5;
                    StartCoroutine(turnOnMovement());
                }
        }
        
        if (other.gameObject.CompareTag("DagrAura"))
        {
            crashForce = nott.transform.position - dagr.transform.position; // nott then dagr
            crashForce.Normalize();
            
            if (dagrRB.velocity.x <= 0)
            {
                if (Input.GetButton("AuraPushP2"))
                {
                    nottRB.AddForce(crashForce * -30, ForceMode2D.Impulse);
                    Debug.Log("AuraPushP2");
                }
            }

            if (crashForce.y > 0.01f) 
            { 
                nottRB.velocity *= -1.5f;
            }
            else
            { 
                nott.GetComponent<PlayerMovementTest>().enabled = false;
                nottRB.velocity *= -5;
                StartCoroutine(turnOnMovement());
            }
        }
    }

    private IEnumerator turnOnMovement()
    {
        yield return new WaitForSeconds(0.15f);
        dagr.GetComponent<PlayerMovementTest>().enabled = true;
        nott.GetComponent<PlayerMovementTest>().enabled = true;
    }
    
}
