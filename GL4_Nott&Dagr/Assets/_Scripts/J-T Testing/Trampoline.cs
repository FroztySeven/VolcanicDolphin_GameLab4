using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public enum WhoCanJump { Night, Day, Both }

    public WhoCanJump setJumper;

    public float jumpForce;

    private Color nightColor, dayColor, bothColor;

    private SpriteRenderer theSR;

    private float playerJumpForce;

    private int startLayer; 

    private void Start()
    {
        theSR = GetComponentInChildren<SpriteRenderer>();
        nightColor = Color.blue;
        dayColor = Color.yellow;
        bothColor = Color.green;

        playerJumpForce = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementTest>().jumpForce;

        startLayer = gameObject.layer;

        if (setJumper.ToString() == "Both")
        {
            theSR.color = bothColor;
        }
        if (setJumper.ToString() == "Night")
        {
            theSR.color = nightColor;
        }
        if (setJumper.ToString() == "Day")
        {
            theSR.color = dayColor;
        }
    }

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
            if (setJumper.ToString() == "Both")
            {
                other.gameObject.GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
                other.gameObject.GetComponent<PlayerMovementTest>().theRB.AddForce(new Vector2(0f, jumpForce));
            }

            if (setJumper.ToString() == "Night")
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
                {
                    other.gameObject.GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
                    other.gameObject.GetComponent<PlayerMovementTest>().theRB.AddForce(new Vector2(0f, jumpForce));
                }
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
                {
                    gameObject.layer = default;
                }
            }

            if (setJumper.ToString() == "Day")
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
                {
                    other.gameObject.GetComponent<PlayerMovementTest>().theRB.velocity = Vector2.zero;
                    other.gameObject.GetComponent<PlayerMovementTest>().theRB.AddForce(new Vector2(0f, jumpForce));
                }
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
                {
                    gameObject.layer = default;
                }
            }

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

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (setJumper.ToString() == "Night")
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
                {
                    gameObject.layer = startLayer;
                }
            }

            if (setJumper.ToString() == "Day")
            {
                if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
                {
                    gameObject.layer = startLayer;
                }
            }

            if (other.gameObject.GetComponent<PlayerMovementTest>().canDoubleJump)
            {
                other.gameObject.GetComponent<PlayerMovementTest>().doubleJumpCounter = 1;
            }
        }
    }
}
