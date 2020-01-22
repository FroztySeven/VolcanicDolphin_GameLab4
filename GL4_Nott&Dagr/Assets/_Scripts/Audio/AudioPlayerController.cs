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

    public bool isGrounded, isFalling, isGrass;

    //--------------------------------------------------------------------------//

    // Example

    [Header("  Footsteps ")]

    [FMODUnity.EventRef]                //... find the path to the fmod event.
    public string footstepGrass;

    [Header(" Jump Landings ")]

    [FMODUnity.EventRef]
    public string landOnGrass;

    //--------------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // Plays the footsteps FMod events.
    //--------------------------------// 
    void CallFootsteps()
    {
        if (isGrounded == true)
        {
            if (isGrass == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot(footstepGrass);
            }
        }
    }

    // Plays the landing from a jump FMod events.
    //--------------------------------//
    void OnTriggerEnter(Collider other)
    {
        if (isFalling == true)
        {
            if (isGrass)
            {
                FMODUnity.RuntimeManager.PlayOneShot(landOnGrass, GetComponent<Transform>().position);
            }
        }
    }
}


