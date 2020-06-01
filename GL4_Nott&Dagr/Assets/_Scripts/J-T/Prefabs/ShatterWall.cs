using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterWall : MonoBehaviour
{
    public int wallHeight;

    public GameObject wallPrefab;

    public Sprite[] brokenWalls;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < wallHeight; i++)
        {
            GameObject wall = Instantiate(wallPrefab, transform.position + new Vector3(0f, i, 0f), transform.rotation);
            wall.transform.parent = transform;
        }
    }
}
