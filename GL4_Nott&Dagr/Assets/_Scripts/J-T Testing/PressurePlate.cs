using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public enum WhoCanUse { Night, Day, Both }

    public WhoCanUse setUser;

    public GameObject wall;

    private Vector3 startPos;
    private Vector3 pressedPos;

    private void Start()
    {
        startPos = transform.position;
        pressedPos = startPos - new Vector3(0f, 0.08f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == setUser.ToString())
            {
                transform.position = pressedPos;
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.8f);
                wall.SetActive(false);
            }

            if (setUser.ToString() == "Both")
            {
                transform.position = pressedPos;
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.8f);
                wall.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == setUser.ToString())
            {
                transform.position = pressedPos;
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.8f);
                wall.SetActive(false);
            }

            if (setUser.ToString() == "Both")
            {
                transform.position = pressedPos;
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.8f);
                wall.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == setUser.ToString())
            {
                transform.position = startPos;
                GetComponent<BoxCollider2D>().offset = Vector2.zero;
                wall.SetActive(true);
            }

            if (setUser.ToString() == "Both")
            {
                transform.position = startPos;
                GetComponent<BoxCollider2D>().offset = Vector2.zero;
                wall.SetActive(true);
            }
        }
    }
}
