using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                other.GetComponent<PlayerMovementTest>().isOnLadder = true;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                other.GetComponent<PlayerMovementTest>().isOnLadder = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                other.GetComponent<PlayerMovementTest>().isOnLadder = false;
            }

            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                other.GetComponent<PlayerMovementTest>().isOnLadder = false;
            }
        }
    }
}
