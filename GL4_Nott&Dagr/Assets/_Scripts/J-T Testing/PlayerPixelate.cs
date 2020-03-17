using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPixelate : MonoBehaviour
{
    private Material material;

    private float pixelateSpeed;
    private float auraPixelateSpeed;
    private float pixelateAmount;
    private float pixelateAmountTarget;

    private bool isOnAura;

    [HideInInspector]
    public bool pixelating;

    private ExitLevel exit;

    private void Start()
    {
        exit = GameObject.Find("Door").GetComponent<ExitLevel>();
        material = GetComponent<SpriteRenderer>().material;
        pixelateAmountTarget = 1f;
        pixelateAmount = 0;
        pixelateSpeed = 0.30f;
        auraPixelateSpeed = (pixelateSpeed / 2f) * 1.5f;

        if (gameObject.name == "AuraShader")
        {
            isOnAura = true;
            pixelateSpeed = auraPixelateSpeed;
        }
    }

    private void Update()
    {
        pixelating = exit.playerPixelate;

        if (pixelating)
        {
            pixelateAmount = Mathf.MoveTowards(pixelateAmount, pixelateAmountTarget, Time.deltaTime * pixelateSpeed);

            material.SetFloat("_PixelateAmount", pixelateAmount);

            //if (pixelateAmount > 0.5f)
            //{
            //    pixelateSpeed = 0.5f;
            //}
            if (pixelateAmount >= 1 && !isOnAura)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (pixelateAmount >= 0.75f && isOnAura)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
