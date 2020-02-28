using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
    //... this is to control the volume, it is an in-game mixer of sorts, it was easier to control the volume this way rather than through 
    //... FMod mixer, these can be tweaked while testing the game, make as many sliders as needed to either control a group volume or to control single
    //... bus volume. Group similar audio assets together such as  Player or Environment and place them in an alphabetical order.

    // Main Masters
    private FMOD.Studio.Bus _musicMaster;
    private FMOD.Studio.Bus _sfxMaster;

    // Environment SFX
    private FMOD.Studio.Bus _iceFreezeMaster;
    private FMOD.Studio.Bus _iceMeltMaster;

    // Objects
    private FMOD.Studio.Bus _bubbleEnterExitMaster;
    private FMOD.Studio.Bus _openDoorMaster;
    private FMOD.Studio.Bus _trampolineMaster;

    // Players SFX
    // Footsteps
    private FMOD.Studio.Bus _grassFsMaster;
    private FMOD.Studio.Bus _snowFsMaster;

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
    public float grassFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float snowFsVolume;


    //--------------------------------------------------------------------------------------------------------//

    private void Start()
    {
        //... these look for the bus channels in the FMod mixer, the pathways need to be exact. 
        //... Pathways should be located in the FMod Bus window, 
        //... right click on the bus you want and copy the path name and add it.

        // Main Masters
        _musicMaster = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        _sfxMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

        // Environment
        _iceFreezeMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Environment/IceFreeze");
        _iceMeltMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Environment/IceMelt");


        // Objects
        _bubbleEnterExitMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Bubbles/BubbleEnterExit");
        _openDoorMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/DoorOpen");
        _trampolineMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Trampoline");


        // Players SFX
        // Footsteps
        _grassFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/GrassFS");
        _snowFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/SnowFS");

    }

    private void Update()
    {
        //... these change the values of the volume sliders.

        // Main Masters
        _musicMaster.setVolume(DecibelToLinear(musicMasterVolume));
        _sfxMaster.setVolume(DecibelToLinear(sfxMasterVolume));

        // Environment
        _iceFreezeMaster.setVolume(DecibelToLinear(iceFreezeVolume));
        _iceMeltMaster.setVolume(DecibelToLinear(iceMeltVolume));

        // Objects
        _bubbleEnterExitMaster.setVolume(DecibelToLinear(bubbleEnterExitVolume));
        _openDoorMaster.setVolume(DecibelToLinear(openDoorVolume));
        _trampolineMaster.setVolume(DecibelToLinear(trampolineVolume));

        // Players
        // Footsteps
        _grassFsMaster.setVolume(DecibelToLinear(grassFsVolume));
        _snowFsMaster.setVolume(DecibelToLinear(snowFsVolume));
    }

    //--------------------------------------------------------------------------------------------------------//

    //... this is so the float value is more like audio dB value.
    private static float DecibelToLinear(float dB) 
    {
        var linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    //--------------------------------------------------------------------------------------------------------//
}
