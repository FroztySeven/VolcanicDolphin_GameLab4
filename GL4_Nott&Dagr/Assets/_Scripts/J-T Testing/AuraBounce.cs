using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraBounce : MonoBehaviour
{
    [Range(1, 20)]
    public float auraMovementSpeed = 5;

    [Range(1, 10)]
    public int auraRange = 2;

    private float auraRangeStart;

    private PlayerMovementTest player;

    private float auraBounceTimer;
    private bool auraBounce;
    private int playerId;

    private Vector2 moveInput;
    private CapsuleCollider2D col;
    private Vector2 colSize, colOffset;

    private bool hasCapsuleCollider;

    private GameObject auraVisuals;
    private Material auraMat;

    private float auraWidth, auraHeight;
    private Vector4 auraOffset;

    public Color auraColor;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementTest>();
        if (GetComponent<CapsuleCollider2D>())
        {
            hasCapsuleCollider = true;
        }

        if (hasCapsuleCollider)
        {
            col = GetComponent<CapsuleCollider2D>();
            colSize = col.size;
            colOffset = col.offset;
            auraRangeStart = colSize.x;
            auraVisuals = transform.GetChild(0).gameObject;
            auraMat = auraVisuals.GetComponent<SpriteRenderer>().material;
            auraMat.SetColor("_Color", auraColor);
            auraWidth = auraMat.GetFloat("_Width");
            auraHeight = auraMat.GetFloat("_Height");
            auraOffset = auraMat.GetVector("_Offset");
        }
    }

    private void Update()
    {
        playerId = player.playerId;
        moveInput.x = Input.GetAxisRaw("RStickHorizontalP" + playerId);
        moveInput.y = Input.GetAxisRaw("RStickVerticalP" + playerId);
        //moveInput.Normalize();

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

        if (hasCapsuleCollider)
        {
            if (moveInput.x != 0 && moveInput.y == 0 && col.offset.y == colOffset.y && auraMat.GetVector("_Offset").y == auraOffset.y)
            {
                col.direction = CapsuleDirection2D.Horizontal;

                if (moveInput.x > 0 && col.size.x < auraRangeStart * auraRange && col.offset.x >= colOffset.x && auraMat.GetFloat("_Width") < auraWidth * auraRange)
                {
                    col.size = col.size + new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f);
                    col.offset = col.offset + (new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f) / 2);
                    auraMat.SetFloat("_Width", auraMat.GetFloat("_Width") + moveInput.x * (Time.deltaTime * auraMovementSpeed) / 16);
                    auraMat.SetVector("_Offset", auraMat.GetVector("_Offset") + (new Vector4(-moveInput.x * ((Time.deltaTime * auraMovementSpeed) / 2) / 16 , 0f, 0f, 0f)));
                }

                if (moveInput.x > 0 && col.offset.x <= colOffset.x && auraMat.GetVector("_Offset").x >= auraOffset.x)
                {
                    col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                    col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
                    auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                    auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), auraOffset.y));
                }

                if (moveInput.x < 0 && col.size.x < auraRangeStart * auraRange && col.offset.x <= colOffset.x && auraMat.GetFloat("_Width") < auraWidth * auraRange)
                {
                    col.size = col.size + new Vector2(-moveInput.x * Time.deltaTime * auraMovementSpeed, 0f);
                    col.offset = col.offset + (new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f) / 2);
                    auraMat.SetFloat("_Width", auraMat.GetFloat("_Width") + -moveInput.x * (Time.deltaTime * auraMovementSpeed) / 16);
                    auraMat.SetVector("_Offset", auraMat.GetVector("_Offset") + (new Vector4(-moveInput.x * ((Time.deltaTime * auraMovementSpeed) / 2) / 16, 0f, 0f, 0f)));
                }

                if (moveInput.x < 0 && col.offset.x >= colOffset.x && auraMat.GetVector("_Offset").x <= auraOffset.x)
                {
                    col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                    col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
                    auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                    auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), auraOffset.y));
                }
            }
            else if (moveInput.x != 0 && col.offset.y != colOffset.y && auraMat.GetVector("_Offset").y != auraOffset.y)
            {
                col.size = new Vector2(colSize.x, Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                col.offset = new Vector2(colOffset.x, Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
                auraMat.SetFloat("_Height", Mathf.MoveTowards(auraMat.GetFloat("_Height"), auraHeight, (Time.deltaTime * auraMovementSpeed) / 16));
                auraMat.SetVector("_Offset", new Vector2(auraOffset.x, Mathf.MoveTowards(auraMat.GetVector("_Offset").y, auraOffset.y, (((Time.deltaTime * auraMovementSpeed) / 2) / 16))));
            }

            if (moveInput.y != 0 && moveInput.x == 0 && col.offset.x == colOffset.x && auraMat.GetVector("_Offset").x == auraOffset.x)
            {
                col.direction = CapsuleDirection2D.Vertical;

                if (moveInput.y > 0 && col.size.y < auraRangeStart * auraRange && col.offset.y >= colOffset.y && auraMat.GetFloat("_Height") < auraHeight * auraRange)
                {
                    col.size = col.size + new Vector2(0f, moveInput.y * Time.deltaTime * auraMovementSpeed);
                    col.offset = col.offset + (new Vector2(0f, moveInput.y * Time.deltaTime * auraMovementSpeed) / 2);
                    auraMat.SetFloat("_Height", auraMat.GetFloat("_Height") + moveInput.y * (Time.deltaTime * auraMovementSpeed) / 16);
                    auraMat.SetVector("_Offset", auraMat.GetVector("_Offset") + (new Vector4(0f, -moveInput.y * ((Time.deltaTime * auraMovementSpeed) / 2) / 16, 0f, 0f)));
                }

                if (moveInput.y > 0 && col.offset.y <= colOffset.y && auraMat.GetVector("_Offset").y >= auraOffset.y)
                {
                    col.size = new Vector2(colSize.x, Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                    col.offset = new Vector2(colOffset.x, Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
                    auraMat.SetFloat("_Height", Mathf.MoveTowards(auraMat.GetFloat("_Height"), auraHeight, (Time.deltaTime * auraMovementSpeed) / 16));
                    auraMat.SetVector("_Offset", new Vector2(auraOffset.x, Mathf.MoveTowards(auraMat.GetVector("_Offset").y, auraOffset.y, (((Time.deltaTime * auraMovementSpeed) / 2) / 16))));
                }

                if (moveInput.y < 0 && col.size.y < auraRangeStart * auraRange && col.offset.y <= colOffset.y && auraMat.GetFloat("_Height") < auraHeight * auraRange)
                {
                    col.size = col.size + new Vector2(0f, -moveInput.y * Time.deltaTime * auraMovementSpeed);
                    col.offset = col.offset + (new Vector2(0f, moveInput.y * Time.deltaTime * auraMovementSpeed) / 2);
                    auraMat.SetFloat("_Height", auraMat.GetFloat("_Height") + -moveInput.y * (Time.deltaTime * auraMovementSpeed) / 16);
                    auraMat.SetVector("_Offset", auraMat.GetVector("_Offset") + (new Vector4(0f, -moveInput.y * ((Time.deltaTime * auraMovementSpeed) / 2) / 16, 0f, 0f)));
                }

                if (moveInput.y < 0 && col.offset.y >= colOffset.y && auraMat.GetVector("_Offset").y <= auraOffset.y)
                {
                    col.size = new Vector2(colSize.x, Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                    col.offset = new Vector2(colOffset.x, Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
                    auraMat.SetFloat("_Height", Mathf.MoveTowards(auraMat.GetFloat("_Height"), auraHeight, (Time.deltaTime * auraMovementSpeed) / 16));
                    auraMat.SetVector("_Offset", new Vector2(auraOffset.x, Mathf.MoveTowards(auraMat.GetVector("_Offset").y, auraOffset.y, (((Time.deltaTime * auraMovementSpeed) / 2) / 16))));
                }
            }
            else if (moveInput.y != 0 && col.offset.x != colOffset.x && auraMat.GetVector("_Offset").x != auraOffset.x)
            {
                col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
                auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), auraOffset.y));
            }

            if (moveInput.x == 0 && moveInput.y == 0)
            {
                col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
                auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                auraMat.SetFloat("_Height", Mathf.MoveTowards(auraMat.GetFloat("_Height"), auraHeight, (Time.deltaTime * auraMovementSpeed) / 16));
                auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), Mathf.MoveTowards(auraMat.GetVector("_Offset").y, auraOffset.y, (((Time.deltaTime * auraMovementSpeed) / 2) / 16))));
            }
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
