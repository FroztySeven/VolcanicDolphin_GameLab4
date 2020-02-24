using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        other.transform.SetParent(transform);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        other.transform.SetParent(null);
    //    }
    //}

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            other.transform.GetComponent<PlayerMovementTest>().canMove = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.GetComponent<PlayerMovementTest>().moveInput.x > 0 || other.transform.GetComponent<PlayerMovementTest>().moveInput.x < 0)
            {
                other.transform.GetComponent<PlayerMovementTest>().canMove = true;
            }
            else
            {
                other.transform.GetComponent<PlayerMovementTest>().canMove = false;
            }
            //other.transform.GetComponent<PlayerMovementTest>().theRB = gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
