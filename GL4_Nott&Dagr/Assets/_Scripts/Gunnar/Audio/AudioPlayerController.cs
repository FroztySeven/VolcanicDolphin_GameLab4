using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    // This script mostly controls the players audio, vocal, movement, skills or anything else players related.
    // It checks the sprites data to see what type of ground the players is standing on, to get the right type of footsteps sounds.
    // It checks the players for if they are grounded, moving and falling and will then play the audio events needed.

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
    //[HideInInspector]
    public Sprite[] dirtSprites, grassSprites, iceSprites, leavesSprites, snowSprites, stoneSprites, waterSprites, woodSprites;

    [SerializeField][Range(0,1)]
    public float walkingSpeed;

    [HideInInspector]
    public float height, oldHeight, heightDiff;

    //[HideInInspector]
    public bool isDagr, isNott, dagrJumps, nottJumps, isGrounded, isClimbing, isFalling, isMoving, isTeleporting, hasLanded, wasGrounded = true, onPlant, isDirt, isGrass, isIce, isLeaves, isPlant, isSnow, isStone, isWater, isWood;

    //--------------------------------------------------------------------------//

    private int gt, fol;

    private FMOD.Studio.EventInstance gtInstance, folInstance, jumpInstance; // This is for FMod event parameters, they are changed depending if players are grounded or not, and on the
                                                                             // types of ground they are standing on. Both players share the same events.

    //--------------------------------------------------------------------------//

    [Header("  Teleport Players SFX  ")]

    [FMODUnity.EventRef]
    public string playerTeleport;

    [Header("  Footsteps and Jump Landings  ")]

    [FMODUnity.EventRef]
    public string footstepsLandings;

    [FMODUnity.EventRef]
    public string jumpGrunt;

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
        CheckLanding(); // Checks what happens when players has landed.
        wasGrounded = isGrounded;

        CheckFalling(); // Checks what happens when players are falling.

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

        //----------Grounded------------// FOL is short for footsteps or landing, fol 0 is footsteps while fol 1 is when the players land on ground. This is for the FMod event parameter.
        if (isGrounded)
        {
            if (isDagr || isNott)
            {
                folInstance.setParameterByName("FOL", fol = 0); 
            }
            
            CheckMovement(); // Checks what happens when players are moving and on the ground.
        }

        //----------Not Grounded------------// If the players are not on ground, fol is set to landing, unless they are climbing up the plants gameobject.
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
            jumpInstance.setParameterByName("PlayerJump", 0);

            CheckGroundTypes(); // Checks what ground types the players are on.

            CheckJumps(); // Checks what happens when the players jump.
        }

        //-----------Nótt----------//
        if (isNott)
        {
            onSprite = _pmt.currentSprite;
            isGrounded = _pmt.isGrounded;
            jumpInstance.setParameterByName("PlayerJump", 1);

            CheckGroundTypes();

            CheckJumps();
        }

        LevelDone(); // Checks what happens when the level is won.

        
        
    }

    //-----------------------------------------//

    void PlayerStart() // This sets up the information needed for each player when game starts, since the two players are the same player gameobject.
    {
        if (this.gameObject == GameObject.Find("AudioTriggerDagr"))
        {
            _pmt = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerRB = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
            playerTrans = GameObject.Find("Player1").GetComponent<Transform>();
            audioTrigger = GameObject.Find("Player1").transform.Find("AudioTriggerDagr").gameObject;
            gtInstance = FMODUnity.RuntimeManager.CreateInstance(footstepsLandings);
            folInstance = FMODUnity.RuntimeManager.CreateInstance(footstepsLandings);
            jumpInstance = FMODUnity.RuntimeManager.CreateInstance(jumpGrunt);
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
            jumpInstance = FMODUnity.RuntimeManager.CreateInstance(jumpGrunt);
            isNott = true;
        }
    }
    
    void CheckMovement() // This checks if the players are moving, through the player controller script. If they are moving, they are not climbing the plant.
    {
        //---Movement Checker---//

        isClimbing = false;

        if (isDagr || isNott)
        {
            /*
            if (_pmt.movementInputHorizontalDirection > 0.1 || _pmt.movementInputHorizontalDirection < -0.1)
            {
                isMoving = true;
            }
            else if (_pmt.movementInputHorizontalDirection > -0.1 || _pmt.movementInputHorizontalDirection < 0.1)
            {
                isMoving = false;
            }*/

            if (_pmt.isWalking == true)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }
    
    void CallFootsteps() // This finds what kind of footsteps sfx is needed, depending on what type of ground the players are on, or when they are climbing the plant gameobject.
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
    
    void CheckFalling() // This checks if the players are falling, unless they are on the plant gameobject.
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

    void CheckLanding() // Finds the correct landing sounds when the players land on the ground depending in the ground type.
    {
        if (isGrounded && !wasGrounded)
        {
            GroundTypes();
        }
    }

    void CheckJumps() // This is used to play jump grunt sounds when the players are jumping.
    {
        if (_pmt.singlePlayer)
        {
            if (isDagr)
            {
                if (Input.GetButtonDown("JumpP" + _pmt.playerId))
                {
                    dagrJumps = true;
                    if (isGrounded)
                    {
                        jumpInstance.start();
                    }
                }
                else
                {
                    dagrJumps = false;
                }
            }

            if (isNott)
            {
                if (Input.GetButtonDown("JumpP" + _pmt.playerId))
                {
                    nottJumps = true;
                    if (isGrounded)
                    {
                        jumpInstance.start();
                    }
                }
                else
                {
                    nottJumps = false;
                }
            }
        }

        if (!_pmt.singlePlayer)
        {
            if (isDagr)
            {
                if (Input.GetButtonDown("JumpP" + _pmt.playerId))
                {
                    dagrJumps = true;
                    if (isGrounded)
                    {
                        jumpInstance.start();
                    }
                }
                else
                {
                    dagrJumps = false;
                }
            }

            if (isNott)
            {
                if (Input.GetButtonDown("JumpP" + _pmt.playerId))
                {
                    nottJumps = true;
                    if (isGrounded)
                    {
                        jumpInstance.start();
                    }
                }
                else
                {
                    nottJumps = false;
                }
            }
        }
    }

    void CheckClimbing() // This checks if the players are climbing on the plant gameobject and play the corresponding footstep sfx for the plant.
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
    
    void GroundTypes() // GT is the parameter for Ground Types parameter in the FMod event. It assigns the parameter value depending on which type of sprite the players are on.
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
            if (isLeaves)
            {
                gtInstance.setParameterByName("GT", gt = 3);
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
    
    void CheckGroundTypes() // This compares the ground types of the level and which sprite the players are on.
    {
        // Check type of ground
        if (!isGrounded) // If they are in the air.
        {
            isDirt = false;
            isGrass = false;
            isIce = false;
            isLeaves = false;
            isPlant = false;
            isSnow = false;
            isStone = false;
            isWater = false;
            isWood = false;
        }
        if (!onSprite) // If they are not on any sprite ground type.
        {
            isDirt = false;
            isGrass = false;
            isIce = false;
            isLeaves = false;
            isPlant = false;
            isSnow = false;
            isStone = false;
            isWater = false;
            isWood = false;
        }

        // This goes through all the sprites assigned to the different ground types.

        for (int i = 0; i < dirtSprites.Length; i++)
        {
            if (onSprite == dirtSprites[i])
            {
                isDirt = true;
                isGrass = false;
                isIce = false;
                isLeaves = false;
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
                isLeaves = false;
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
                isLeaves = false;
                isPlant = false;
                isSnow = false;
                isStone = false;
                isWater = false;
                isWood = false;
            }
        }

        for (int i = 0; i < leavesSprites.Length; i++)
        {
            if (onSprite == leavesSprites[i])
            {
                isDirt = false;
                isGrass = false;
                isIce = false;
                isLeaves = true;
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
                isLeaves = false;
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
                isLeaves = false;
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
                isLeaves = false;
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
                isLeaves = false;
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
            isLeaves = false;
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

    void CheckPause() // This is to enable and disable the sfx when selecting between the buttons in the pause menu, so they should not play when in game.
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

    void LevelDone() // Plays the teleportation sfx when the players have entered the portal.
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

    // This is to check when the players are on the plant gameobjects.

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