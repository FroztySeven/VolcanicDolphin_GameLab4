using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private GameObject nott, dagr, key, seed;
    private Vector3 nottStartPosition, dagrStartPosition, keyStartPosition, seedStartPosition;

    public bool resetNott = true, resetDagr = true, resetKey = true, resetSeed = true;

    private void Start()
    {
        nott = GameObject.Find("Player2");
        dagr = GameObject.Find("Player1");
        key = GameObject.Find("Gem");
        seed = GameObject.Find("Seed");
        nottStartPosition = nott.transform.position;
        dagrStartPosition = dagr.transform.position;

        if (key != null)
        {
            keyStartPosition = key.transform.position;
        }

        if (seed != null)
        {
            seedStartPosition = seed.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == nott && resetNott)
        {
            nott.transform.position = nottStartPosition;
        }

        if (other.gameObject == dagr && resetDagr)
        {
            dagr.transform.position = dagrStartPosition;
        }

        if (other.gameObject == key && resetKey)
        {
            key.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            key.transform.position = keyStartPosition;
        }

        if (other.gameObject == seed && resetSeed)
        {
            seed.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            seed.transform.position = seedStartPosition;
        }

        if (other.name == "ShatteredWall")
        {
            Destroy(other.gameObject);
        }
    }
}
