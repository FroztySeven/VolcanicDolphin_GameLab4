using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        other.GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
    //        other.GetComponent<PlayerMovementTest>().theRB.AddForce(new Vector2(0f, jumpForce));
    //    }

    //    if (other.name == "PickupKey")
    //    {
    //        //other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * 5));
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
            other.gameObject.GetComponent<PlayerMovementTest>().theRB.AddForce(new Vector2(0f, jumpForce));

            if (other.gameObject.GetComponent<PlayerMovementTest>().canDoubleJump)
            {
                other.gameObject.GetComponent<PlayerMovementTest>().doubleJumpCounter = 1;
            }
        }

        if (other.gameObject.name == "PickupKey")
        {
            //other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * 5));
        }
    }
}
