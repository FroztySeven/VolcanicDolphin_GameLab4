using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                GetComponentInParent<EnemyPatrol>().enabled = false;
                GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<EnemyPatrol>().transformSprite;
                GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                /*Destroy(transform.GetChild(0).gameObject);
                Destroy(transform.GetChild(1).gameObject);*/
            }
        }
    }
}
