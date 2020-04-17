using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private float rayDist = 0.8f;
    private bool _moveRight;
    public Transform groundDetection;

    private float timer = 0f;

    private bool ledgeDetected;
    private bool groundDetected;
    private bool inAir;

    [Header("Can enemy fall down ledge once?")] public bool canFallDownLedge = false;

    [Header("Can The Enemy Patrol?")] public bool edgeDetection = true;

    [Header("Can Dagr Unfreeze?")] public bool canUnfreeze = false;

    [Header("Set Transformation Sprites")]
    public Sprite defaultSprite;
    public Sprite transformSprite;



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        ledgeDetected = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDist);
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, rayDist);

        Debug.Log(Physics2D.Raycast(transform.position, Vector2.down, rayDist));

        //If this bool is enabled through the Inspector, then prevent the enemy to fall of edges
        if (edgeDetection)
        {

            if (!ledgeDetected && !groundDetected)
            {
                inAir = true;
            }
            else
            {
                inAir = false;
            }

            if (!ledgeDetected && groundDetected && !inAir && !canFallDownLedge)
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
            else if (inAir)
            {
                canFallDownLedge = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundDetection.position, new Vector3(groundDetection.position.x, groundDetection.position.y - rayDist, groundDetection.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - rayDist, transform.position.z));
    }
}
