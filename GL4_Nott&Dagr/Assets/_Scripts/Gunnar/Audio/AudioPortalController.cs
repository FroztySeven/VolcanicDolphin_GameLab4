using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPortalController : MonoBehaviour
{
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
