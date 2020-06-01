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

    private float playerJumpForce;

    private int startLayer;

    private BoxCollider2D theBC;

    private void Start()
    {
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

        playerJumpForce = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().jumpForce;

        startLayer = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (setJumper.ToString() == "Both")
            {
                other.gameObject.GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
                other.gameObject.GetComponent<PlayerController>().theRB.AddForce(new Vector2(0f, jumpForce));
            }

            if (setJumper.ToString() == "Night")
            {
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    other.gameObject.GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
                    other.gameObject.GetComponent<PlayerController>().theRB.AddForce(new Vector2(0f, jumpForce));
                }
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                {
                    gameObject.layer = default;
                }
            }

            if (setJumper.ToString() == "Day")
            {
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                {
                    other.gameObject.GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
                    other.gameObject.GetComponent<PlayerController>().theRB.AddForce(new Vector2(0f, jumpForce));
                }
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    gameObject.layer = default;
                }
            }

            if (other.gameObject.GetComponent<PlayerController>().amountOfJumps > 1)
            {
                other.gameObject.GetComponent<PlayerController>().amountOfJumpsLeft = other.gameObject.GetComponent<PlayerController>().amountOfJumps - 1;
            }
        }

        if (other.gameObject.name == "Gem")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * 10));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (setJumper.ToString() == "Night")
            {
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                {
                    gameObject.layer = startLayer;
                }
            }

            if (setJumper.ToString() == "Day")
            {
                if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    gameObject.layer = startLayer;
                }
            }
        }
    }
}
