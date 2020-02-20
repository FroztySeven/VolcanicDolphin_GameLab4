using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IceCubeMeltAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string iceMeltSFX;

    [FMODUnity.EventRef]
    public string iceFreezeSFX;

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
                    Invoke("CallIceFreeze", 0);

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
                    Invoke("CallIceMelt", 0);

                    gameObject.transform.Find("key_01").gameObject.AddComponent<BoxCollider2D>();
                    GetComponent<Rigidbody2D>().mass = 1110f;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    /*gameObject.transform.Find("key_01").gameObject.AddComponent<Rigidbody2D>();*/
                }
            }
        }

        //if (other.tag == "TM_Water")
        //{
        //    Debug.Log("Hello!");
        //    other.gameObject.layer = 10;
        //}
    }

    void CallIceMelt()
    {
        FMODUnity.RuntimeManager.PlayOneShot(iceMeltSFX);
    }

    void CallIceFreeze()
    {
        FMODUnity.RuntimeManager.PlayOneShot(iceFreezeSFX);
    }
}
