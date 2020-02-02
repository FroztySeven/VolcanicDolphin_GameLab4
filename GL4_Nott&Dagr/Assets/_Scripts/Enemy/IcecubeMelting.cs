using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecubeMelting : MonoBehaviour
{
    public bool isIceCubeExisting = true;
    public GameObject iceCube;
    public GameObject pickupKey;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // 
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                if (!isIceCubeExisting)
                {
                    /*Destroy(transform.Find("Icecube").gameObject);*/
                    gameObject.transform.Find("Icecube").gameObject.SetActive(true);
                    isIceCubeExisting = true;

                    Destroy(transform.Find("key_01").GetComponent<BoxCollider2D>());
                    GetComponent<Rigidbody2D>().mass = 5f;
                    /*Destroy(transform.Find("key_01").GetComponent<Rigidbody2D>());*/
                }
            }
        }
        
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                if (isIceCubeExisting)
                {
                    /*Destroy(transform.Find("Icecube").gameObject);*/
                    gameObject.transform.Find("Icecube").gameObject.SetActive(false);
                    isIceCubeExisting = false;

                    gameObject.transform.Find("key_01").gameObject.AddComponent<BoxCollider2D>();
                    GetComponent<Rigidbody2D>().mass = 110f;
                    /*gameObject.transform.Find("key_01").gameObject.AddComponent<Rigidbody2D>();*/
                }
            }
        }
    }
}
