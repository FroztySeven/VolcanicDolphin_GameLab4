using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PlatformSpirtes" || other.gameObject.name == "ShatteredWall")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<CircleCollider2D>().enabled = true;

                int leftOrRight = Random.Range(0, 2);

                if (leftOrRight == 0)
                {
                    transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, -100), Random.Range(100, 200)));
                }
                else if (leftOrRight == 1)
                {
                    transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(100, 200), Random.Range(100, 200)));

                }
                //transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 1000));
                //transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(5f, 10f)));
                Destroy(transform.GetChild(i).gameObject, Random.Range(5f, 10f));
            }
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }
}
