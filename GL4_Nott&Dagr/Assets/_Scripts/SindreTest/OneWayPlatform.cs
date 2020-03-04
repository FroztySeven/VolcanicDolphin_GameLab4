using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D _platformEffector;

    private float _startJumpforce;

    // Start is called before the first frame update
    void Start()
    {
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _startJumpforce = other.GetComponent<PlayerMovementTest>().jumpForce;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetAxis("VerticalP" + other.GetComponent<PlayerMovementTest>().playerId) <= -1)
            {
                other.GetComponent<PlayerMovementTest>().jumpForce = 0;

                if (other.GetComponent<PlayerMovementTest>().isGrounded && Input.GetAxis("VerticalP" + other.GetComponent<PlayerMovementTest>().playerId) <= -1 && 
                    Input.GetButton("JumpP" + other.GetComponent<PlayerMovementTest>().playerId))
                {
                    _platformEffector.rotationalOffset = 180;
                    StartCoroutine(rotateBack());
                }
            }
            else
            {
                other.GetComponent<PlayerMovementTest>().jumpForce = _startJumpforce;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovementTest>().jumpForce = _startJumpforce;
        }
    }

    private IEnumerator rotateBack()
    {
        yield return new WaitForSeconds(0.5f);
        _platformEffector.rotationalOffset = 0;
    }
}
