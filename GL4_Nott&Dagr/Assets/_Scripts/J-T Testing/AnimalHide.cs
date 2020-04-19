using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHide : MonoBehaviour
{
    public Transform idlePosition;
    public Transform hiddenPosition;

    public float playerDetectionRadius;
    public float timeBeforeMoveBack;
    public float moveSpeed;

    public LayerMask whatIsPlayer;

    private bool playerDetected;
    private bool canFlip;
    private bool isFacingRight;
    private bool isIdle = true;
    private bool isMovingAwayFromPlayer;
    private bool isMovingToIdle;
    private bool isCountingDown;

    private float timer;

    private void Start()
    {
        idlePosition.parent = null;
        hiddenPosition.parent = null;
        timer = timeBeforeMoveBack;
    }

    private void Update()
    {
        CheckState();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        playerDetected = Physics2D.OverlapCircle(idlePosition.position, playerDetectionRadius, whatIsPlayer);
    }

    private void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void CheckState()
    {
        if (playerDetected && isIdle)
        {
            isMovingAwayFromPlayer = true;
            isIdle = false;
        }

        if (isMovingAwayFromPlayer)
        {
            MoveAwayFromPlayer();
        }

        if (isMovingToIdle)
        {
            MoveToIdlePosition();
        }

        if (isCountingDown)
        {
            CountDown();
        }
    }

    private void MoveToIdlePosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, idlePosition.position, moveSpeed * Time.deltaTime);
        if (transform.position == idlePosition.position)
        {
            isIdle = true;
            isMovingToIdle = false;
        }
    }

    private void MoveAwayFromPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, hiddenPosition.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, hiddenPosition.position) < 0.01f)
        {
            isMovingAwayFromPlayer = false;
            isCountingDown = true;
        }
    }

    private void CountDown()
    {   
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (playerDetected)
            {
                timer = timeBeforeMoveBack;
            }
            else
            {
                isCountingDown = false;
                timer = timeBeforeMoveBack;
                isMovingToIdle = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(idlePosition.position, playerDetectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, idlePosition.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, hiddenPosition.position);
    }
}
