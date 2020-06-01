using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public int freezeDuration;

    [HideInInspector]
    public bool isFrozen = false;
    [HideInInspector]
    public bool freeze = false;
    [HideInInspector]
    public float freezeTimer = 0;
    [HideInInspector]
    public int freezeNumber = 0;
    [HideInInspector]
    public int plantId;

    private SpriteRenderer theSR;

    public Color frozenPlantColor;
    private Color startColor;

    private GrowingPlant parentPlant;

    private void Start()
    {
        parentPlant = GetComponentInParent<GrowingPlant>();
        theSR = GetComponent<SpriteRenderer>();
        startColor = theSR.color;
    }

    private void Update()
    {
        if (isFrozen)
        {
            theSR.color = frozenPlantColor;
            freezeNumber = 0;
            freezeTimer = 0;
            freeze = false;
        }
        else
        {
            theSR.color = startColor;

            if (freeze)
            {
                float freezeSpeed = 2;

                if (freezeTimer < 1)
                {
                    freezeTimer += Time.deltaTime * freezeSpeed;
                }
                if (freezeTimer >= 1)
                {
                    freezeNumber++;
                    freezeTimer = 0;
                }
                if (freezeNumber % 2 != 0)
                {
                    theSR.color = frozenPlantColor;
                }
                if (freezeNumber == freezeDuration * 2)
                {
                    freezeNumber = 0;
                    freezeTimer = 0;
                    freeze = false;
                    StartCoroutine(parentPlant.FreezePlant(plantId, plantId));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                other.GetComponent<PlayerController>().isOnLadder = true;

                if (isFrozen)
                {
                    StartCoroutine(parentPlant.UnFreezePlant(plantId, plantId));
                }
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                if (!isFrozen)
                {
                    freeze = true;
                }

            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                other.GetComponent<PlayerController>().isOnLadder = true;
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                if (isFrozen)
                {
                    other.GetComponent<PlayerController>().isOnLadder = false;
                }
                else
                {
                    other.GetComponent<PlayerController>().isOnLadder = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                other.GetComponent<PlayerController>().isOnLadder = false;
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                other.GetComponent<PlayerController>().isOnLadder = false;
            }
        }
    }
}
