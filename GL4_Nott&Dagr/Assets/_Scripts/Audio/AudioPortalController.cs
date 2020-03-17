using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPortalController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string portalChime;

    public GameObject doorPortal;

    public bool portalActive, playChime;

    // Start is called before the first frame update

    private void Awake()
    {
        doorPortal = GameObject.Find("PortalSwirl");
    }

    void Start()
    {
        playChime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorPortal.activeSelf == true)
        {
            portalActive = true;
        }

        if (portalActive == true && playChime == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(portalChime);
            playChime = false;
        }
    }
}
