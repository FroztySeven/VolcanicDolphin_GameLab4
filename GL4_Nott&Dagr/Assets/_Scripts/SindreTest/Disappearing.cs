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
        nottStartPosition = nott.transform.position; // find the position the players start in so if they get stuck in the wall we can reset them.
        dagrStartPosition = dagr.transform.position;
        
        if (startDisappeared) // if the wall is starting disappeared i turn it off here
        {
            foreach (Transform child in gameObject.transform)
            {
                child.transform.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (!dagrInsideTrigger) // update the "startposition" of each player to reset them to their newest position
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
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        child.transform.gameObject.SetActive(false);
                    }
                }
                else if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        child.transform.gameObject.SetActive(true);
                    }
                }
            }

            if (disappearBehindDagr)
            { 
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day") 
                { 
                    foreach (Transform child in gameObject.transform) 
                    { 
                        child.transform.gameObject.SetActive(false);
                    }
                }
                else if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        child.transform.gameObject.SetActive(true);
                    }
                }
            }

            if (disappearBehindDagr && disappearBehindNott)
            {
                foreach (Transform child in gameObject.transform) 
                { 
                    child.transform.gameObject.SetActive(false);
                }
            }

            if (startDisappeared) //platform start disappeared
            {
                if (turnOnByNott) 
                { 
                    if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night") //night turns on a object
                    { 
                        foreach (Transform child in gameObject.transform) 
                        { 
                            child.transform.gameObject.SetActive(true);
                        }
                    }
                    else if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                    { //turns off object when day enters the trigger
                        foreach (Transform child in gameObject.transform) 
                        { 
                            child.transform.gameObject.SetActive(false);
                        }
                    }
                }

                if (turnOnByDagr)
                {
                    if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day") //day turns on a object
                    {
                        foreach (Transform child in gameObject.transform)
                        {
                            child.transform.gameObject.SetActive(true);
                        }
                    }
                    else if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                    { //turn off object when night tries to enter
                        foreach (Transform child in gameObject.transform) 
                        { 
                            child.transform.gameObject.SetActive(false);
                        }
                    }
                } 
            }

            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            { //if day is inside the trigger and night tries to enter, have to move night so she does not get stuck inside the wall
                dagrInsideTrigger = true;
                if (nottInsideTrigger)
                {
                    nott.transform.position = nottStartPosition;
                }
            }

            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            { //same as above just with day instead of night
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
        if (startDisappeared) //turn off object after one of them leave the trigger
        {
            if (!nottInsideTrigger || !dagrInsideTrigger)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.transform.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (!nottInsideTrigger || !dagrInsideTrigger) //turn on object after one of them leave the trigger
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.transform.gameObject.SetActive(true);
                }
            }
        }

        if (other.gameObject.CompareTag("Player")) //turn off the correct boolean
        {
            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                nottInsideTrigger = false;
            }
            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                dagrInsideTrigger = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                if (disappearBehindNott)
                {
                    nottInsideTrigger = true;
                    foreach (Transform child in gameObject.transform)
                    {
                        child.transform.gameObject.SetActive(false);
                    }
                }
            }

            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                if (disappearBehindDagr)
                {
                    dagrInsideTrigger = true;
                    foreach (Transform child in gameObject.transform)
                    {
                        child.transform.gameObject.SetActive(false);
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            foreach (Transform child in gameObject.transform)
            {
                child.transform.gameObject.SetActive(false);
            }
        }
    }
}
