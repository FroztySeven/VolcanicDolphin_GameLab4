using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIcecubeDestroy : MonoBehaviour
{
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
