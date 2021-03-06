﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpenDoor : MonoBehaviour
{
    private GameObject door;

    private void Start()
    {
        if (GameObject.Find("Door")) // find the door/portal in the scene
        {
            door = GameObject.Find("Door");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key")) //if the key enters this gameobject open door
        {
            door.GetComponent<ExitLevel>().DoorOpen();
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
    
}
