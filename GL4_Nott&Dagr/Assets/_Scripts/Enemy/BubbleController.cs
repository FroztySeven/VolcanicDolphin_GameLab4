using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [Header("Change the bubble move speed")]
    public float speed = 1.5f;

    [Header("Bubble tile ground collision detector")]
    public bool hasCollided;

    // Update is called once per frame
    [HideInInspector]
    public int bubbleMoveInt = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.GetChild(0).gameObject)
        {
            if (collision.gameObject.CompareTag("TM_Ground"))
            {
                hasCollided = true;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                if (bubbleMoveInt == 0)
                {
                    bubbleMoveInt = 1;
                }
            }
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
                hasCollided = false;

                bubbleMoveInt = 2;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                bubbleMoveInt = 0;
            }
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                bubbleMoveInt = 0;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    
    
    void Update()
    {
        if (!hasCollided)
        {
            if (bubbleMoveInt == 1)
            {

            }

            else if (bubbleMoveInt == 2)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
            }

            else if (bubbleMoveInt == 0)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
