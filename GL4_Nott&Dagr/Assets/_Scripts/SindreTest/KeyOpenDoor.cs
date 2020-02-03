using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpenDoor : MonoBehaviour
{
    public Sprite closedDoor, openDoor;
    public GameObject door;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            door.GetComponent<SpriteRenderer>().sprite = openDoor;
            other.gameObject.SetActive(false);
        }
    }
    
}
