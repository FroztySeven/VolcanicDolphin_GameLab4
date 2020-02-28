using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Sprite[] animatedWater;
    public Sprite frozenWater;
    public Sprite crackingWater;

    public float animationRate;
    public int unfreezeDuration;

    private SpriteRenderer theSR;

    //[HideInInspector]
    public bool isFrozen;
    public bool unfreeze;

    private float spriteTimer = 0;
    private int spriteNumber = 0;

    [HideInInspector]
    public float unfreezeTimer = 0;
    [HideInInspector]
    public int unfreezeNumber = 0;

    //[HideInInspector]
    public int waterId;

    private WaterPit waterPit;

    public GameObject night;

    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        waterPit = GetComponentInParent<WaterPit>();
        waterPit.animationFrames = animatedWater.Length;
    }

    private void Update()
    {
        //if (!isFrozen)
        //{
        //    //if (spriteTimer < 1)
        //    //{
        //    //    spriteTimer += Time.deltaTime * animationRate;
        //    //}
        //    //if (spriteTimer >= 1)
        //    //{
        //    //    spriteNumber++;
        //    //    spriteTimer = 0;
        //    //}
        //    //if (spriteNumber == animatedWater.Length)
        //    //{
        //    //    spriteNumber = 0;
        //    //}

        //    gameObject.layer = 10;
        //    theSR.color = Color.white;
        //    theSR.sprite = animatedWater[waterPit.spriteNumber];
        //    unfreezeNumber = 0;
        //    unfreezeTimer = 0;
        //    unfreeze = false;
        //}

        if (isFrozen)
        {
            //gameObject.layer = default;
            //theSR.sprite = frozenWater;

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
                if (unfreezeNumber %2 == 0)
                {
                    theSR.sprite = crackingWater;
                }
                else
                {
                    theSR.sprite = frozenWater;
                }
                if (unfreezeNumber == unfreezeDuration * 2)
                {
                    unfreezeNumber = 0;
                    unfreezeTimer = 0;
                    unfreeze = false;
                    waterPit.UnfreezeWater(waterId, true);
                    //StartCoroutine(waterPit.UnFreezeWater(waterId, waterId));
                }
            }
        }
    }

    public void Frozen()
    {
        gameObject.layer = default;
        theSR.sprite = frozenWater;
        isFrozen = true;
    }

    public void Unfrozen()
    {
        gameObject.layer = 10;
        theSR.color = Color.white;
        theSR.sprite = animatedWater[waterPit.spriteNumber];
        unfreezeNumber = 0;
        unfreezeTimer = 0;
        unfreeze = false;
        isFrozen = false;

        if (night != null)
        {
            night.transform.position += new Vector3(0f, 0.01f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                night = other.gameObject;
                if (!isFrozen)
                {
                    waterPit.FreezeWater(waterId, true);
                }
            }

            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
            {
                if (isFrozen)
                {
                    unfreeze = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
            {
                night = null;
            }
        }
    }
}
