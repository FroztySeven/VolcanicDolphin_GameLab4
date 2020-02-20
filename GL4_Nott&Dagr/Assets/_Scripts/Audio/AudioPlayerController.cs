using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    //... this script mostly controls the player audio, vocal, movement, skills or anything else player related.
    //... it can use the terrain data to see what type of ground the player is standing on, to get the right type of footsteps sounds.

    //--------------------------------------------------------------------------//

    public Rigidbody playerRigidbody;

    public float walkingSpeed;

    public bool isGrounded, isWalking, isFalling, isGrass, isSnow;

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

        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if player is falling.
        //--------------------------------// 
        if (playerRigidbody.velocity.y <= -3.5f)
        {
            isFalling = true;
        }
        if (isGrounded == true)
        {
            isFalling = false;
        }
        //--------------------------------//


        // Get confirmation that players are walking and stops from walking in air sounds.
        //--------------------------------//
        if (isGrounded == true & playerRigidbody == true)
        {
            if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
            {
                isWalking = true;
            }
            else if (Input.GetAxis("Vertical") == 0f || Input.GetAxis("Horizontal") == 0f)
            {
                isWalking = false;
            }
        }

        if (isGrounded == false & playerRigidbody == true)
        {
            if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
            {
                isWalking = false;
            }
            else if (Input.GetAxis("Vertical") == 0f || Input.GetAxis("Horizontal") == 0f)
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
                if (isGrass == true & GameObject.Find("Player1") == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(footstepGrass);
                }

                if (isSnow == true & GameObject.Find("Player2") == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(footstepSnow);
                }
            }
        }
           
    }

    // Plays the landing from a jump FMod events.
    //--------------------------------//
    void OnTriggerEnter(Collider other)
    {
        if (isGrass == true & GameObject.Find("Player1") == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(landOnGrass, GetComponent<Transform>().position);
        }

        if (isSnow == true & GameObject.Find("Player2") == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(landOnSnow, GetComponent<Transform>().position);
        }
    }
}


