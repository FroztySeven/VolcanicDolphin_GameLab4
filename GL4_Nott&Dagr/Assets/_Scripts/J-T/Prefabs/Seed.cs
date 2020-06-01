using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{

    public Color frozenColor;

    private void Start()
    {
        name = "Seed";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<Rigidbody2D>().mass = 10;
                GetComponent<CircleCollider2D>().isTrigger = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<Rigidbody2D>().mass = 1000;
                GetComponent<CircleCollider2D>().isTrigger = false;
                GetComponent<SpriteRenderer>().color = frozenColor;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                GetComponent<Rigidbody2D>().mass = 10;
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                GetComponent<Rigidbody2D>().mass = 1000;
                GetComponent<SpriteRenderer>().color = frozenColor;
            }
        }
    }
}
