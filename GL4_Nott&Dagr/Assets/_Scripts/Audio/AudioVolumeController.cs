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
    private FMOD.Studio.Bus _waterFreezeMaster;

    // Objects
    private FMOD.Studio.Bus _bubbleEnterExitMaster;
    private FMOD.Studio.Bus _openDoorMaster;
    private FMOD.Studio.Bus _plantGrowMaster;
    private FMOD.Studio.Bus _stoneNoisesMaster;
    private FMOD.Studio.Bus _trampolineMaster;
    private FMOD.Studio.Bus _gemActivatedMaster;

    // Players SFX
    // Footsteps
    private FMOD.Studio.Bus _dirtFsMaster;
    private FMOD.Studio.Bus _grassFsMaster;
    private FMOD.Studio.Bus _iceFsMaster;
    private FMOD.Studio.Bus _plantFsMaster;
    private FMOD.Studio.Bus _snowFsMaster;
    private FMOD.Studio.Bus _stoneFsMaster;
    private FMOD.Studio.Bus _waterFsMaster;
    private FMOD.Studio.Bus _woodFsMaster;

    // Jump Landings
    private FMOD.Studio.Bus _dirtJlMaster;
    private FMOD.Studio.Bus _grassJlMaster;
    private FMOD.Studio.Bus _iceJlMaster;
    private FMOD.Studio.Bus _plantJlMaster;
    private FMOD.Studio.Bus _snowJlMaster;
    private FMOD.Studio.Bus _stoneJlMaster;
    private FMOD.Studio.Bus _waterJlMaster;
    private FMOD.Studio.Bus _woodJlMaster;

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

    [SerializeField]
    [Range(-80f, 20f)]
    public float waterFreezeVolume;

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
    public float plantGrowVolume;
    
    [SerializeField]
    [Range(-80f, 20f)]
    public float stoneNoisesVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float trampolineVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float gemActiveVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header("       Footsteps SFX Controllers")]

    [Header(" Players SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float dirtFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float grassFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float iceFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float plantFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float snowFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float stoneFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float waterFsVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float woodFsVolume;

    [Header("       Jump Landings SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float dirtJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float grassJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float iceJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float plantJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float snowJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float stoneJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float waterJlVolume;

    [SerializeField]
    [Range(-80f, 20f)]
    public float woodJlVolume;
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
        _waterFreezeMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Environment/WaterFreeze");


        // Objects
        _bubbleEnterExitMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Bubbles/BubbleEnterExit");
        _openDoorMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/DoorOpen");
        _plantGrowMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/PlantGrowing");
        _stoneNoisesMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/StoneNoises");
        _trampolineMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Trampoline");
        _gemActivatedMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Gems");


        // Players SFX
        // Footsteps
        _dirtFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/DirtFS");
        _grassFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/GrassFS");
        _iceFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/IceFS");
        _plantFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/PlantFS");
        _snowFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/SnowFS");
        _stoneFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/StoneFS");
        _waterFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/WaterFS");
        _woodFsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps/WoodFS");

        // Jump Landings
        _dirtJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/DirtJL");
        _grassJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/GrassJL");
        _iceJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/IceJL");
        _plantJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/PlantJL");
        _snowJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/SnowJL");
        _stoneJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/StoneJL");
        _waterJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/WaterJL");
        _woodJlMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Landings/WoodJL");

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
        _waterFreezeMaster.setVolume(DecibelToLinear(waterFreezeVolume));

        // Objects
        _bubbleEnterExitMaster.setVolume(DecibelToLinear(bubbleEnterExitVolume));
        _openDoorMaster.setVolume(DecibelToLinear(openDoorVolume));
        _plantGrowMaster.setVolume(DecibelToLinear(plantGrowVolume));
        _stoneNoisesMaster.setVolume(DecibelToLinear(stoneNoisesVolume));
        _trampolineMaster.setVolume(DecibelToLinear(trampolineVolume));
        _gemActivatedMaster.setVolume(DecibelToLinear(gemActiveVolume));

        // Players
        // Footsteps
        _dirtFsMaster.setVolume(DecibelToLinear(dirtFsVolume));
        _grassFsMaster.setVolume(DecibelToLinear(grassFsVolume));
        _iceFsMaster.setVolume(DecibelToLinear(iceFsVolume));
        _plantFsMaster.setVolume(DecibelToLinear(plantFsVolume));
        _snowFsMaster.setVolume(DecibelToLinear(snowFsVolume));
        _stoneFsMaster.setVolume(DecibelToLinear(stoneFsVolume));
        _waterFsMaster.setVolume(DecibelToLinear(waterFsVolume));
        _woodFsMaster.setVolume(DecibelToLinear(woodFsVolume));

        // Jump Landings
        _dirtJlMaster.setVolume(DecibelToLinear(dirtJlVolume));
        _grassJlMaster.setVolume(DecibelToLinear(grassJlVolume));
        _iceJlMaster.setVolume(DecibelToLinear(iceJlVolume));
        _plantJlMaster.setVolume(DecibelToLinear(plantJlVolume));
        _snowJlMaster.setVolume(DecibelToLinear(snowJlVolume));
        _stoneJlMaster.setVolume(DecibelToLinear(stoneJlVolume));
        _waterJlMaster.setVolume(DecibelToLinear(waterJlVolume));
        _woodJlMaster.setVolume(DecibelToLinear(woodJlVolume));
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
