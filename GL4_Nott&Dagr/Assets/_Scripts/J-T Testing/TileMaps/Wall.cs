using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).GetComponent<CircleCollider2D>().enabled = true;
            Destroy(transform.GetChild(i).gameObject, 5f);
        }
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
