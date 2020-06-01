using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    public int unfreezeDuration;

    private bool unfreeze;
    private float unfreezeTimer = 0;
    private int unfreezeNumber = 0;
    private SpriteRenderer theSR;

    public Color unfreezeColor;
    private Color startColor;

    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        startColor = theSR.color;    
    }

    private void Update()
    {
        if (unfreeze)
        {
            float freezeSpeed = 4;

            if (unfreezeTimer < 1)
            {
                unfreezeTimer += Time.deltaTime * freezeSpeed;
            }
            if (unfreezeTimer >= 1)
            {
                unfreezeNumber++;
                unfreezeTimer = 0;
            }
            if (unfreezeNumber % 2 == 0)
            {
                theSR.color = startColor;
            }
            else
            {
                theSR.color = unfreezeColor;
            }
            if (unfreezeNumber == unfreezeDuration * 2)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().setPlayer.ToString() == "Day")
            {
                unfreeze = true;
            }
        }
    }
}
