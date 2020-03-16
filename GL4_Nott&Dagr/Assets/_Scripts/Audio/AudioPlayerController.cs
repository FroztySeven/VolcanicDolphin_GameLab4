using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    //... this script mostly controls the player audio, vocal, movement, skills or anything else player related.
    //... it can use the terrain data to see what type of ground the player is standing on, to get the right type of footsteps sounds.

    //--------------------------------------------------------------------------//

    public Rigidbody2D playerRigidbody;

    public float walkingSpeed;

    public bool isGrounded, isWalking, isClimbing, isFalling, onPlant, isDagr, isNott, isGrass, isSnow, isStone;

    

    //--------------------------------------------------------------------------//

    // Example

    [Header("  Footsteps ")]

    [FMODUnity.EventRef]                //... find the path to the fmod event.
    public string footstepGrass;

    [FMODUnity.EventRef]                
    public string footstepSnow;

    [Header(" Jump Landings ")]

    [FMODUnity.EventRef]
    public string landOnGrass;

    [FMODUnity.EventRef]
    public string landOnSnow;

    //--------------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);

        if (this.gameObject == GameObject.Find("AudioTriggerNott"))
        {
            playerRigidbody = GameObject.Find("Player2").GetComponent<Rigidbody2D>();
            isNott = true;
            isDagr = false;
        }
        else if (this.gameObject == GameObject.Find("AudioTriggerDagr"))
        {
            playerRigidbody = GameObject.Find("Player1").GetComponent<Rigidbody2D>();
            isNott = false;
            isDagr = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if players is falling.
        //--------------------------------// 
        if (playerRigidbody.velocity.y <= -3.5f)
        {
            isFalling = true;
        }
        if (playerRigidbody.velocity.y >= -3.5f)
        {
            isFalling = false;
        }

        if (onPlant == true && isClimbing == true)
        {
            isFalling = false;
        }
        /*
        if (isGrounded == true)
        {
            isFalling = false;
        }
        */
        //--------------------------------//

        if (isFalling == true && isGrounded == true)
        {
            Debug.Log("I Fell");
        }

        // Get confirmation that players are walking and stops from walking in air sounds.
        //--------------------------------//
        if (isGrounded == true && isNott == true)
        {
            if (Input.GetAxisRaw("HorizontalP1") >= 0.01f || Input.GetAxisRaw("HorizontalP1") <= -0.01f)
            {
                isWalking = true;
            }
            else if (Input.GetAxisRaw("HorizontalP1") <= 0.01f || Input.GetAxisRaw("HorizontalP1") >= -0.01f)
            {
                isWalking = false;
            }
        }

        if (onPlant == true && isNott == true)
        {
            if (Input.GetAxisRaw("VerticalP1") >= 0.01f || Input.GetAxisRaw("VerticalP1") <= -0.01f)
            {
                isClimbing = true;
            }
            else if (Input.GetAxisRaw("VerticalP1") <= 0.01f || Input.GetAxisRaw("VerticalP1") >= -0.01f)
            {
                isClimbing = false;
            }
        }

        if (isGrounded == true && isDagr == true)
        {
            if (Input.GetAxisRaw("HorizontalP1") >= 0.01f || Input.GetAxisRaw("HorizontalP1") <= -0.01f)
            {
                isWalking = true;
            }
            else if (Input.GetAxisRaw("HorizontalP1") <= 0.01f || Input.GetAxisRaw("HorizontalP1") >= -0.01f)
            {
                isWalking = false;
            }
        }

        if (onPlant == true && isDagr == true)
        {
            if (Input.GetAxisRaw("VerticalP1") >= 0.01f || Input.GetAxisRaw("VerticalP1") <= -0.01f)
            {
                isClimbing = true;
            }
            else if (Input.GetAxisRaw("VerticalP1") <= 0.01f || Input.GetAxisRaw("VerticalP1") >= -0.01f)
            {
                isClimbing = false;
            }
        }

        if (isGrounded == false && isNott == true)
        {
            if (Input.GetAxis("VerticalP1") >= 0.01f || Input.GetAxis("HorizontalP1") >= 0.01f || Input.GetAxis("VerticalP1") <= -0.01f || Input.GetAxis("HorizontalP1") <= -0.01f)
            {
                isWalking = false;
            }
            else if (Input.GetAxis("VerticalP1") == 0f || Input.GetAxis("HorizontalP1") == 0f)
            {
                isWalking = false;
            }
        }

        if (isGrounded == false && isDagr == true)
        {
            if (Input.GetAxis("VerticalP1") >= 0.01f || Input.GetAxis("HorizontalP1") >= 0.01f || Input.GetAxis("VerticalP1") <= -0.01f || Input.GetAxis("HorizontalP1") <= -0.01f)
            {
                isWalking = false;
            }
            else if (Input.GetAxis("VerticalP1") == 0f || Input.GetAxis("HorizontalP1") == 0f)
            {
                isWalking = false;
            }
        }
        //--------------------------------//
    }

    // Plays the footsteps FMod events.
    //--------------------------------// 
    void CallFootsteps()
    {
        if (isWalking == true)
        {
            if (isGrounded == true)
            {
                if (isGrass == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(footstepGrass);
                }

                if (isStone == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(footstepSnow);
                }
            }
        }
           
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            //Debug.Log("On Plant");
            onPlant = true;
        }

        if (other.gameObject.CompareTag("GroundDetector"))
        {
            //Debug.Log("On Ground");
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("GroundDetector") && other.gameObject.GetComponent<AudioGroundController>().isGrass == true)
        {
            //Debug.Log("On Grass");
            isGrass = true;
        }

        if (other.gameObject.CompareTag("GroundDetector") && other.gameObject.GetComponent<AudioGroundController>().isStone == true)
        {
            //Debug.Log("On Stone");
            isStone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            //Debug.Log("Off Plant");
            onPlant = false;
        }

        if (other.gameObject.CompareTag("GroundDetector"))
        {
            //Debug.Log("Off Ground");
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("GroundDetector") && other.gameObject.GetComponent<AudioGroundController>().isGrass == true)
        {
            //Debug.Log("Off Grass");
            isGrass = false;
        }

        if (other.gameObject.CompareTag("GroundDetector") && other.gameObject.GetComponent<AudioGroundController>().isStone == true)
        {
            //Debug.Log("Off Stone");
            isStone = false;
        }
    }
}


