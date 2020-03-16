using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    //... this script mostly controls the player audio, vocal, movement, skills or anything else player related.
    //... it can use the terrain data to see what type of ground the player is standing on, to get the right type of footsteps sounds.

    //--------------------------------------------------------------------------//
    public PlayerMovementTest _pmt;
    public AudioPlayerDeciderController _apdc;

    public Rigidbody2D playerRB;

    public Sprite onSprite;
    public Sprite[] dirtSprites, grassSprites, iceSprites, snowSprites, stoneSprites, waterSprites, woodSprites;

    private int  plant, dirt, grass, ice, snow, stone, water, wood;

    public float walkingSpeed;

    public bool isDagr, isNott, isGrounded, isMoving, isClimbing, isFalling, onPlant, isDirt, isGrass, isIce, isSnow, isStone, isWater, isWood;

    //--------------------------------------------------------------------------//

    //... find the path to the fmod event.

    [Header("  Footsteps ")]

    [FMODUnity.EventRef]                
    public string footsteps;

    [Header(" Jump Landings ")]

    [FMODUnity.EventRef]
    public string jumpLands;

    //--------------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);

        if (this.gameObject == GameObject.Find("AudioTriggerDagr"))
        {
            playerRB = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
            _pmt = GameObject.Find("Player1").GetComponent<PlayerMovementTest>();
            _apdc = GameObject.Find("Player1").GetComponent<AudioPlayerDeciderController>();
            isDagr = _apdc.isDagr;
            isNott = _apdc.isNott;
            
        }

        if (this.gameObject == GameObject.Find("AudioTriggerNott"))
        {
            playerRB = GameObject.Find("Player2").GetComponent<Rigidbody2D>();
            _pmt = GameObject.Find("Player2").GetComponent<PlayerMovementTest>();
            _apdc = GameObject.Find("Player2").GetComponent<AudioPlayerDeciderController>();
            isDagr = _apdc.isDagr;
            isNott = _apdc.isNott;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //-----------Dagr----------//

        if (isDagr == true && isNott == false)
        {
            onSprite = _pmt.currentSprite;
            isGrounded = _pmt.isGrounded;
            //isMoving = _pmt.canMove;
            isClimbing = _pmt.isOnLadder;

            // Checks falling.
            if (playerRB.velocity.y <= -3.5f)
            {
                isFalling = true;
            }
            if (playerRB.velocity.y >= -3.5f)
            {
                isFalling = false;
            }

            if (onPlant == true && isClimbing == true)
            {
                isFalling = false;
            }
            if (isFalling == true)
            {
                isMoving = false;
            }
            else
            {
                isMoving = _pmt.canMove;
            }

            // Check type of ground
            if (isGrounded == false)
            {
                isDirt = false;
                isGrass = false;
                isIce = false;
                isSnow = false;
                isStone = false;
                isWater = false;
                isWood = false;
            }

            for (int i = 0; i <= dirtSprites.Length -1; i++)
            {
                if (onSprite == dirtSprites[i])
                {
                    isDirt = true;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }

            for (int i = 0; i <= grassSprites.Length -1; i++)
            {
                if (onSprite == grassSprites[i])
                {
                    isDirt = false;
                    isGrass = true;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }

            for (int i = 0; i <= iceSprites.Length - 1; i++)
            {
                if (onSprite == iceSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = true;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }


            for (int i = 0; i <= snowSprites.Length - 1; i++)
            {
                if (onSprite == snowSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = true;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }
            
            for (int i = 0; i <= stoneSprites.Length - 1; i++)
            {
                if (onSprite == stoneSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = true;
                    isWater = false;
                    isWood = false;
                }
            }


            for (int i = 0; i <= waterSprites.Length - 1; i++)
            {
                if (onSprite == waterSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = true;
                    isWood = false;
                }
            }


            for (int i = 0; i <= grassSprites.Length - 1; i++)
            {
                if (onSprite == woodSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = true;
                }
            }
            
        }
        //--------------------------------//



        //-----------Nótt----------//
        if (isDagr == false && isNott == true)
        {
            onSprite = _pmt.currentSprite;
            isGrounded = _pmt.isGrounded;
            //isMoving = _pmt.canMove;
            isClimbing = _pmt.isOnLadder;

            // Checks if players is falling.
            if (playerRB.velocity.y <= -3.5f)
            {
                isFalling = true;
            }
            if (playerRB.velocity.y >= -3.5f)
            {
                isFalling = false;
            }

            if (onPlant == true && isClimbing == true)
            {
                isFalling = false;
            }

            if (isFalling == true)
            {
                isMoving = false;
            }
            else
            {
                isMoving = _pmt.canMove;
            }

            // Check type of ground
            if (isGrounded == false)
            {
                isDirt = false;
                isGrass = false;
                isIce = false;
                isSnow = false;
                isStone = false;
                isWater = false;
                isWood = false;
            }

            for (int i = 0; i <= dirtSprites.Length - 1; i++)
            {
                if (onSprite == dirtSprites[i])
                {
                    isDirt = true;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }

            for (int i = 0; i <= grassSprites.Length - 1; i++)
            {
                if (onSprite == grassSprites[i])
                {
                    isDirt = false;
                    isGrass = true;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }

            for (int i = 0; i <= iceSprites.Length - 1; i++)
            {
                if (onSprite == iceSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = true;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }


            for (int i = 0; i <= snowSprites.Length - 1; i++)
            {
                if (onSprite == snowSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = true;
                    isStone = false;
                    isWater = false;
                    isWood = false;
                }
            }

            for (int i = 0; i <= stoneSprites.Length - 1; i++)
            {
                if (onSprite == stoneSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = true;
                    isWater = false;
                    isWood = false;
                }
            }


            for (int i = 0; i <= waterSprites.Length - 1; i++)
            {
                if (onSprite == waterSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = true;
                    isWood = false;
                }
            }


            for (int i = 0; i <= grassSprites.Length - 1; i++)
            {
                if (onSprite == woodSprites[i])
                {
                    isDirt = false;
                    isGrass = false;
                    isIce = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = true;
                }
            }
        }

        //--------------------------------// 
    }

    // Plays the footsteps FMod events.
    //--------------------------------// 
    void CallFootsteps()
    {
        if (isMoving == true)
        {
            if (isGrounded == true)
            {
                if (isGrass == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(footsteps);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant") && isDagr == true)
        {
            onPlant = true;
        }

        if (other.gameObject.CompareTag("Plant") && isNott == true)
        {
            onPlant = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant") && isDagr == true)
        {
            onPlant = false;
        }

        if (other.gameObject.CompareTag("Plant") && isNott == true)
        {
            onPlant = false;
        }
    }
}


