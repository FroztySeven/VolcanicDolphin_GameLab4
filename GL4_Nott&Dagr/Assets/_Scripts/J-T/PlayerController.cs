using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public enum NightOrDay { Night, Day }

    public NightOrDay setPlayer;

    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;

    private int facingDirection = 1;
    private int lastWallJumpDirection;
    private int currentlevelindex;

    private bool isFacingRight = true;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    private bool canClimbLedge = false;
    private bool ledgeDetected;

    private Vector2 movementInputHorizontal;
    private Vector2 movementInputVertical;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    private Animator anim;

    [HideInInspector]
    public int playerId = 0;
    [HideInInspector]
    public int amountOfJumpsLeft;
    public int amountOfJumps = 1;

    [HideInInspector]
    public float movementInputHorizontalDirection;
    [HideInInspector]
    public float movementInputVerticalDirection;

    public float movementSpeed = 10f;
    public float jumpForce = 16f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float wallJumpDownwardForce = 1;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;

    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;

    [HideInInspector]
    public bool isOnLadder;
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool isWalking;
    [HideInInspector]
    public bool auraBounce;
    public bool playerCanMove = true;
    public bool playerCanJump = true;
    public bool singlePlayer;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;

    public LayerMask whatIsGround;

    [HideInInspector]
    public Rigidbody2D theRB;

    [HideInInspector]
    public Sprite currentSprite;

    public RuntimeAnimatorController animDay;
    public RuntimeAnimatorController animNight;

    private string animPlayer;

    private void Awake()
    {
        SetCharacterID();
    }

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        playerCanMove = false;
        Invoke("SetPlayerMovement", 0.6f);

        if (setPlayer.ToString() == "Night")
        {
            anim.runtimeAnimatorController = animNight;
            animPlayer = "Night";
        }

        if (setPlayer.ToString() == "Day")
        {
            anim.runtimeAnimatorController = animDay;
            animPlayer = "Day";
        }
    }

    private void SetPlayerMovement()
    {
        playerCanMove = true;
    }

    private void FixedUpdate()
    {
            CheckSurroundings();
        if (playerCanMove)
        {
            ApplyMovement();
        }
    }

    private void Update()
    {
        if (playerCanJump)
        {
            CheckInput();
            CheckIfCanJump();
        }
        if (playerCanMove)
        {
            CheckMovementDirection();
            CheckIfWallSliding();
            CheckJump();
            CheckLedgeClimb();
        }
        UpdateAnimations();

    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking" + animPlayer, isWalking);
        anim.SetBool("isGrounded" + animPlayer, isGrounded);
        anim.SetFloat("yVelocity" + animPlayer, theRB.velocity.y);
        anim.SetBool("isWallSliding" + animPlayer, isWallSliding);
    }

    private void SetCharacterID()
    {
        if (setPlayer.ToString() == "Night")
        {
            if (CharacterStoredInfo.instance)
            {
                if (CharacterStoredInfo.instance.night != 0)
                {
                    playerId = CharacterStoredInfo.instance.night;
                }
                else
                {
                    playerId = 1;
                }
                singlePlayer = CharacterStoredInfo.instance.singlePlayer;
            }
            else
            {
                playerId = 1;
            }
        }

        if (setPlayer.ToString() == "Day")
        {
            if (CharacterStoredInfo.instance)
            {
                if (CharacterStoredInfo.instance.day != 0)
                {
                    playerId = CharacterStoredInfo.instance.day;
                }
                else
                {
                    playerId = 2;
                }
                singlePlayer = CharacterStoredInfo.instance.singlePlayer;
            }
            else
            {
                playerId = 2;
            }
        }
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputHorizontalDirection == facingDirection && theRB.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge" + animPlayer, canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge" + animPlayer, canClimbLedge);
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }

        currentSprite = null;

        // Check what sprite player is standing on.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && theRB.velocity.y <= 4)
            {
                if (colliders[i].GetComponent<Tilemap>())
                {
                    Vector3Int currentCell = colliders[i].GetComponent<Tilemap>().WorldToCell(transform.position);
                    currentCell.y -= 1;

                    currentSprite = colliders[i].GetComponent<Tilemap>().GetSprite(currentCell);
                }

                if (colliders[i].GetComponent<SpriteRenderer>())
                {
                    currentSprite = colliders[i].GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && theRB.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputHorizontalDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputHorizontalDirection > 0)
        {
            Flip();
        }

        if (theRB.velocity.x != 0 && !isTouchingWall && (Input.GetAxis("HorizontalP" + playerId) != 0 || Input.GetAxis("VerticalP" + playerId) != 0))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void CheckInput()
    {
        movementInputHorizontal.x = Input.GetAxis("HorizontalP" + playerId);
        movementInputVertical.y = Input.GetAxis("VerticalP" + playerId);
        movementInputHorizontal.Normalize();
        movementInputVertical.Normalize();

        movementInputHorizontalDirection = movementInputHorizontal.x;
        movementInputVerticalDirection = movementInputVertical.y;

        if (Input.GetButtonDown("JumpP" + playerId))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        //if (Input.GetAxisRaw("HorizontalP" + playerId) != 0 && isTouchingWall)
        //{
        //    if (!isGrounded && movementInputHorizontalDirection != facingDirection)
        //    {
        //        canMove = false;
        //        canFlip = false;

        //        turnTimer = turnTimerSet;
        //    }
        //}

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("JumpP" + playerId))
        {
            checkJumpMultiplier = false;
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * variableJumpHeightMultiplier);
        }

        // Menu and reset buttons and single player swapping.

        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            currentlevelindex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SavedPrefs", currentlevelindex);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button7))
        {
            currentlevelindex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SavedPrefs", currentlevelindex);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button6))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (singlePlayer)
        {
            // Xbox controller button Y toggle
            if (Input.GetButtonDown("SwapP1P2"))
            {
                if (setPlayer.ToString() == "Night")
                {
                    if (playerId == 1)
                    {
                        playerId = 2;
                    }
                    else
                    {
                        playerId = 1;
                    }
                }

                if (setPlayer.ToString() == "Day")
                {
                    if (playerId == 1)
                    {
                        playerId = 2;
                    }
                    else
                    {
                        playerId = 1;
                    }
                }
            }
        }
    }

    private void CheckJump()
    {
        if (jumpTimer > 0)
        {
            // Wall jump
            if (!isGrounded && isTouchingWall && movementInputHorizontalDirection != 0 && movementInputHorizontalDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }

        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputHorizontalDirection == -lastWallJumpDirection)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, -wallJumpDownwardForce);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }

        //else if (isWallSliding && movementInputDirection == 0 && canJump) // Wall hop
        //{
        //    isWallSliding = false;
        //    amountOfJumpsLeft--;
        //    Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
        //    theRB.AddForce(forceToAdd, ForceMode2D.Impulse);
        //}
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if (canWallJump) // Wall jump
        {
            theRB.velocity = new Vector2(theRB.velocity.x, 0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputHorizontalDirection, wallJumpForce * wallJumpDirection.y);
            theRB.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }

    private void ApplyMovement()
    {
        if (auraBounce)
        {
            theRB.velocity = new Vector2(theRB.velocity.x + (movementInputHorizontalDirection * (movementSpeed * 2) * Time.deltaTime), theRB.velocity.y);
        }
        else if (isOnLadder)
        {
            theRB.velocity = new Vector2(movementInputHorizontalDirection * movementSpeed, movementInputVerticalDirection * movementSpeed);
        }
        else if (!isGrounded && !isWallSliding && movementInputHorizontalDirection == 0 && !auraBounce)
        {
            theRB.velocity = new Vector2(theRB.velocity.x * airDragMultiplier, theRB.velocity.y);
        }
        else if (canMove)
        {
            theRB.velocity = new Vector2(movementInputHorizontalDirection * movementSpeed, theRB.velocity.y);
        }

        if (isWallSliding)
        {
            if (theRB.velocity.y < -wallSlideSpeed)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}