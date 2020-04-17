using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPickUp : MonoBehaviour
{
    private PlayerController player;

    private Gem gem;

    private Transform gemPickup;

    private int playerId;

    private bool pickedUp;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        playerId = player.playerId;

        if (Input.GetButtonDown("PickupP" + playerId) && gemPickup != null && gem.isFrozen && !gem.isPickedUp && !pickedUp)
        {
            gemPickup.GetComponent<CircleCollider2D>().enabled = false;
            gem.isPickedUp = true;
            pickedUp = true;
        }

        if (Input.GetButton("PickupP" + playerId) && gemPickup != null && gem.isFrozen && gem.isPickedUp && pickedUp)
        {
            gemPickup.position = transform.position - new Vector3(0f, 0.5f, 0f);
        }

        if (Input.GetButtonUp("PickupP" + playerId) && gemPickup != null && gem.isFrozen && gem.isPickedUp && pickedUp)
        {
            gem.theRB.velocity = player.theRB.velocity;
            gemPickup.GetComponent<CircleCollider2D>().enabled = true;
            gemPickup = null;
            pickedUp = false;
            gem.isPickedUp = false;
            gem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Gem")
        {
            gemPickup = other.transform;
            gem = other.GetComponent<Gem>();
            //Debug.Log("Player: " + playerId + " Triggered Gem");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Gem")
        {
            Invoke("EmptyGemPickup", 0.01f);
        }
    }

    private void EmptyGemPickup()
    {
        if (!pickedUp)
        {
            gemPickup = null;
            gem = null;
            //Debug.Log("Invoked Player: " + playerId);
        }
    }
}
