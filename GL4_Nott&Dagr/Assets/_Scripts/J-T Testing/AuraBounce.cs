using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraBounce : MonoBehaviour
{
    public bool canMoveAura;

    [Range(1, 20)]
    public float auraMovementSpeed = 5;

    [Range(1, 10)]
    public int auraRange = 2;

    private float auraRangeStart;

    private PlayerController player;

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

    public Color dayAuraColor;
    public Color nightAuraColor;
    public float colorIntensity;
    private Color auraColorOutput;

    [Range(0f, 0.6f)]
    public float targetFade = 0;
    [Range(0.5f, 3f)]
    public float fadeSpeed = 1;
    private float currentFade;

    [HideInInspector]
    public bool isFading;
    [HideInInspector]
    public bool isRestoring;

    private float pixelStart;
    private float pixelTarget;
    private float pixelAmount;

    private PlayerPixelate pixelate;

    public GameObject auraPickupRight, auraPickupLeft;
    private Vector3 auraPickUpRightPos, auraPickUpLeftPos;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        if (GetComponent<CapsuleCollider2D>())
        {
            hasCapsuleCollider = true;
        }

        if (hasCapsuleCollider)
        {
            pixelate = transform.GetChild(0).GetComponent< PlayerPixelate>();
            col = GetComponent<CapsuleCollider2D>();
            colSize = col.size;
            colOffset = col.offset;
            auraRangeStart = colSize.x;
            auraVisuals = transform.GetChild(0).gameObject;
            auraMat = auraVisuals.GetComponent<SpriteRenderer>().material;
            auraWidth = auraMat.GetFloat("_Width");
            auraHeight = auraMat.GetFloat("_Height");
            auraOffset = auraMat.GetVector("_Offset");
            pixelStart = auraMat.GetFloat("_PixelateAmount");
            pixelTarget = pixelStart * 2;
            pixelAmount = pixelStart;

            auraPickUpRightPos = auraPickupRight.transform.localPosition;
            auraPickUpLeftPos = auraPickupLeft.transform.localPosition;

            colorIntensity += 1;
            if (player.setPlayer.ToString() == "Night")
            {
                auraColorOutput = new Color(nightAuraColor.r * colorIntensity, nightAuraColor.g * colorIntensity, nightAuraColor.b * colorIntensity, nightAuraColor.a);
                auraMat.SetColor("_Color", auraColorOutput);
                currentFade = auraMat.GetColor("_Color").a;
            }

            if (player.setPlayer.ToString() == "Day")
            {
                auraColorOutput = new Color(dayAuraColor.r * colorIntensity, dayAuraColor.g * colorIntensity, dayAuraColor.b * colorIntensity, dayAuraColor.a);
                auraMat.SetColor("_Color", auraColorOutput);
                currentFade = auraMat.GetColor("_Color").a;
            }
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        playerId = player.playerId;
        if (canMoveAura)
        {
            moveInput.x = Input.GetAxisRaw("RStickHorizontalP" + playerId);
            moveInput.y = Input.GetAxisRaw("RStickVerticalP" + playerId);
        }
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

        if (isFading)
        {
            auraMat.SetColor("_Color", new Vector4(auraColorOutput.r, auraColorOutput.g, auraColorOutput.b, Mathf.MoveTowards(auraMat.GetColor("_Color").a, targetFade, Time.deltaTime * fadeSpeed)));

            if (auraMat.GetColor("_Color").a <= targetFade)
            {
                isFading = false;
            }
        }
        else if (isRestoring)
        {
            auraMat.SetColor("_Color", new Vector4(auraColorOutput.r, auraColorOutput.g, auraColorOutput.b, Mathf.MoveTowards(auraMat.GetColor("_Color").a, currentFade, Time.deltaTime * fadeSpeed)));

            if (auraMat.GetColor("_Color").a >= currentFade)
            {
                isRestoring = false;
            }
        }

        if (hasCapsuleCollider)
        {
            if (!pixelate.pixelating)
            {
                pixelAmount = Mathf.MoveTowards(pixelAmount, pixelTarget, Time.deltaTime * 0.01f);
                if (pixelAmount >= pixelTarget)
                {
                    pixelAmount = pixelStart;
                }
                auraMat.SetFloat("_PixelateAmount", pixelAmount);
            }



            if (moveInput.x != 0 && moveInput.y == 0 && col.offset.y == colOffset.y && auraMat.GetVector("_Offset").y == auraOffset.y)
            {
                col.direction = CapsuleDirection2D.Horizontal;

                if (moveInput.x > 0 && col.size.x < auraRangeStart * auraRange && col.offset.x >= colOffset.x && auraMat.GetFloat("_Width") < auraWidth * auraRange)
                {
                    col.size = col.size + new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f);
                    col.offset = col.offset + (new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f) / 2);
                    auraMat.SetFloat("_Width", auraMat.GetFloat("_Width") + moveInput.x * (Time.deltaTime * auraMovementSpeed) / 16);
                    auraMat.SetVector("_Offset", auraMat.GetVector("_Offset") + (new Vector4(-moveInput.x * ((Time.deltaTime * auraMovementSpeed) / 2) / 16 , 0f, 0f, 0f)));
                    auraPickupRight.transform.localPosition = auraPickUpRightPos + new Vector3(col.offset.x * 2, 0f, 0f);
                }

                if (moveInput.x > 0 && col.offset.x <= colOffset.x && auraMat.GetVector("_Offset").x >= auraOffset.x)
                {
                    col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                    col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
                    auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                    auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), auraOffset.y));
                    auraPickupRight.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupRight.transform.localPosition.x, auraPickUpRightPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpRightPos.y, auraPickUpRightPos.z);
                    auraPickupLeft.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupLeft.transform.localPosition.x, auraPickUpLeftPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpLeftPos.y, auraPickUpLeftPos.y);
                }

                if (moveInput.x < 0 && col.size.x < auraRangeStart * auraRange && col.offset.x <= colOffset.x && auraMat.GetFloat("_Width") < auraWidth * auraRange)
                {
                    col.size = col.size + new Vector2(-moveInput.x * Time.deltaTime * auraMovementSpeed, 0f);
                    col.offset = col.offset + (new Vector2(moveInput.x * Time.deltaTime * auraMovementSpeed, 0f) / 2);
                    auraMat.SetFloat("_Width", auraMat.GetFloat("_Width") + -moveInput.x * (Time.deltaTime * auraMovementSpeed) / 16);
                    auraMat.SetVector("_Offset", auraMat.GetVector("_Offset") + (new Vector4(-moveInput.x * ((Time.deltaTime * auraMovementSpeed) / 2) / 16, 0f, 0f, 0f)));
                    auraPickupLeft.transform.localPosition = auraPickUpLeftPos + new Vector3(col.offset.x * 2, 0f, 0f);
                }

                if (moveInput.x < 0 && col.offset.x >= colOffset.x && auraMat.GetVector("_Offset").x <= auraOffset.x)
                {
                    col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), colSize.y);
                    col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), colOffset.y);
                    auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                    auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), auraOffset.y));
                    auraPickupLeft.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupLeft.transform.localPosition.x, auraPickUpLeftPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpLeftPos.y, auraPickUpLeftPos.y);
                    auraPickupRight.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupRight.transform.localPosition.x, auraPickUpRightPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpRightPos.y, auraPickUpRightPos.z);
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
                auraPickupRight.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupRight.transform.localPosition.x, auraPickUpRightPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpRightPos.y, auraPickUpRightPos.z);
                auraPickupLeft.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupLeft.transform.localPosition.x, auraPickUpLeftPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpLeftPos.y, auraPickUpLeftPos.y);
            }

            if (moveInput.x == 0 && moveInput.y == 0)
            {
                col.size = new Vector2(Mathf.MoveTowards(col.size.x, colSize.x, Time.deltaTime * auraMovementSpeed), Mathf.MoveTowards(col.size.y, colSize.y, Time.deltaTime * auraMovementSpeed));
                col.offset = new Vector2(Mathf.MoveTowards(col.offset.x, colOffset.x, (Time.deltaTime * auraMovementSpeed) / 2), Mathf.MoveTowards(col.offset.y, colOffset.y, (Time.deltaTime * auraMovementSpeed) / 2));
                auraMat.SetFloat("_Width", Mathf.MoveTowards(auraMat.GetFloat("_Width"), auraWidth, (Time.deltaTime * auraMovementSpeed) / 16));
                auraMat.SetFloat("_Height", Mathf.MoveTowards(auraMat.GetFloat("_Height"), auraHeight, (Time.deltaTime * auraMovementSpeed) / 16));
                auraMat.SetVector("_Offset", new Vector2(Mathf.MoveTowards(auraMat.GetVector("_Offset").x, auraOffset.x, (((Time.deltaTime * auraMovementSpeed) / 2) / 16)), Mathf.MoveTowards(auraMat.GetVector("_Offset").y, auraOffset.y, (((Time.deltaTime * auraMovementSpeed) / 2) / 16))));
                auraPickupRight.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupRight.transform.localPosition.x, auraPickUpRightPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpRightPos.y, auraPickUpRightPos.z);
                auraPickupLeft.transform.localPosition = new Vector3(Mathf.MoveTowards(auraPickupLeft.transform.localPosition.x, auraPickUpLeftPos.x, (Time.deltaTime * auraMovementSpeed)), auraPickUpLeftPos.y, auraPickUpLeftPos.y);
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
