using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    
    public bool disappearBehindNott = false, disappearBehindDagr = false;
    
    [Header("Start Disappeared")]
    public bool startDisappeared = false;
    public bool turnOnByNott = false, turnOnByDagr = false;
    
    private bool nottInsideTrigger = false, dagrInsideTrigger = false;
    private GameObject nott, dagr;
    private Vector3 nottStartPosition, dagrStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        nott = GameObject.Find("Player2");
        dagr = GameObject.Find("Player1");
        nottStartPosition = nott.transform.position;
        dagrStartPosition = dagr.transform.position;
        
        if (startDisappeared)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponentInChildren<Renderer>().enabled = false;
                child.GetComponentInChildren<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void Update()
    {
        if (!dagrInsideTrigger)
        {
            dagrStartPosition = dagr.transform.position;
        }

        if (!nottInsideTrigger)
        {
            nottStartPosition = nott.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); 
            if (disappearBehindNott)
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        child.GetComponentInChildren<Renderer>().enabled = false;
                        if (child.GetComponentInChildren<BoxCollider2D>())
                        {
                            child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                        }
                    }
                }
            }

            if (disappearBehindDagr)
            { 
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day") 
                { 
                    foreach (Transform child in gameObject.transform) 
                    { 
                        child.GetComponentInChildren<Renderer>().enabled = false;
                        if (child.GetComponentInChildren<BoxCollider2D>())
                        {
                            child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                        }
                    }
                    
                }
            }

            if (startDisappeared) //platform start disappeared
            {
                if (turnOnByNott) 
                { 
                    if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night") //night turns on a platform
                    { 
                        foreach (Transform child in gameObject.transform) 
                        { 
                            child.GetComponentInChildren<Renderer>().enabled = true;
                            if (child.GetComponentInChildren<BoxCollider2D>())
                            {
                                child.GetComponentInChildren<BoxCollider2D>().enabled = true;
                            }
                        }
                    }
                }

                if (turnOnByDagr)
                {
                    if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day") //day turns on a platform
                    {
                        foreach (Transform child in gameObject.transform)
                        {
                            child.GetComponentInChildren<Renderer>().enabled = true;
                            if (child.GetComponentInChildren<BoxCollider2D>())
                            {
                                child.GetComponentInChildren<BoxCollider2D>().enabled = true;
                            }
                        }
                    }
                } 
            }

            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dagrInsideTrigger = true;
                if (nottInsideTrigger)
                {
                    nott.transform.position = nottStartPosition;
                }
            }

            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                nottInsideTrigger = true;
                if (dagrInsideTrigger)
                {
                    dagr.transform.position = dagrStartPosition;
                }
            }
                
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (startDisappeared)
        {
            if (!nottInsideTrigger || !dagrInsideTrigger)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.GetComponentInChildren<Renderer>().enabled = false;
                    if (child.GetComponentInChildren<BoxCollider2D>())
                    {
                        child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }
        else
        {
            if (!nottInsideTrigger || !dagrInsideTrigger)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.GetComponentInChildren<Renderer>().enabled = true;
                    if (child.GetComponentInChildren<BoxCollider2D>())
                    {
                        child.GetComponentInChildren<BoxCollider2D>().enabled = true;
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                nottInsideTrigger = false;
            }
            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                dagrInsideTrigger = false;
            }
        }
    }
}
