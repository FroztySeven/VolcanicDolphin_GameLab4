using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePressurePlateMover : MonoBehaviour
{
    public float speed = 5.0f;

    public int moveUnits;

    private Vector2 position, targetUp, targetDown, targetRight, targetLeft;

    public bool moveUp, moveDown, moveLeft, moveRight, move;

    void Start()
    {
        position = gameObject.transform.position;
        targetUp = new Vector2(position.x, position.y + moveUnits);
        targetDown = new Vector2(position.x, position.y - moveUnits);
        targetRight = new Vector2(position.x + moveUnits, position.y);
        targetLeft = new Vector2(position.x - moveUnits, position.y);
    }

    void Update()
    {
        MoveObjects();
    }

    public void MoveObjects()
    {
        float step = speed * Time.deltaTime;

        if (move)
        {
            if (moveUp)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetUp, step);
            }
            else if (moveDown)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetDown, step);
            }
            else if (moveRight)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetRight, step);
            }
            else if (moveLeft)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetLeft, step);
            }
            
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, position, step);
        }
    }
}

