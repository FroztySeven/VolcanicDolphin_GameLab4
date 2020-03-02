using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public enum Gems {Turquoise, Yellow, Purple, Pink, Red, Blue, Green}

    public Gems gemColour;

    public Sprite[] gemSprites;

    private void Start()
    {
        name = "Gem";

        GetComponent<SpriteRenderer>().sprite = gemSprites[(int)gemColour];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<Rigidbody2D>().mass = 10;
                GetComponent<CircleCollider2D>().isTrigger = false;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<Rigidbody2D>().mass = 1000;
                GetComponent<CircleCollider2D>().isTrigger = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                GetComponent<Rigidbody2D>().mass = 10;
            }

            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                GetComponent<Rigidbody2D>().mass = 1000;
            }
        }
    }
}
