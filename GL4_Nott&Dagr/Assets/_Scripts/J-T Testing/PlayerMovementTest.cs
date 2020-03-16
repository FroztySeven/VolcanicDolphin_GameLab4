using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovementTest : MonoBehaviour
{
    public enum NightOrDay { Night, Day }

    public NightOrDay setPlayer;

    [HideInInspector]
    public int playerId = 0;

    public bool singlePlayer;
    private bool swapInputs;

    public float movementSpeed;
    public float jumpForce;
    public Vector2 moveInput;

    public Rigidbody2D theRB;

    public bool canWallJump;
    public bool canDoubleJump;

    [HideInInspector]
    public int doubleJumpCounter = 0;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool isGrounded;
    private bool isOnWall;

    [SerializeField]
    private LayerMask whatIsGround;

    public float groundedRadius;
    public float ceilingRadius;
    public Vector2 wallSize;

    [HideInInspector]
    public Sprite currentSprite;

    //[HideInInspector]
    public bool isOnLadder;

    public bool auraBounce;


    public Transform groundCheck, ceilingCheck, wallCheck;

    private void Awake()
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

    private void FixedUpdate()
    {
        isGrounded = false;
        currentSprite = null;
        isOnWall = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                Vector3Int currentCell = colliders[i].GetComponent<Tilemap>().WorldToCell(transform.position);
                currentCell.y -= 1;

                currentSprite = colliders[i].GetComponent<Tilemap>().GetSprite(currentCell);
            }
        }

        //Collider2D[] wallColliders = Physics2D.OverlapBoxAll(wallCheck.position, wallSize, 0f);
        //for (int i = 0; i < wallColliders.Length; i++)
        //{
        //    if (wallColliders[i].gameObject != gameObject)
        //    {
        //        isOnWall = true;
        //    }
        //}

        Collider2D[] wallColliders = Physics2D.OverlapCircleAll(wallCheck.position, 0.2f, whatIsGround);
        for (int i = 0; i < wallColliders.Length; i++)
        {
            if (wallColliders[i].gameObject != gameObject)
            {
                isOnWall = true;
            }
        }
    }

    private int currentlevelindex;
    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("HorizontalP" + playerId);
        moveInput.Normalize();

        if (isGrounded && Input.GetButtonDown("JumpP" + playerId))
        {
            theRB.AddForce(new Vector2(0f, jumpForce));
            //Debug.Log("Player" + playerId + " is Jumping!");
            doubleJumpCounter++;
        }

        if (canMove)
        {
            if (auraBounce)
            {
                theRB.velocity = new Vector2(theRB.velocity.x + (moveInput.x * movementSpeed * 2 * Time.deltaTime), theRB.velocity.y);
            }
            else
            {
                theRB.velocity = new Vector2(moveInput.x * movementSpeed, theRB.velocity.y);
            }

            // Movement while in air.

            //if (!isGrounded)
            //{
            //    theRB.velocity = new Vector2((moveInput.x * movementSpeed) / 1.5f, theRB.velocity.y);
            //}

            // Makes the player double jump if enabled.

            if (canDoubleJump && !isGrounded && doubleJumpCounter >= 1 && Input.GetButtonDown("JumpP" + playerId))
            {
                theRB.velocity = Vector2.zero;
                theRB.AddForce(new Vector2(0f, jumpForce));
                //Debug.Log("Player" + playerId + " is Double Jumping!");
                doubleJumpCounter = 0;
            }

            if (isOnLadder)
            {
                moveInput.y = Input.GetAxisRaw("VerticalP" + playerId);
                moveInput.Normalize();
                theRB.velocity = new Vector2(moveInput.x * movementSpeed, moveInput.y * movementSpeed);
            }
            else
            {
                moveInput.y = 0;
            }
        }

        // If player is on the wall, slide down.

        if (!isGrounded && isOnWall)
        {
            moveInput.y += -10f;

            // Jump off wall if wall jump is enabled.

            if (canWallJump && Input.GetButtonDown("JumpP" + playerId))
            {
                theRB.AddForce(new Vector2(moveInput.x * jumpForce, jumpForce));
            }
        }

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
}