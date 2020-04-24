using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public enum Gems {Turquoise, Yellow, Purple, Pink, Red, Blue, Green}

    public Gems gemColour;

    public Sprite[] gemFrozenSprites;
    public Sprite[] gemUnfrozenSprites;

    [HideInInspector]
    public Rigidbody2D theRB;
    private CircleCollider2D theCC;

    private Vector2 startOffset;
    private Vector2 unfrozenOffset;

    private float startRadius;
    private float unfrozenRadius;

    public bool isFrozen;
    public bool isPickedUp = false;

    private bool auraPickupTouch = false;

    private void Awake()
    {
        name = "Gem";
    }

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        theCC = GetComponent<CircleCollider2D>();

        startOffset = theCC.offset;
        startRadius = theCC.radius;

        unfrozenOffset = new Vector2(0f, 0.2f);
        unfrozenRadius = 0.2f;

        isFrozen = true;

        GetComponent<SpriteRenderer>().sprite = gemFrozenSprites[(int)gemColour];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                theRB.isKinematic = false;
                theRB.mass = 10;
                theCC.isTrigger = false;
            }

            if (other.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                theRB.isKinematic = false;
                theRB.velocity = Vector2.zero;
                theRB.mass = 1000;
                theCC.isTrigger = false;
                theCC.offset = unfrozenOffset;
                theCC.radius = unfrozenRadius;
                isFrozen = false;
                GetComponent<SpriteRenderer>().sprite = gemUnfrozenSprites[(int)gemColour];
            }
        }

        if (other.tag == "AuraPickup" && !auraPickupTouch)
        {
            theRB.isKinematic = false;
            theCC.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Night")
            {
                theRB.mass = 10;
                theCC.offset = startOffset;
                theCC.radius = startRadius;
                isFrozen = true;
                GetComponent<SpriteRenderer>().sprite = gemFrozenSprites[(int)gemColour];
            }

            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                theRB.velocity = Vector2.zero;
                theRB.mass = 1000;
                theCC.offset = unfrozenOffset;
                theCC.radius = unfrozenRadius;
                isFrozen = false;
                GetComponent<SpriteRenderer>().sprite = gemUnfrozenSprites[(int)gemColour];
            }
        }
    }
}
