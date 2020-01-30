using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecubeMelting : MonoBehaviour
{
    public GameObject iceCube;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 
        if (other.gameObject.name == "Player2")
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
