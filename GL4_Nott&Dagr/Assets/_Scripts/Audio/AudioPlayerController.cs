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
    [HideInInspector]
    public PlayerMovementTest _pmt;
    [HideInInspector]
    public AudioPlayerDeciderController _apdc;
    [HideInInspector]
    public ExitLevel _el;
    
    public Rigidbody2D playerRB;


    public Sprite onSprite;
    //[HideInInspector]
    public Sprite[] dirtSprites, grassSprites, iceSprites, snowSprites, stoneSprites, waterSprites, woodSprites;

    public float walkingSpeed;
   
    public bool isDagr, isNott, isSingle, isCoop, isGrounded, isMoving, isMovingDagr, isMovingNott, isClimbing, isFalling, hasLanded, onPlant, isDirt, isGrass, isIce, isPlant, isSnow, isStone, isWater, isWood;

    //--------------------------------------------------------------------------//



    //... find the path to the fmod event.

    //[Header("  Footsteps ")]
    [HideInInspector]
    [FMODUnity.EventRef]                
    public string footstepDirt;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepGrass;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepIce;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepPlant;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepSnow;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepStone;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepWater;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string footstepWood;


    //[Header(" Jump Landings ")]
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandDirt;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandGrass;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandIce;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandPlant;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandSnow;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandStone;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandWater;
    [HideInInspector]
    [FMODUnity.EventRef]
    public string jumpLandWood;

    //--------------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        _el = GameObject.Find("Door").GetComponent<ExitLevel>();

        InvokeRepeating("CallFootsteps", 0, walkingSpeed);

        if (this.gameObject == GameObject.Find("AudioTriggerDagr"))
        {
            playerRB = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
            _pmt = GameObject.Find("Player1").GetComponent<PlayerMovementTest>();
            _apdc = GameObject.Find("Player1").GetComponent<AudioPlayerDeciderController>();
            isDagr = _apdc.isDagr;
            isNott = _apdc.isNott;
            isSingle = _pmt.singlePlayer;

        }

        if (this.gameObject == GameObject.Find("AudioTriggerNott"))
        {
            playerRB = GameObject.Find("Player2").GetComponent<Rigidbody2D>();
            _pmt = GameObject.Find("Player2").GetComponent<PlayerMovementTest>();
            _apdc = GameObject.Find("Player2").GetComponent<AudioPlayerDeciderController>();
            isDagr = _apdc.isDagr;
            isNott = _apdc.isNott;
            isSingle = _pmt.singlePlayer;

        }

        if (isSingle == true)
        {
            isCoop = false;
        }
        else if(isSingle == false)

        {
            isCoop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            if (isSingle == true)
            {
                if (_pmt.playerId == 1)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        isMoving = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        isMoving = false;
                    }
                }
            }

            if (isCoop == true)
            {
                if (_pmt.playerId == 1 && isDagr == false && isNott == true)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        isMovingNott = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        isMovingNott = false;
                    }
                }
                else if (_pmt.playerId == 1 && isDagr == true && isNott == false)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        isMovingDagr = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        isMovingDagr = false;
                    }
                }

                if (_pmt.playerId == 2 && isDagr == false && isNott == true)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        isMovingNott = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        isMovingNott = false;
                    }
                }
                else if (_pmt.playerId == 2 && isDagr == true && isNott == false)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        isMovingDagr = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        isMovingDagr = false;
                    }
                }
            }
        }

        if (isGrounded == false)
        {
            isMovingDagr = false;
            isMovingNott = false;
        }

        //---------JumpLandings---------//
        if (hasLanded == true)
        {

            if (isDirt == true)
            {
                //Debug.Log("Landed in dirt");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandDirt);
            }
            else if (isGrass == true)
            {
                //Debug.Log("Landed in grass");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandGrass);
            }
            else if (isIce == true)
            {
                //Debug.Log("Landed in ice");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandIce);
            }
            else if (isIce == true)
            {
                //Debug.Log("Landed in plant");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandPlant);
            }
            else if (isSnow == true)
            {
                //Debug.Log("Landed in snow");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandSnow);
            }
            else if (isStone == true)
            {
                //Debug.Log("Landed in stone");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandStone);
            }
            else if (isWater == true)
            {
                //Debug.Log("Landed in water");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandWater);
            }
            else if (isWood == true)
            {
                //Debug.Log("Landed in wood");
                FMODUnity.RuntimeManager.PlayOneShot(jumpLandWood);
            }
        }

        //-----------Dagr----------//

        if (isDagr == true && isNott == false)
        {
            onSprite = _pmt.currentSprite;
            isGrounded = _pmt.isGrounded;
            isClimbing = _pmt.isOnLadder;

            // Checks falling.
            if (playerRB.velocity.y <= -3.5f)
            {
                isFalling = true;
            }
            if (playerRB.velocity.y >= -3.5f)
            {
                StartCoroutine(Falling());
            }

            if (onPlant == true && isClimbing == true)
            {
                isFalling = false;
            }

            if (isFalling == true && isGrounded == true)
            {
                hasLanded = true;
            }
            else
            {
                hasLanded = false;
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
            if (onSprite == null)
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

            for (int i = 0; i <= woodSprites.Length - 1; i++)
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
            isClimbing = _pmt.isOnLadder;

            // Checks if players is falling.
            if (playerRB.velocity.y <= -3.5f)
            {
                isFalling = true;
            }
            if (playerRB.velocity.y >= -3.5f)
            {
                StartCoroutine(Falling());
            }

            if (onPlant == true && isClimbing == true)
            {
                isFalling = false;
            }

            if (isFalling == true && isGrounded == true)
            {
                hasLanded = true;
            }
            else
            {
                hasLanded = false;
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
            if (onSprite == null)
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

            for (int i = 0; i <= woodSprites.Length - 1; i++)
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
        if (isSingle == true)
        {
            if (isMoving == true)
            {
                if (isGrounded == true)
                {
                    if (isDirt == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepDirt);
                    }

                    if (isGrass == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepGrass);
                    }

                    if (isIce == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepIce);
                    }
                    /*
                    if (isPlant == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepPlant);
                    }
                    */
                    if (isSnow == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepSnow);
                    }

                    if (isStone == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepStone);
                    }

                    if (isWater == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepWater);
                    }

                    if (isWood == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepWood);
                    }
                }
            }
        }

        if (isCoop == true)
        {
            if (isMovingDagr == true)
            {
                if (isGrounded == true)
                {
                    if (isDirt == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepDirt);
                    }

                    if (isGrass == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepGrass);
                    }

                    if (isIce == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepIce);
                    }
                    /*
                    if (isPlant == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepPlant);
                    }
                    */
                    if (isSnow == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepSnow);
                    }

                    if (isStone == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepStone);
                    }

                    if (isWater == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepWater);
                    }

                    if (isWood == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepWood);
                    }
                }
            }

            if (isMovingNott == true)
            {
                if (isGrounded == true)
                {
                    if (isDirt == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepDirt);
                    }

                    if (isGrass == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepGrass);
                    }

                    if (isIce == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepIce);
                    }
                    /*
                    if (isPlant == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepPlant);
                    }
                    */
                    if (isSnow == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepSnow);
                    }

                    if (isStone == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepStone);
                    }

                    if (isWater == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepWater);
                    }

                    if (isWood == true)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(footstepWood);
                    }
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

    IEnumerator Falling()
    {
        //yield return new WaitForSeconds(0.001f);
        yield return new WaitForEndOfFrame();
        isFalling = false;
    }
}


