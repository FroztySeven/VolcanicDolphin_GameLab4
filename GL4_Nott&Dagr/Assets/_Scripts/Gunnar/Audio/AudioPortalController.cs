using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPortalController : MonoBehaviour
{
    // This plays the portal chime sfx, each time the portal has been set to active it will play the chime event.

    [FMODUnity.EventRef]
    public string portalChime;

    [HideInInspector]
    public GameObject doorPortal;

    [HideInInspector]
    public bool portalActive, playChime;

    private void Awake()
    {
        doorPortal = GameObject.Find("PortalSwirl");
        playChime = true;
    }

    void Start()
    {

    }

    void Update()
    {
        if (doorPortal.activeSelf)
        {
            portalActive = true;
        }

        if (portalActive && playChime)
        {
            FMODUnity.RuntimeManager.PlayOneShot(portalChime);
            playChime = false;
        }
    }
}
