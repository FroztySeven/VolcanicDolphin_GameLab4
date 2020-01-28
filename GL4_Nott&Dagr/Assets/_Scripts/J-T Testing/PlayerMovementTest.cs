using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementTest : MonoBehaviour
{
    public enum NightOrDay { Night, Day }

    public NightOrDay setPlayer;

    [HideInInspector]
    public int playerId;

    public float movementSpeed;
    public float jumpForce;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public bool canWallJump;
    public bool canDoubleJump;

    private int doubleJumpCounter = 0;

    public bool canMove = true;
    private bool isGrounded;
    private bool isOnwall;

    [SerializeField]
    private LayerMask whatIsGround;

    public float groundedRadius;
    public float ceilingRadius;
    public Vector2 wallSize;

    public Transform groundCheck, ceilingCheck, wallCheck;

    private void Awake()
    {
        if (setPlayer.ToString() == "Night")
        {
            if (CharacterStoredInfo.instance)
            {
                playerId = CharacterStoredInfo.instance.night;
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
                playerId = CharacterStoredInfo.instance.day;
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
        isOnwall = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    
        Collider2D[] wallColliders = Physics2D.OverlapBoxAll(wallCheck.position, wallSize, 0f);
        for (int i = 0; i < wallColliders.Length; i++)
        {
            if (wallColliders[i].gameObject != gameObject)
            {
                isOnwall = true;
            }
        }
    }

    private void Update()
    {
        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("HorizontalP" + playerId);

            moveInput.Normalize();

            theRB.velocity = new Vector2(moveInput.x * movementSpeed, theRB.velocity.y);

            if (isGrounded && Input.GetButtonDown("JumpP" + playerId))
            {
                theRB.AddForce(new Vector2(0f, jumpForce));
                Debug.Log("Player" + playerId + " is Jumping!");
                doubleJumpCounter++;
            }

            // Makes the player double jump if enabled.

            if (canDoubleJump && !isGrounded && doubleJumpCounter >= 1 && Input.GetButtonDown("JumpP" + playerId))
            {
                theRB.AddForce(new Vector2(0f, jumpForce));
                Debug.Log("Player" + playerId + " is Double Jumping!");
                doubleJumpCounter = 0;
            }

        }

        // If player is on the wall, slide down.

        if (!isGrounded && isOnwall)
        {
            moveInput.y += -10f;

            // Jump off wall if wall jump is enabled.

            if (canWallJump && Input.GetButtonDown("JumpP" + playerId))
            {
                theRB.AddForce(new Vector2(moveInput.x * jumpForce, jumpForce));
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            SceneManager.LoadScene("Character Selection");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button6))
        {
            SceneManager.LoadScene("Character Selection");
        }
    }
}