using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GetComponentInParent<EnemyPatrol>().canUnfreeze == true)
            {
                if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<EnemyPatrol>().transformSprite;
                    GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                }
                if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
                {
                    startUnFreezing = true;
                }
            }

            if (GetComponentInParent<EnemyPatrol>().canUnfreeze == false)
            {
                if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
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

    float unfreezeTimer = 3f;
    bool startUnFreezing = false;
    void Update()
    {
        if (startUnFreezing)
        {
            unfreezeTimer -= Time.deltaTime;


            if (unfreezeTimer < 0)
            {
                GetComponentInParent<EnemyPatrol>().enabled = true;
                GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<EnemyPatrol>().defaultSprite;
                GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                startUnFreezing = false;
                unfreezeTimer = 3f;
            }
        }
    }
}
