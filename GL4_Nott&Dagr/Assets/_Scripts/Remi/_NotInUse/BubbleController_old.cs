using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController_old : MonoBehaviour
{
    [Header("Prevent the bubble from moving")]
    public bool canMove = true;
    
    public enum BubbleDirection {Vertical, Horizontal};
    [Header("Bubble movement settings")]
    public BubbleDirection direction;
    
    [Range(-10f, 10.0f)]
    public float speed = 1.5f;

    [Header("Bubble tile ground collision detector")]
    private bool hasCollided;
    
    [HideInInspector]
    public int bubbleMoveInt = 0;



    GameObject _player1;
    GameObject _player2;
    private void Start()
    {
        _player1 = GameObject.Find("Player1");
        _player2 = GameObject.Find("Player2");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.GetChild(0).gameObject)
        {
            if (collision.gameObject.CompareTag("TM_Ground"))
            {
                if (canMove)
                {
                    hasCollided = true;
                }
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                if (canMove) 
                {
                    _player2.transform.parent = this.transform;
                    
                    if (bubbleMoveInt == 0)
                    {
                        bubbleMoveInt = 1;
                    }
                }
            }
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                if (canMove)
                {
                    _player1.transform.parent = this.transform;
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    hasCollided = false;
                    bubbleMoveInt = 2;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                if (canMove)
                {
                    _player2.transform.parent = null;
                    
                    bubbleMoveInt = 0;
                }
            }
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                if (canMove)
                {
                    _player1.transform.parent = null;
                    bubbleMoveInt = 0;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
    
    void Update()
    {
        if (!hasCollided)
        {
            // Bubble return to default path when no players are inside
            if (bubbleMoveInt == 0)
            {
                if (canMove)
                {
                    if (direction.ToString() == "Vertical")
                    {
                        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
                    }

                    if (direction.ToString() == "Horizontal")
                    {
                        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
                    }
                }
            }
            
            // bubbleMoveInt == 1 is set to player "Night" and will freeze the bubble to place
            
            // Bubble starts moving when player "Day" is inside
            if (bubbleMoveInt == 2)
            {
                if (direction.ToString() == "Vertical")
                {
                    transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
                }
                if (direction.ToString() == "Horizontal")
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }
}
