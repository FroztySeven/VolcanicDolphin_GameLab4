﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private float rayDist = 2.78f;
    private bool _moveRight;
    public Transform groundDetection;

    [Header("Can The Enemy Patrol?")] public bool edgeDetection = true;

    [Header("Can Dagr Unfreeze?")] public bool canUnfreeze = false;

    [Header("Set Transformation Sprites")]
    public Sprite defaultSprite;
    public Sprite transformSprite;



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDist);

        //If this bool is enabled through the Inspector, then prevent the enemy to fall of edges
        if (edgeDetection)
        {
            if (groundCheck.collider == false)
            {
                if (_moveRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    _moveRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    _moveRight = true;
                }
            }
        }
    }
}
