using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
    //... this is to control the volume, it is an in-game mixer of sorts, it was easier to control the volume this way rather than through 
    //... fmod mixer, these can be tweaked while testing the game, make as many sliders as needed to either control a group volume or to control single
    //... bus volume. Group similar audio assets together such as  Player or Environment and place them in an alphabetical order.

    // Main Masters
    FMOD.Studio.Bus musicMaster;
    FMOD.Studio.Bus sfxMaster;

    // Environment SFX
    FMOD.Studio.Bus iceFreezeMaster;
    FMOD.Studio.Bus iceMeltMaster;

    // Objects
    FMOD.Studio.Bus bubbleEnterExitMaster;
    FMOD.Studio.Bus openDoorMaster;
    FMOD.Studio.Bus trampolineMaster;

    // Players SFX
    // Footsteps
    FMOD.Studio.Bus grassFSMaster;
    FMOD.Studio.Bus snowFSMaster;

    //--------------------------------------------------------------------------------------------------------//

    //... these are the volume sliders, the volume goes from -80 to 20(10 is usually used but have more to able to play it really louad)
    //... with 0 being the default value, -80 means the sound is off. They should be linked the apropriate names, master as in the bus 
    //... in FMod, name for what it controls i.e. music, then name it musicMasterVolume.

    [Header(" Master Controllers ")]

    [SerializeField] [Range(-80f, 20f)]
    public float musicMasterVolume;

    [SerializeField] [Range(-80f, 20f)]
    public float sfxMasterVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header(" Environment SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float iceFreezeVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float iceMeltVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header(" Objects SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float bubbleEnterExitVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float openDoorVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float trampolineVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header("       Footsteps SFX Controllers")]

    [Header(" Players SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float grassFSVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float snowFSVolume;


    //--------------------------------------------------------------------------------------------------------//

    void Start()
    {
        //... these look for the bus channels in the fmod mixer, the pathways need to be exact. 
        //... Pathways should be located in the FMod Bus window, 
        //... right click on the bus you want and copy the path name and add it.

        // Main Masters
        musicMaster = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

        // Environment
        iceFreezeMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Environment/IceFreeze");
        iceMeltMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Environment/IceMelt");


        // Objects
        bubbleEnterExitMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Bubbles/BubbleEnterExit");
        openDoorMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/DoorOpen");
        trampolineMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Trampoline");


        // Players SFX
        // Footsteps
        grassFSMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/GrassFS");
        snowFSMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/SnowFS");

    }

    void Update()
    {
        //... these change the values of the volume sliders.

        // Main Masters
        musicMaster.setVolume(DecibelToLinear(musicMasterVolume));
        sfxMaster.setVolume(DecibelToLinear(sfxMasterVolume));

        // Environment
        iceFreezeMaster.setVolume(DecibelToLinear(iceFreezeVolume));
        iceMeltMaster.setVolume(DecibelToLinear(iceMeltVolume));

        // Objects
        bubbleEnterExitMaster.setVolume(DecibelToLinear(bubbleEnterExitVolume));
        openDoorMaster.setVolume(DecibelToLinear(openDoorVolume));
        trampolineMaster.setVolume(DecibelToLinear(trampolineVolume));

        // Players
        // Footsteps
        grassFSMaster.setVolume(DecibelToLinear(grassFSVolume));
        snowFSMaster.setVolume(DecibelToLinear(snowFSVolume));
    }

    //--------------------------------------------------------------------------------------------------------//

    //... this is so the float value is more like audio dB value.
    private float DecibelToLinear(float dB) 
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    //--------------------------------------------------------------------------------------------------------//
}
