using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SwingingRope : MonoBehaviour
{
    public bool nott, dagr, both;

    public int ropeLength;

    public Sprite ropeSprite;
    
    //----Audio Addon-----//
    [FMODUnity.EventRef] public string ropeTwist;
    //----Audio Addon-----//

    // Start is called before the first frame update
    void Start()
    {
        ropeLength = ropeLength - 1;
        
        for (int i = 0; i < ropeLength; i++) //spawning of the rope depending on how long it should be.
        {
            GameObject sprite = new GameObject("Rope" + (i + 1));
            sprite.transform.parent = transform;
            sprite.transform.position = transform.position + new Vector3(0f, -i - 1f, 0f);
            sprite.AddComponent<SpriteRenderer>().sprite = ropeSprite;
            sprite.AddComponent<BoxCollider2D>();
            sprite.AddComponent<HingeJoint2D>().connectedBody = this.gameObject.transform.GetChild(i).GetComponent<Rigidbody2D>();
            sprite.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
            sprite.GetComponent<Rigidbody2D>().mass = 25;
            sprite.layer = 11;

            if (i == ropeLength -1) //spawning an empty gameobject with a trigger and another script on it.
            {
                GameObject endZone = new GameObject("EndZone");
                endZone.transform.parent = gameObject.transform.GetChild(i + 1);
                endZone.transform.position = transform.position + new Vector3(0f, -i - 1, 0f);
                endZone.AddComponent<BoxCollider2D>().isTrigger = true;
                endZone.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f); //Changed the X from 0.25 to 1 to fix repeating audiobug. -Gunnar
                endZone.AddComponent<SwingingRopeEndZone>();
            }
        }
    }
}
