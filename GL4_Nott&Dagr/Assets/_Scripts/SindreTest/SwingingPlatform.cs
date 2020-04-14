using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            other.transform.GetComponent<PlayerController>().playerCanMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            other.transform.GetComponent<PlayerController>().playerCanMove = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.GetComponent<PlayerController>().movementInputHorizontalDirection > 0 || other.transform.GetComponent<PlayerController>().movementInputHorizontalDirection < 0)
            {
                other.transform.GetComponent<PlayerController>().playerCanMove = true;
                other.transform.GetComponent<PlayerController>().theRB.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, other.transform.GetComponent<PlayerController>().theRB.velocity.y);
            }
            else if (other.transform.GetComponent<PlayerController>().movementInputHorizontalDirection == 0)
            {
                other.transform.GetComponent<PlayerController>().playerCanMove = false;
                other.transform.GetComponent<PlayerController>().theRB.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, other.transform.GetComponent<PlayerController>().theRB.velocity.y);
            }
        }
    }
}
