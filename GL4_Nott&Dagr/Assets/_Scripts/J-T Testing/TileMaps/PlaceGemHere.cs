using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGemHere : MonoBehaviour
{
    public enum Gems {Turquoise, Yellow, Purple, Pink, Red, Blue, Green}

    public Gems gemColour;

    public Sprite[] gemSprites;

    private int gemSelect;

    private GameObject door;
    private ParticleSystem particle;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = gemSprites[(int)gemColour];

        if (GameObject.Find("Door"))
        {
            door = GameObject.Find("Door");
        }

        if (transform.childCount > 0)
        {
            particle = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Gem")
        {
            if (other.GetComponent<Gem>().gemColour.ToString() == gemColour.ToString() && !other.GetComponent<Gem>().isFrozen)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                door.GetComponent<ExitLevel>().DoorOpen();
                Destroy(other.gameObject);
                particle.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Gem")
        {
            if (other.GetComponent<Gem>().gemColour.ToString() == gemColour.ToString() && !other.GetComponent<Gem>().isFrozen)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                door.GetComponent<ExitLevel>().DoorOpen();
                Destroy(other.gameObject);
                particle.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}
