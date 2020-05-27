using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickWallWallController : MonoBehaviour
{
    //... Notice! This script was originally made by Jan-Tore.
    //... Original scripts used were PressurePlates and PressurePlatesWall. 
    //... I (Gunnar), made a copy those scripts to make alterations to them, add more variants of wall behaviours that I wanted.
    //... This script instantiate the wall prefabs and adds the wall clones to a list and assigns them to a master controller plate, in case there are more then one plates the interact with walls.

    public GameObject primePlate;

    public int wallHeight = 1;
    public int wallWidth = 1;

    public Sprite wallSprite;

    public GameObject wallPrefab;

    public List<GameObject> children = new List<GameObject>();

    public bool hideMe, usePrime;

    private int cloneNr;

    private void Awake()
    {
        
    }

    private void Start()
    {
        for (int i = 0; i < wallWidth; i++)
        {
            for (int j = 0; j < wallHeight; j++)
            {
                GameObject wall = Instantiate(wallPrefab, transform.position + new Vector3(i, j, 0f), transform.rotation);
                wall.GetComponent<SpriteRenderer>().sprite = wallSprite;
                wall.transform.parent = transform;


                wall.tag = "WallClone";
                wall.AddComponent<TrickWallBoolChecker>();
                //wall.name = "WallClone" + cloneNr++;
                children.Add(wall.gameObject);

                if (usePrime)
                {
                    primePlate.GetComponent<TrickWallHideController>().clones.Add(wall.gameObject);

                    //primePlate.GetComponent<TrickWallHideController>().wallClones = children.ToArray();
                }
            }
        }

        Destroy(wallPrefab);
        //wallPrefab.SetActive(false);


    }
}
