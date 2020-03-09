using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBurn : MonoBehaviour
{
    public GameObject platform;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                gameObject.SetActive(false);
                platform.GetComponent<PlatformEffector2D>().enabled = false;
                platform.GetComponent<BoxCollider2D>().usedByEffector = false;
            }
        }
    }
}
