using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseWallController : MonoBehaviour
{
    //... Notice! This script was originally made by Jan-Tore.
    //... Original scripts used were PressurePlates and PressurePlatesWall. 
    //... I (Gunnar), made a copy those scripts to make alterations to them, add more variants of wall behaviours that I wanted.

    public int wallHeight = 1;
    public int wallWidth = 1;

    public Sprite wallSprite;

    public GameObject wallPrefab;

    public bool showMe;

    private void Start()
    {
        for (int i = 0; i < wallWidth; i++)
        {
            for (int j = 0; j < wallHeight; j++)
            {
                GameObject wall = Instantiate(wallPrefab, transform.position + new Vector3(i, j, 0f), transform.rotation);
                wall.GetComponent<SpriteRenderer>().sprite = wallSprite;
                wall.transform.parent = transform;
            }
        }

        Destroy(wallPrefab);
    }
}
