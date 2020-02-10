using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioKeyOpenDoor : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string openDoorSFX;

    public Sprite closedDoor, openDoor;

    public GameObject door;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            door.GetComponent<SpriteRenderer>().sprite = openDoor;
            door.GetComponent<BoxCollider2D>().enabled = true;
            other.gameObject.SetActive(false);
            Invoke("CallOpenDoor", 0);
        }
    }

    void CallOpenDoor()
    {
        FMODUnity.RuntimeManager.PlayOneShot(openDoorSFX);
    }

}
