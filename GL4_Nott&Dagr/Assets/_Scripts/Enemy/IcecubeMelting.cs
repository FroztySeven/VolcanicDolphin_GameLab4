﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IcecubeMelting : MonoBehaviour
{
    public bool isIceCubeExisting = true;
    public int cubeMass = 5;
    public GameObject iceCube;
    public GameObject pickupKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                if (!isIceCubeExisting)
                {
                    /*Destroy(transform.Find("Icecube").gameObject);*/
                    gameObject.transform.Find("Icecube").gameObject.SetActive(true);
                    isIceCubeExisting = true;

                    Destroy(transform.Find("key_01").GetComponent<BoxCollider2D>());
                    GetComponent<Rigidbody2D>().mass = cubeMass;
                    /*Destroy(transform.Find("key_01").GetComponent<Rigidbody2D>());*/

                    // For Audio   
                    gameObject.transform.Find("key_01").GetComponent<CircleCollider2D>().enabled = false;
                }
            }
        }
        
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                if (isIceCubeExisting)
                {
                    /*Destroy(transform.Find("Icecube").gameObject);*/
                    gameObject.transform.Find("Icecube").gameObject.SetActive(false);
                    isIceCubeExisting = false;

                    gameObject.transform.Find("key_01").gameObject.AddComponent<BoxCollider2D>();
                    GetComponent<Rigidbody2D>().mass = 1110f;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    /*gameObject.transform.Find("key_01").gameObject.AddComponent<Rigidbody2D>();*/

                    // For Audio   
                    gameObject.transform.Find("key_01").GetComponent<CircleCollider2D>().enabled = true;
                }
            }
        }

        //if (other.tag == "TM_Water")
        //{
        //    Debug.Log("Hello!");
        //    other.gameObject.layer = 10;
        //}
    }
}
