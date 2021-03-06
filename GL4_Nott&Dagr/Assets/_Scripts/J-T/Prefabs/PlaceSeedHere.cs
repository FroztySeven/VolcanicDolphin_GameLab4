﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSeedHere : MonoBehaviour
{
    public int growHeight;

    public GameObject plant;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Seed")
        {
            GameObject newPlant = Instantiate(plant, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
            newPlant.GetComponent<GrowingPlant>().growHeight = growHeight;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
