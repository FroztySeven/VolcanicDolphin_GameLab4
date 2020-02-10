using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private GameObject nott, dagr, key;
    private Vector3 nottStartPosition, dagrStartPosition, keyStartPosition;

    private void Start()
    {
        nott = GameObject.Find("Player2");
        dagr = GameObject.Find("Player1");
        key = GameObject.Find("PickupKey");
        nottStartPosition = nott.transform.position;
        dagrStartPosition = dagr.transform.position;

        if (key != null)
        {
            keyStartPosition = key.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == nott)
        {
            nott.transform.position = nottStartPosition;
        }

        if (other.gameObject == dagr)
        {
            dagr.transform.position = dagrStartPosition;
        }

        if (other.gameObject == key)
        {
            key.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            key.transform.position = keyStartPosition;
        }
    }
}
