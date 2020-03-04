using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraBounce : MonoBehaviour
{
    [Range(1, 20)]
    public float auraMovementSpeed = 5;

    private PlayerMovementTest player;

    private float auraBounceTimer;
    private bool auraBounce;
    private int playerId;

    private Vector2 moveInput;
    private CapsuleCollider2D col;
    private Vector2 colSize, colOffset;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementTest>();
        col = GetComponent<CapsuleCollider2D>();
        colSize = col.size;
        colOffset = col.offset;
    }

    private void Update()
    {
        playerId = player.playerId;
        moveInput.x = Input.GetAxisRaw("RStickHorizontalP" + playerId);
        moveInput.y = Input.GetAxisRaw("RStickVerticalP" + playerId);
        moveInput.Normalize();

        //Debug.Log(moveInput);

        if (auraBounce)
        {
            auraBounceTimer += Time.deltaTime;

            if (auraBounceTimer >= 1)
            {
                auraBounceTimer = 0;
                auraBounce = false;
                player.auraBounce = false;
            }
        }

        if (moveInput.x != 0 && moveInput.y == 0 && col.offset.y == colOffset.y)
        {
            col.direction = CapsuleDirection2D.Horizontal;

            if (moveInput.x > 0 && col.offset.x >= colOffset.x)
            {
                col.size = col.size + new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f);
                col.offset = col.offset + (new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f) / 2);
            }

            if (moveInput.x > 0 && col.offset.x <= colOffset.x)
            {
                col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
            }

            if (moveInput.x < 0 && col.offset.x <= colOffset.x)
            {
                col.size = col.size + new Vector2(-moveInput.x * Time.deltaTime * auraMovementSpeed, 0f);
                col.offset = col.offset + (new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f) / 2);
            }

            if (moveInput.x < 0 && col.offset.x >= colOffset.x)
            {
                col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
            }
        }
        else if (moveInput.x != 0 && col.offset.y != colOffset.y)
        {
            col.size = new Vector2(colSize.x, Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
            col.offset = new Vector2(colOffset.x, Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
        }

        if (moveInput.y != 0 && moveInput.x == 0 && col.offset.x == colOffset.x)
        {
            col.direction = CapsuleDirection2D.Vertical;

            if (moveInput.y > 0 && col.offset.y >= colOffset.y)
            {
                col.size = col.size + new Vector2(0f, moveInput.y * Time.deltaTime * auraMovementSpeed);
                col.offset = col.offset + (new Vector2(0f, moveInput.y * Time.deltaTime * auraMovementSpeed) / 2);
            }

            if (moveInput.y > 0 && col.offset.y <= colOffset.y)
            {
                col.size = new Vector2(colSize.x, Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                col.offset = new Vector2(colOffset.x, Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
            }

            if (moveInput.y < 0 && col.offset.y <= colOffset.y)
            {
                col.size = col.size + new Vector2(0f, -moveInput.y * Time.deltaTime * auraMovementSpeed);
                col.offset = col.offset + (new Vector2(0f, moveInput.y * Time.deltaTime * auraMovementSpeed) / 2);
            }

            if (moveInput.y < 0 && col.offset.y >= colOffset.y)
            {
                col.size = new Vector2(colSize.x, Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                col.offset = new Vector2(colOffset.x, Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
            }
        }
        else if (moveInput.y != 0 && col.offset.x != colOffset.x)
        {
            col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
            col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
        }

        if (moveInput.x == 0 && moveInput.y == 0)
        {
            col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
            col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AuraBounceTimer(other.gameObject.GetComponentInChildren<AuraBounce>().gameObject);
    }

    private void AuraBounceTimer(GameObject player)
    {
        player.GetComponent<AuraBounce>().player.auraBounce = true;
        player.GetComponent<AuraBounce>().auraBounce = true;
    }
}
