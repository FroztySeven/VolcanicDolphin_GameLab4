using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public enum WhoCanJump { Night, Day, Both }

    public WhoCanJump setJumper;

    public int trampolineLength;

    public float jumpForce;

    public Sprite singleTile, leftEnd, middle, rightEnd;

    private Color nightColor, dayColor, bothColor;

    private SpriteRenderer theSR;

    private float playerJumpForce;

    private int startLayer;

    private BoxCollider2D theBC;

    private void Start()
    {
        //Debug.Log(trampolineLength);
        theBC = GetComponent<BoxCollider2D>();

        theBC.size = new Vector2(trampolineLength, 1f);

        if (trampolineLength % 2 == 0)
        {
            theBC.offset = new Vector2((trampolineLength / 2) - 0.5f, 0f);
        }
        else
        {
            theBC.offset = new Vector2((trampolineLength / 2), 0f);
        }

        if (trampolineLength == 1)
        {
            GetComponent<SpriteRenderer>().sprite = singleTile;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = null;

            for (int i = 0; i < trampolineLength; i++)
            {
                GameObject sprite = new GameObject("TrampolineSprite");
                sprite.transform.parent = transform;
                sprite.transform.position = transform.position + new Vector3(i, 0f, 0f);
                sprite.AddComponent<SpriteRenderer>().sprite = middle;
                //sprite.transform.parent = transform;

                if (i == 0)
                {
                    sprite.GetComponent<SpriteRenderer>().sprite = leftEnd;
                }

                if (i == trampolineLength - 1)
                {
                    sprite.GetComponent<SpriteRenderer>().sprite = rightEnd;
                }
            }
        }

        theSR = GetComponentInChildren<SpriteRenderer>();
        nightColor = Color.blue;
        dayColor = Color.yellow;
        bothColor = Color.green;

        playerJumpForce = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementTest>().jumpForce;

        startLayer = gameObject.layer;

        //if (setJumper.ToString() == "Both")
        //{
        //    theSR.color = bothColor;
        //}
        //if (setJumper.ToString() == "Night")
        //{
        //    theSR.color = nightColor;
        //}
        //if (setJumper.ToString() == "Day")
        //{
        //    theSR.color = dayColor;
        //}
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

        if (other.gameObject.name == "Gem")
        {
            //other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * 10));
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
