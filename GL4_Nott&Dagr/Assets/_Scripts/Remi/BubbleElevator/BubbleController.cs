using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[SelectionBase]
public class BubbleController : MonoBehaviour
{

    public enum SpeedModifier
    {
        ConsistentSpeed,
        SplitSpeed
    };


    [Header("Bubble Settings")]
    public SpeedModifier pathSpeedModifier;


    [Range(0f, 10.0f)] public float consistentSpeed = 0.4f;

    [Space(20)] [Range(0f, 10.0f)] public float splitForwardSpeed = 0.4f;
    [Range(0f, 10.0f)] public float splitBackwardsSpeed = 0.4f;
    
        
    [HideInInspector] public int bubbleMoveInt = 0;

    private Vector3 posA;
    private Vector3 posB;


    [Header("Movement Path Setup")] [SerializeField]
    Transform currentBubble;

    [SerializeField] private Transform movePathChild;

    
    private Vector3 startPos;
    private Vector3 endPos;

    GameObject _player1;
    GameObject _player2;
    private Component _parentScript;
    
    
    private BoxCollider2D childCollider;

    private void Start()
    {
        //childCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        
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
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                _player2.transform.parent = this.transform;
                _player2.GetComponent<PlayerController>().theRB.interpolation = RigidbodyInterpolation2D.None;
                _player2.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
                _player2.transform.Find("Aura").GetComponent<AuraBounce>().isFading = true;
                _player2.transform.Find("Aura").GetComponent<AuraBounce>().isRestoring = false;

                if (bubbleMoveInt == 0)
                {
                    bubbleMoveInt = 1;
                }
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                _player1.transform.parent = this.transform;
                _player1.GetComponent<PlayerController>().theRB.interpolation = RigidbodyInterpolation2D.None;
                _player1.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
                _player1.transform.Find("Aura").GetComponent<AuraBounce>().isFading = true;
                _player1.transform.Find("Aura").GetComponent<AuraBounce>().isRestoring = false;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                bubbleMoveInt = 2;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                _player2.transform.parent = null;
                _player2.GetComponent<PlayerController>().theRB.interpolation = RigidbodyInterpolation2D.Interpolate;
                _player2.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
                _player2.transform.Find("Aura").GetComponent<AuraBounce>().isRestoring = true;
                _player2.transform.Find("Aura").GetComponent<AuraBounce>().isFading = false;

                bubbleMoveInt = 0;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                _player1.transform.parent = null;
                _player1.GetComponent<PlayerController>().theRB.interpolation = RigidbodyInterpolation2D.Interpolate;
                _player1.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
                _player1.transform.Find("Aura").GetComponent<AuraBounce>().isRestoring = true;
                _player1.transform.Find("Aura").GetComponent<AuraBounce>().isFading = false;

                bubbleMoveInt = 0;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }


    private void Update()
    {
        // Bubble return to default path when no players are inside
        if (bubbleMoveInt == 0)
        {
            if (pathSpeedModifier.ToString() == "ConsistentSpeed")
            {
                currentBubble.localPosition =
                    Vector3.MoveTowards(currentBubble.localPosition, posA, consistentSpeed * Time.deltaTime);
            }
            else if (pathSpeedModifier.ToString() == "SplitSpeed")
            {
                currentBubble.localPosition =
                    Vector3.MoveTowards(currentBubble.localPosition, posA, splitBackwardsSpeed * Time.deltaTime);
            }
        }

        //// bubbleMoveInt == 1 is set to player "Night" and will freeze the bubble to place
        //if (bubbleMoveInt > 0)
        //{
        //    childCollider.enabled = true;
        //}
        //else
        //{
        //    childCollider.enabled = false;
        //}

        // Bubble starts moving when player "Day" is inside
        if (bubbleMoveInt == 2)
        {
            if (pathSpeedModifier.ToString() == "ConsistentSpeed")
            {
                currentBubble.localPosition =
                    Vector3.MoveTowards(currentBubble.localPosition, endPos, consistentSpeed * Time.deltaTime);
            }
            else if (pathSpeedModifier.ToString() == "SplitSpeed")
            {
                currentBubble.localPosition =
                    Vector3.MoveTowards(currentBubble.localPosition, endPos, splitForwardSpeed * Time.deltaTime);
            }
        }
    }
}
