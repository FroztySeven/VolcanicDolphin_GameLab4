using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    
    public int speed = 12;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public int bubbleMoveInt = 0;
    public bool hasCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.GetChild(0).gameObject)
        {
            if (collision.gameObject)
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
            }
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
                hasCollided = false;

                bubbleMoveInt = 1;
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
                transform.Translate(Vector2.up * Time.deltaTime, Space.World);
            }
            else if (bubbleMoveInt == 0)
            {
                transform.Translate(Vector2.down * Time.deltaTime, Space.World);
            }
        }
    }
}
