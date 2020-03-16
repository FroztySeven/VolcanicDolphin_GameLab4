using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPixelate : MonoBehaviour
{
    private Material material;

    private float pixelateSpeed;
    private float pixelateAmount;
    private float pixelateAmountTarget;

    private bool pixelating;

    private ExitLevel exit;

    private void Start()
    {
        exit = GameObject.Find("Door").GetComponent<ExitLevel>();
        material = GetComponent<SpriteRenderer>().material;
        pixelateAmountTarget = 1;
        pixelateAmount = 0;
        pixelateSpeed = 1;
    }

    private void Update()
    {
        pixelating = exit.playerPixelate;

        if (pixelating)
        {
            pixelateAmount = Mathf.Lerp(pixelateAmount, pixelateAmountTarget, Time.deltaTime * pixelateSpeed);

            material.SetFloat("_PixelateAmount", pixelateAmount);

            //if (pixelateAmount > 0.5f)
            //{
            //    pixelateSpeed = 0.5f;
            //}
            if (pixelateAmount > 0.95f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
