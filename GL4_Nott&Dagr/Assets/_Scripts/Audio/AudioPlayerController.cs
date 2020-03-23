using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
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
    public Rigidbody2D playerRB;

    [HideInInspector]
    public GameObject dagrTrigger, nottTrigger;

    public Sprite onSprite;

    [HideInInspector]
    public Sprite[] dirtSprites, grassSprites, iceSprites, snowSprites, stoneSprites, waterSprites, woodSprites;

    [HideInInspector]
    public int dgNr, ntNr, gtDagr, fojDagr, gtNott, fojNott;

    public float walkingSpeed;

    [HideInInspector]
    public bool isDagr, isNott, isSingle, isCoop, isGrounded, isMoving, isMovingDagr, isMovingNott, isClimbing, isFalling, hasLanded, onPlant, isDirt, isGrass, isIce, isPlant, isSnow, isStone, isWater, isWood;
    
    private FMOD.Studio.EventInstance gtDagrInstance, fojDagrInstance, gtNottInstance, fojNottInstance;

    //--------------------------------------------------------------------------//

    [Header("  Dagr Footsteps and Landings  ")]

    [FMODUnity.EventRef] 
    public string fjDagr;

    [Header("  Dagr Footsteps and Landings  ")]

    [FMODUnity.EventRef] 
    public string fjNott;

    // Start is called before the first frame update
    void Start()
    {
        dagrTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
        nottTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;

        InvokeRepeating("CallFootsteps", 0, walkingSpeed);

        if (this.gameObject == GameObject.Find("AudioTriggerDagr"))
        {
            playerRB = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
            _pmt = GameObject.Find("Player1").GetComponent<PlayerMovementTest>();
            _apdc = GameObject.Find("Player1").GetComponent<AudioPlayerDeciderController>();

            isDagr = _apdc.isDagr;
            isNott = _apdc.isNott;
            isSingle = _pmt.singlePlayer;

            gtDagrInstance = FMODUnity.RuntimeManager.CreateInstance(fjDagr);
            fojDagrInstance = FMODUnity.RuntimeManager.CreateInstance(fjDagr);
        }

        if (this.gameObject == GameObject.Find("AudioTriggerNott"))
        {
            playerRB = GameObject.Find("Player2").GetComponent<Rigidbody2D>();
            _pmt = GameObject.Find("Player2").GetComponent<PlayerMovementTest>();
            _apdc = GameObject.Find("Player2").GetComponent<AudioPlayerDeciderController>();

            isDagr = _apdc.isDagr;
            isNott = _apdc.isNott;
            isSingle = _pmt.singlePlayer;

            gtNottInstance = FMODUnity.RuntimeManager.CreateInstance(fjNott);
            fojNottInstance = FMODUnity.RuntimeManager.CreateInstance(fjNott);
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
        //----------Grounded------------//
        if (isGrounded == true)
        {
            if (isDagr == true)
            {
                fojDagrInstance.setParameterByName("fojDagr", fojDagr = 0);
            }
            if (isNott == true)
            {
                fojNottInstance.setParameterByName("fojNott", fojNott = 0);
            }

            if (isSingle == true)
            {
                dgNr = _pmt.playerId;
                ntNr = _pmt.playerId;

                if (_pmt.playerId == 1 && dagrTrigger.GetComponent<AudioPlayerController>().dgNr == 1)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        dagrTrigger.GetComponent<AudioPlayerController>().isMoving = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        dagrTrigger.GetComponent<AudioPlayerController>().isMoving = false;
                    }
                }
                if (_pmt.playerId == 1 && dagrTrigger.GetComponent<AudioPlayerController>().dgNr == 2)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        dagrTrigger.GetComponent<AudioPlayerController>().isMoving = false;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        dagrTrigger.GetComponent<AudioPlayerController>().isMoving = false;
                    }
                }
                if (_pmt.playerId == 1 && nottTrigger.GetComponent<AudioPlayerController>().ntNr == 1)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        nottTrigger.GetComponent<AudioPlayerController>().isMoving = true;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        nottTrigger.GetComponent<AudioPlayerController>().isMoving = false;
                    }
                }
                if (_pmt.playerId == 1 && nottTrigger.GetComponent<AudioPlayerController>().ntNr == 2)
                {
                    if (_pmt.moveInput.x > 0.1 || _pmt.moveInput.x < -0.1)
                    {
                        nottTrigger.gameObject.GetComponent<AudioPlayerController>().isMoving = false;
                    }
                    else if (_pmt.moveInput.x > -0.1 || _pmt.moveInput.x < 0.1)
                    {
                        nottTrigger.GetComponent<AudioPlayerController>().isMoving = false;
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

        //----------NotGrounded------------//
        if (isGrounded == false)
        {
            isMoving = false;
            isMovingDagr = false;
            isMovingNott = false;

            if (isDagr == true)
            {
                fojDagrInstance.setParameterByName("fojDagr", fojDagr = 1);
            }
            if (isNott == true)
            {
                fojNottInstance.setParameterByName("fojNott", fojNott = 1);
            }
        }

        //---------JumpLandings---------//
        if (hasLanded == true)
        {
            if (isDirt == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 0);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 0);
                    gtNottInstance.start();
                }
            }
            else if (isGrass == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 1);
                    gtDagrInstance.start();
                }

                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 1);
                    gtNottInstance.start();
                }
            }
            else if (isIce == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 2);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 2);
                    gtNottInstance.start();
                }
            }
            else if (isPlant == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 3);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 3);
                    gtNottInstance.start();
                }
            }
            else if (isSnow == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 4);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 4);
                    gtNottInstance.start();
                }
            }
            else if (isStone == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 5);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 5);
                    gtNottInstance.start();
                }
            }
            else if (isWater == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 6);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 6);
                    gtNottInstance.start();
                }
            }
            else if (isWood == true)
            {
                if (isDagr == true)
                {
                    gtDagrInstance.setParameterByName("gtDagr", gtDagr = 7);
                    gtDagrInstance.start();
                }
                if (isNott == true)
                {
                    gtNottInstance.setParameterByName("gtNott", gtNott = 7);
                    gtNottInstance.start();
                }
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
                isPlant = false;
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
                isPlant = false;
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
                    isPlant = false;
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
                    isPlant = false;
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
                    isPlant = false;
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
                    isPlant = false;
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
                    isPlant = false;
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
                    isPlant = false;
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
                    isPlant = false;
                    isSnow = false;
                    isStone = false;
                    isWater = false;
                    isWood = true;
                }
            }
        }

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
    }

    // Plays the footsteps FMod events.
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
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 0);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 0);
                            gtNottInstance.start();
                        }
                    }
                    if (isGrass == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 1);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 1);
                            gtNottInstance.start();
                        }
                    }
                    if (isIce == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 2);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 2);
                            gtNottInstance.start();
                        }
                    }
                    if (isPlant == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 3);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 3);
                            gtNottInstance.start();
                        }
                    }
                    if (isSnow == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 4);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 4);
                            gtNottInstance.start();
                        }
                    }
                    if (isStone == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 5);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 5);
                            gtNottInstance.start();
                        }
                    }
                    if (isWater == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 6);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 6);
                            gtNottInstance.start();
                        }
                    }
                    if (isWood == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 7);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 7);
                            gtNottInstance.start();
                        }
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
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 0);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 0);
                            gtNottInstance.start();
                        }
                    }
                    if (isGrass == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 1);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 1);
                            gtNottInstance.start();
                        }
                    }
                    if (isIce == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 2);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 2);
                            gtNottInstance.start();
                        }
                    }
                    if (isPlant == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 3);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 3);
                            gtNottInstance.start();
                        }
                    }
                    if (isSnow == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 4);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 4);
                            gtNottInstance.start();
                        }
                    }
                    if (isStone == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 5);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 5);
                            gtNottInstance.start();
                        }
                    }
                    if (isWater == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 6);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 6);
                            gtNottInstance.start();
                        }
                    }
                    if (isWood == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 7);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 7);
                            gtNottInstance.start();
                        }
                    }
                }
            }

            if (isMovingNott == true)
            {
                if (isGrounded == true)
                {
                    if (isDirt == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 0);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 0);
                            gtNottInstance.start();
                        }
                    }
                    if (isGrass == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 1);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 1);
                            gtNottInstance.start();
                        }
                    }
                    if (isIce == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 2);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 2);
                            gtNottInstance.start();
                        }
                    }
                    if (isPlant == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 3);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 3);
                            gtNottInstance.start();
                        }
                    }
                    if (isSnow == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 4);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 4);
                            gtNottInstance.start();
                        }
                    }
                    if (isStone == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 5);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 5);
                            gtNottInstance.start();
                        }
                    }
                    if (isWater == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 6);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 6);
                            gtNottInstance.start();
                        }
                    }
                    if (isWood == true)
                    {
                        if (isDagr == true)
                        {
                            gtDagrInstance.setParameterByName("gtDagr", gtDagr = 7);
                            gtDagrInstance.start();
                        }
                        if (isNott == true)
                        {
                            gtNottInstance.setParameterByName("gtNott", gtNott = 7);
                            gtNottInstance.start();
                        }
                    }
                }
            }
        }
    }
    //--------------------------------// 

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


