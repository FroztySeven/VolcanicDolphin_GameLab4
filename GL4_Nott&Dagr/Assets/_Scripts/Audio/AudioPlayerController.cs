using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    //... this script mostly controls the players audio, vocal, movement, skills or anything else players related.
    //... it checks the sprites data to see what type of ground the players is standing on, to get the right type of footsteps sounds.

    //--------------------------------------------------------------------------//
    [HideInInspector]
    public PlayerController _pmt;
    [HideInInspector]
    public Rigidbody2D playerRB;
    [HideInInspector]
    public Transform playerTrans;
    [HideInInspector]
    public GameObject audioTrigger, door, pause;

    public Sprite onSprite;
    [HideInInspector]
    public Sprite[] dirtSprites, grassSprites, iceSprites, snowSprites, stoneSprites, waterSprites, woodSprites;

    [SerializeField][Range(0,1)]
    public float walkingSpeed;

    [HideInInspector]
    public float height, oldHeight, heightDiff;

    //[HideInInspector]
    public bool isDagr, isNott, isGrounded, isClimbing, isFalling, isMoving, isTeleporting, hasLanded, wasGrounded = true, onPlant, isDirt, isGrass, isIce, isPlant, isSnow, isStone, isWater, isWood;

    //--------------------------------------------------------------------------//

    private int gt, fol;

    private FMOD.Studio.EventInstance gtInstance, folInstance;

    //--------------------------------------------------------------------------//

    [Header("  Teleport Players SFX  ")]

    [FMODUnity.EventRef]
    public string playerTeleport;

    [Header("  Footsteps and Jump Landings  ")]

    [FMODUnity.EventRef]
    public string footstepsLandings;

    //-----------------------------------------//

    void Awake()
    {
        GameObject.Find("Player1").transform.Find("AudioTriggerNott").gameObject.SetActive(false);
        GameObject.Find("Player2").transform.Find("AudioTriggerDagr").gameObject.SetActive(false);
    }

    void Start()
    {
        PlayerStart();

        door = GameObject.Find("Door");
        pause = GameObject.Find("PauseMenu");

        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }

    void FixedUpdate()
    {
        CheckLanding();
        wasGrounded = isGrounded;

        CheckFalling();

        if (wasGrounded == false)
        {
            isFalling = true;
            isMoving = false;
        }
        else
        {
            isFalling = false;
        }
    }

    void Update()
    {
        
        CheckPause();

        //----------Grounded------------//
        if (isGrounded)
        {
            if (isDagr || isNott)
            {
                folInstance.setParameterByName("FOL", fol = 0);
            }
            
            CheckMovement();
        }

        //----------Not Grounded------------//
        if (!isGrounded)
        {
            if (isDagr || isNott)
            {
                folInstance.setParameterByName("FOL", fol = 1);
            }

            CheckClimbing();
        }

        //-----------Dagr----------//
        if (isDagr)
        {
            onSprite = _pmt.currentSprite;
            isGrounded = _pmt.isGrounded;

            CheckGroundTypes();
        }

        //-----------Nótt----------//
        if (isNott)
        {
            onSprite = _pmt.currentSprite;
            isGrounded = _pmt.isGrounded;

            CheckGroundTypes();
        }

        LevelDone();
    }

    //-----------------------------------------//

    void PlayerStart()
    {
        if (this.gameObject == GameObject.Find("AudioTriggerDagr"))
        {
            _pmt = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerRB = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
            playerTrans = GameObject.Find("Player1").GetComponent<Transform>();
            audioTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
            gtInstance = FMODUnity.RuntimeManager.CreateInstance(footstepsLandings);
            folInstance = FMODUnity.RuntimeManager.CreateInstance(footstepsLandings);
            isDagr = true;
        }

        if (this.gameObject == GameObject.Find("AudioTriggerNott"))
        {
            _pmt = GameObject.Find("Player2").GetComponent<PlayerController>();
            playerRB = GameObject.Find("Player2").GetComponent<Rigidbody2D>();
            playerTrans = GameObject.Find("Player2").GetComponent<Transform>();
            audioTrigger = GameObject.Find("Player2").transform.Find("AudioTriggerNott").gameObject;
            gtInstance = FMODUnity.RuntimeManager.CreateInstance(footstepsLandings);
            folInstance = FMODUnity.RuntimeManager.CreateInstance(footstepsLandings);
            isNott = true;
        }
    }
    
    void CheckMovement()
    {
        //---Movement Checker---//

        isClimbing = false;

        if (isDagr || isNott)
        {
            if (_pmt.movementInputHorizontalDirection > 0.1 || _pmt.movementInputHorizontalDirection < -0.1)
            {
                isMoving = true;
            }
            else if (_pmt.movementInputHorizontalDirection > -0.1 || _pmt.movementInputHorizontalDirection < 0.1)
            {
                isMoving = false;
            }
        }
    }
    
    void CallFootsteps()
    {
        if (isMoving && isGrounded)
        {
            GroundTypes();
        }

        if (isClimbing && isPlant && !isGrounded)
        {
            GroundTypes();
        }
    }
    
    void CheckFalling()
    {
        //----Checks Falling----//

        if (isDagr || isNott)
        {
            oldHeight = height;

            height = playerTrans.position.y;

            heightDiff = height - oldHeight;

            if (heightDiff > 0)
            {
                heightDiff = heightDiff * -1;
            }
        }

        if (onPlant && isClimbing)
        {
            isFalling = false;
        }
    }

    void CheckLanding()
    {
        if (isGrounded && !wasGrounded)
        {
            GroundTypes();
        }
    }

    void CheckClimbing()
    {
        //----Check Climbing----//

        isMoving = false;

        if (isDagr || isNott)
        {
            if (onPlant)
            {
                if (_pmt.movementInputVerticalDirection > 0.1 || _pmt.movementInputVerticalDirection < -0.1)
                {
                    isClimbing = true;
                }
                else if (_pmt.movementInputVerticalDirection > -0.1 || _pmt.movementInputVerticalDirection < 0.1)
                {
                    isClimbing = false;
                }
            }
            else if (!onPlant)
            {
                if (_pmt.movementInputVerticalDirection > 0.1 || _pmt.movementInputVerticalDirection < -0.1)
                {
                    isClimbing = false;
                }
                else if (_pmt.movementInputVerticalDirection > -0.1 || _pmt.movementInputVerticalDirection < 0.1)
                {
                    isClimbing = false;
                }
            }
        }
    }
    
    void GroundTypes()
    {
        if (isDagr || isNott)
        {
            if (isDirt)
            {
                gtInstance.setParameterByName("GT", gt = 0);
                gtInstance.start();
            }
            if (isGrass)
            {
                gtInstance.setParameterByName("GT", gt = 1);
                gtInstance.start();
            }
            if (isIce)
            {
                gtInstance.setParameterByName("GT", gt = 2);
                gtInstance.start();
            }
            if (isPlant)
            {
                gtInstance.setParameterByName("GT", gt = 3);
                gtInstance.start();
            }
            if (isSnow)
            {
                gtInstance.setParameterByName("GT", gt = 4);
                gtInstance.start();
            }
            if (isStone)
            {
                gtInstance.setParameterByName("GT", gt = 5);
                gtInstance.start();
            }
            if (isWater)
            {
                gtInstance.setParameterByName("GT", gt = 6);
                gtInstance.start();
            }
            if (isWood)
            {
                gtInstance.setParameterByName("GT", gt = 7);
                gtInstance.start();
            }
        }
    }
    
    void CheckGroundTypes()
    {
        // Check type of ground
        if (!isGrounded)
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
        if (!onSprite)
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

        for (int i = 0; i < dirtSprites.Length; i++)
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

        for (int i = 0; i < grassSprites.Length; i++)
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

        for (int i = 0; i < iceSprites.Length; i++)
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

        for (int i = 0; i < snowSprites.Length; i++)
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

        for (int i = 0; i < stoneSprites.Length; i++)
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

        for (int i = 0; i < waterSprites.Length; i++)
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

        for (int i = 0; i < woodSprites.Length; i++)
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

        if (onPlant)
        {
            isDirt = false;
            isGrass = false;
            isIce = false;
            isPlant = true;
            isSnow = false;
            isStone = false;
            isWater = false;
            isWood = false;
        }
        else
        {
            isPlant = false;
        }
    }

    void CheckPause()
    {
        if (pause != null)
        {
            if (pause.GetComponent<PauseMenu>().mainPanel.gameObject.activeSelf || pause.GetComponent<PauseMenu>().settingsPanel.gameObject.activeSelf || pause.GetComponent<PauseMenu>().howToPlayPanel.gameObject.activeSelf)
            {
                pause.GetComponent<AudioButtonPressed>().enabled = true;
            }
            else
            {
                pause.GetComponent<AudioButtonPressed>().enabled = false;
            }
        }
    }

    void LevelDone()
    {
        if (door.GetComponent<ExitLevel>().nightEnter && door.GetComponent<ExitLevel>().dayEnter)
        {
            isMoving = false;
            if (isTeleporting == false)
            {
                StartCoroutine(TeleportSFX());
            }
        }
    }

    //-----------------------------------------//

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            onPlant = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            onPlant = false;
        }
    }

    //-----------------------------------------//

    IEnumerator TeleportSFX()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(0.1f);
        FMODUnity.RuntimeManager.PlayOneShot(playerTeleport);
        yield return new WaitForSeconds(0.1f);
        audioTrigger.SetActive(false);
    }
    //-----------------------------------------//
}