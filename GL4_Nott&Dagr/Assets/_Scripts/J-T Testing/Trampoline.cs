using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
            other.GetComponent<PlayerMovementTest>().theRB.AddForce(new Vector2(0f, jumpForce));
        }
    }
}
