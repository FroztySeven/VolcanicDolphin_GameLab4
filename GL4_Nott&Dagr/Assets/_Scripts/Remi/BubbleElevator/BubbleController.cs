using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[SelectionBase]
public class BubbleController : MonoBehaviour
{
    public enum BubbleMovement
    {
        Disabled,
        OnceToDestination,
        AlwaysToDestination
    };
    
    [Header("Bubble Settings")]
    public BubbleMovement movement;
    
    
    [Range(0.1f, 10.0f)]
    public float speed = 0.4f;

    
    [HideInInspector]
    public int bubbleMoveInt = 0;

    private Vector3 posA;
    private Vector3 posB;


    [Header("Movement Path Setup")]
    [SerializeField] 
    private Transform currentBubble;
    [SerializeField] 
    private Transform movePathChild;

    
    private Vector3 startPos;
    private Vector3 endPos;
    
    GameObject _player1;
    GameObject _player2;
    private Component _parentScript;

    private void Start()
    {
        _parentScript = GetComponentInParent(typeof(BubblePath));
        _player1 = GameObject.Find("Player1");
        _player2 = GameObject.Find("Player2");
        
        posA = currentBubble.localPosition;
        posB = movePathChild.localPosition;
        endPos = posB;
        startPos = transform.position;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                if (movement.ToString() == "AlwaysToDestination" || movement.ToString() == "OnceToDestination")
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
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                if (movement.ToString() == "AlwaysToDestination" || movement.ToString() == "OnceToDestination")
                {
                    _player1.transform.parent = this.transform;
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                    bubbleMoveInt = 2;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                if (movement.ToString() == "AlwaysToDestination" || movement.ToString() == "OnceToDestination")
                {
                    _player2.transform.parent = null;
                    
                    bubbleMoveInt = 0;
                }
            }
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                if (movement.ToString() == "AlwaysToDestination" || movement.ToString() == "OnceToDestination")
                {
                    _player1.transform.parent = null;
                    bubbleMoveInt = 0;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }



    private void Update()
    {
        // Bubble return to default path when no players are inside
        if (bubbleMoveInt == 0)
        {
            if (movement.ToString() == "Disabled")
            {
                Destroy(_parentScript);
                speed = 0f;
            }
            if (movement.ToString() == "AlwaysToDestination")
            {
                currentBubble.localPosition =
                    Vector3.MoveTowards(currentBubble.localPosition, posA, speed * Time.deltaTime);
            }
        }


        // bubbleMoveInt == 1 is set to player "Night" and will freeze the bubble to place

        // Bubble starts moving when player "Day" is inside
        if (bubbleMoveInt == 2)
        {
            if (movement.ToString() == "AlwaysToDestination" || movement.ToString() == "OnceToDestination")
            {
                currentBubble.localPosition =
                    Vector3.MoveTowards(currentBubble.localPosition, endPos, speed * Time.deltaTime);
            }
        }
    }
}
