using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIcecubeDestroy : MonoBehaviour
{
    // This plays the audio event when the ice cubes are destroyed. There is a problem I have not managed to fix which is when the next scene starts the remaining ice cubes are
    // destroyed and thus play the sound event for it, which is noticeable for the players.

    private FMOD.Studio.EventInstance icecubeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        icecubeDestroy = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/IcecubeDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Joystick2Button6))
        {
            icecubeDestroy.release();
        }
    }
    
    private void OnDestroy()
    {
        icecubeDestroy.start();
        icecubeDestroy.release();
    }
}
