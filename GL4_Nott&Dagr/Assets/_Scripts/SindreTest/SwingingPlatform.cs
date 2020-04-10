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
            }
            else
            {
                other.transform.GetComponent<PlayerController>().playerCanMove = false;
            }
        }
    }
}
