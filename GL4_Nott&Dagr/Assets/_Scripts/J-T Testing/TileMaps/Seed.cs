using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
