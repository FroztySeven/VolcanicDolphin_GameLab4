using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
    //... this is to control the volume, it is an in-game mixer of sorts, it was easier to control the volume this way rather than through 
    //... FMod mixer, these can be tweaked while testing the game, make as many sliders as needed to either control a group volume or to control single
    //... bus volume. Group similar audio assets together such as  Player or Environment and place them in an alphabetical order.

    // Main Masters
    private FMOD.Studio.Bus 
        _musicMaster, _sfxMaster;

    // Environment SFX
    private FMOD.Studio.Bus 
        _iceFreezeMaster, _iceMeltMaster, _waterFreezeMaster;

    // Objects
    private FMOD.Studio.Bus 
        _bubbleEnterExitMaster, _gemActiveMaster, _icecubeDestroyedMaster, _portalActivatedMaster, _plantGrowMaster, _stoneNoisesMaster, _trampolineMaster;

    // Players SFX

    // Footsteps & Jump Landings
    private FMOD.Studio.Bus
        _footstepsAndLandingsMaster;

    // Players SFX
    private FMOD.Studio.Bus
        _buttonHighlightMaster, _buttonSelectedMaster;

    //--------------------------------------------------------------------------------------------------------//

    //... these are the volume sliders, the volume goes from -80 to 20(10 is usually used but have more to able to play it really louad)
    //... with 0 being the default value, -80 means the sound is off. They should be linked the apropriate names, master as in the bus 
    //... in FMod, name for what it controls i.e. music, then name it musicMasterVolume.

    [Header(" Master Controllers ")]

    [SerializeField] 
    [Range(-80f, 20f)]
    public float musicMasterVolume, sfxMasterVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header(" Environment SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float iceFreezeVolume, iceMeltVolume, waterFreezeVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header(" Objects SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float bubbleEnterExitVolume, gemActiveVolume, _icecubeDestroyedVolume, _portalActivatedVolume, plantGrowVolume, stoneNoisesVolume, trampolineVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header("       Footsteps SFX Controllers")]

    [Header(" Players SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float footstepsAndLandingsVolume;

    //--------------------------------------------------------------------------------------------------------//

    [Header(" UI SFX Controllers")]

    [SerializeField]
    [Range(-80f, 20f)]
    public float buttonHighlightVolume, buttonSelectedVolume;
    
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
        _gemActiveMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Gems");
        _icecubeDestroyedMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/IcecubeDestroyed");
        _portalActivatedMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/PortalActivated");
        _plantGrowMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/PlantGrowing");
        _stoneNoisesMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/StoneNoises");
        _trampolineMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Objects/Trampoline");


        // Players SFX
        // Footsteps
        _footstepsAndLandingsMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Players/Footsteps & Jump Landings");

        // UI
        _buttonHighlightMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/UI/ButtonHighlight");
        _buttonSelectedMaster = FMODUnity.RuntimeManager.GetBus("bus:/SFX/UI/ButtonSelected");
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
        _gemActiveMaster.setVolume(DecibelToLinear(gemActiveVolume));
        _icecubeDestroyedMaster.setVolume(DecibelToLinear(gemActiveVolume));
        _portalActivatedMaster.setVolume(DecibelToLinear(_portalActivatedVolume));
        _plantGrowMaster.setVolume(DecibelToLinear(plantGrowVolume));
        _stoneNoisesMaster.setVolume(DecibelToLinear(stoneNoisesVolume));
        _trampolineMaster.setVolume(DecibelToLinear(trampolineVolume));

        // Players
        // Footsteps
        _footstepsAndLandingsMaster.setVolume(DecibelToLinear(footstepsAndLandingsVolume));

        // UI
        _buttonHighlightMaster.setVolume(DecibelToLinear(buttonHighlightVolume));
        _buttonSelectedMaster.setVolume(DecibelToLinear(buttonSelectedVolume));
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
