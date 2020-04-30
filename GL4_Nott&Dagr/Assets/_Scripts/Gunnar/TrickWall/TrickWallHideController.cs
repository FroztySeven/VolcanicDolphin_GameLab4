using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using FMODUnity;
using UnityEngine;

public class TrickWallHideController : MonoBehaviour
{
    //... Notice! This script was originally made by Jan-Tore.
    //... Original scripts used were PressurePlates and PressurePlatesWall. 
    //... I (Gunnar), made a copy those scripts to make alterations to them, add more variants of wall behaviours that I wanted.

    public enum WhoCanUse { Night, Day, Both }

    public WhoCanUse setUser;

    public enum HideMoveMethod { HideAllOnStart, ShowAllOnStart, SetByWall, SetByWallLockPermanent, SetByWallLockUnlockOnEnter, SetByWallLockTimer, MoveAllWalls, MoveLockPermanent, MoveLockUnlockOnEnter, MoveLockOnTimer}

    public HideMoveMethod hideMoveMethod;

    public GameObject[] walls, multiPlates;

    public GameObject button;

    [HideInInspector]
    public Sprite pressed, unPressed, pressDagr, unpressDagr, pressNott, unpressNott, pressBoth, unpressBoth;

    public StudioEventEmitter playEnter, playExit, playTimerNormal, playTimerFast, playBong;

    public float timer;
    
    public bool hasMoved, hasHidden;

    //--- Private ---//

    public int playEventCount = 0, lastPressed = 0;
    
    private float currCountdownValue;

    private FMOD.Studio.EventInstance onButton;
    private void Start()
    {
        onButton = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Objects/PressurePlate");

        if (setUser == WhoCanUse.Day)
        {
            unPressed = unpressDagr;
            pressed = pressDagr;

            button.GetComponent<SpriteRenderer>().sprite = unPressed;

        }
        else if (setUser == WhoCanUse.Night)
        {
            unPressed = unpressNott;
            pressed = pressNott;

            button.GetComponent<SpriteRenderer>().sprite = unPressed;
        }
        else if (setUser == WhoCanUse.Both)
        {
            unPressed = unpressBoth;
            pressed = pressBoth;

            button.GetComponent<SpriteRenderer>().sprite = unPressed;
        }

        if (hideMoveMethod.ToString() == "HideAllOnStart")
        {
            HideWall();
        }

        if (hideMoveMethod.ToString() == "ShowAllOnStart")
        {
            ShowWall();
        }
        
        if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<TrickWallWallController>().showMe == false)
                    {
                        walls.SetActive(true);
                    }
                    else
                    {
                        walls.SetActive(false);
                    }
                }
            }
        }

        if (hideMoveMethod.ToString() == "SetByWallLockPermanent")
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<TrickWallWallController>().showMe == false)
                    {
                        walls.SetActive(false);
                    }
                    else
                    {
                        walls.SetActive(true);
                    }
                }
            }
        }

        if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    playEnter.PlayEvent = EmitterGameEvent.None;
                    playExit.PlayEvent = EmitterGameEvent.None;

                    if (walls.GetComponent<TrickWallWallController>().showMe == false)
                    {
                        walls.SetActive(true);
                    }
                    else
                    {
                        walls.SetActive(false);
                    }
                }
            }
        }

        if (hideMoveMethod.ToString() == "SetByWallLockTimer")
        {
            //button.transform.position = startPos;

            playEnter.PlayEvent = EmitterGameEvent.None;
            playExit.PlayEvent = EmitterGameEvent.None;

            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<TrickWallWallController>().showMe == false)
                    {
                        walls.SetActive(true);
                    }
                    else
                    {
                        walls.SetActive(false);
                    }
                }
            }
        }

        if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
        {
            playEnter.PlayEvent = EmitterGameEvent.None;
            playExit.PlayEvent = EmitterGameEvent.None;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == setUser.ToString() || setUser.ToString() == "Both")
            {
                button.GetComponent<SpriteRenderer>().sprite = pressed;

                if (hideMoveMethod.ToString() == "HideAllOnStart")
                {
                    ShowWall();
                }

                if (hideMoveMethod.ToString() == "ShowAllOnStart")
                {
                    HideWall();
                }

                if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().showMe == false)
                            {
                                walls.SetActive(false);
                            }
                            else
                            {
                                walls.SetActive(true);
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockPermanent")
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().showMe == false)
                            {
                                walls.SetActive(true);
                            }
                            else
                            {
                                walls.SetActive(false);
                            }

                            if (playEventCount == 0)
                            {
                                playEnter.PlayEvent = EmitterGameEvent.TriggerEnter2D;
                                playEventCount = 1;
                            }
                            else if (playEventCount == 1)
                            {
                                playEnter.PlayEvent = EmitterGameEvent.None;
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
                {
                    playEnter.Play();

                    if (lastPressed != 1)
                    {
                        lastPressed = 1;

                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                if (walls.GetComponent<TrickWallWallController>().showMe == false)
                                {
                                    walls.SetActive(false);
                                }
                                else
                                {
                                    walls.SetActive(true);
                                }
                            }
                        }
                    }
                    else
                    {
                        lastPressed = 0;
                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                if (walls.GetComponent<TrickWallWallController>().showMe == false)
                                {
                                    walls.SetActive(true);
                                }
                                else
                                {
                                    walls.SetActive(false);
                                }
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockTimer")
                {
                    button.GetComponent<SpriteRenderer>().sprite = pressed;

                    if (hasHidden == false)
                    {
                        StartCoroutine(HideWallTimer());
                    }

                }

                if (hideMoveMethod.ToString() == "MoveAllWalls")
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<TrickWallMoveController>().move = true;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockPermanent") 
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<TrickWallMoveController>().move = true;
                            
                            if (playEventCount == 0)
                            {
                                playEnter.PlayEvent = EmitterGameEvent.TriggerEnter2D;
                                playEventCount = 1;
                            }
                            else if (playEventCount == 1)
                            {
                                playEnter.PlayEvent = EmitterGameEvent.None;
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
                {
                    playEnter.Play();

                    if (lastPressed != 1)
                    {
                        foreach (var plate in multiPlates)
                        {
                            plate.GetComponent<TrickWallHideController>().lastPressed = 1;
                        }
                        
                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                walls.GetComponent<TrickWallMoveController>().move = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (var plate in multiPlates)
                        {
                            plate.GetComponent<TrickWallHideController>().lastPressed = 0;
                        }

                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                walls.GetComponent<TrickWallMoveController>().move = false;
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockOnTimer")
                {
                    button.GetComponent<SpriteRenderer>().sprite = pressed;

                    if (hasMoved == false)
                    {
                        StartCoroutine(MoveWallTimer());
                    }

                    playEnter.PlayEvent = EmitterGameEvent.None;
                    playExit.PlayEvent = EmitterGameEvent.None;
                }
            }
        }

        if (other.tag == "Minion")
        {
            button.GetComponent<SpriteRenderer>().sprite = pressed;

            if (hideMoveMethod.ToString() == "HideAllOnStart")
            {
                ShowWall();
            }

            if (hideMoveMethod.ToString() == "ShowAllOnStart")
            {
                HideWall();
            }

            if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        if (walls.GetComponent<TrickWallWallController>().showMe == false)
                        {
                            walls.SetActive(false);
                        }
                        else
                        {
                            walls.SetActive(true);
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "SetByWallLockPermanent")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        if (walls.GetComponent<TrickWallWallController>().showMe == false)
                        {
                            walls.SetActive(true);
                        }
                        else
                        {
                            walls.SetActive(false);
                        }

                        if (playEventCount == 0)
                        {
                            playEnter.PlayEvent = EmitterGameEvent.TriggerEnter2D;
                            playEventCount = 1;
                        }
                        else if (playEventCount == 1)
                        {
                            playEnter.PlayEvent = EmitterGameEvent.None;
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
            {
                playEnter.Play();

                if (lastPressed != 1)
                {
                    lastPressed = 1;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().showMe == false)
                            {
                                walls.SetActive(false);
                            }
                            else
                            {
                                walls.SetActive(true);
                            }
                        }
                    }
                }
                else
                {
                    lastPressed = 0;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().showMe == false)
                            {
                                walls.SetActive(true);
                            }
                            else
                            {
                                walls.SetActive(false);
                            }
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "SetByWallLockTimer")
            {
                button.GetComponent<SpriteRenderer>().sprite = pressed;

                if (hasHidden == false)
                {
                    StartCoroutine(HideWallTimer());
                }

            }

            if (hideMoveMethod.ToString() == "MoveAllWalls")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        walls.GetComponent<TrickWallMoveController>().move = true;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockPermanent")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        walls.GetComponent<TrickWallMoveController>().move = true;

                        if (playEventCount == 0)
                        {
                            playEnter.PlayEvent = EmitterGameEvent.TriggerEnter2D;
                            playEventCount = 1;
                        }
                        else if (playEventCount == 1)
                        {
                            playEnter.PlayEvent = EmitterGameEvent.None;
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
            {
                playEnter.Play();

                if (lastPressed != 1)
                {
                    lastPressed = 1;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<TrickWallMoveController>().move = true;
                        }
                    }
                }
                else
                {
                    lastPressed = 0;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<TrickWallMoveController>().move = false;
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockOnTimer")
            {
                button.GetComponent<SpriteRenderer>().sprite = pressed;

                if (hasMoved == false)
                {
                    StartCoroutine(MoveWallTimer());
                }

                playEnter.PlayEvent = EmitterGameEvent.None;
                playExit.PlayEvent = EmitterGameEvent.None;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == setUser.ToString() || setUser.ToString() == "Both")
            {

                if (hideMoveMethod.ToString() == "HideAllOnStart")
                {
                    button.GetComponent<SpriteRenderer>().sprite = unPressed;
                    HideWall();
                }

                if (hideMoveMethod.ToString() == "ShowAllOnStart")
                {
                    button.GetComponent<SpriteRenderer>().sprite = unPressed;
                    ShowWall();
                }

                if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
                {
                    button.GetComponent<SpriteRenderer>().sprite = unPressed;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().showMe == false)
                            {
                                walls.SetActive(true);
                            }
                            else
                            {
                                walls.SetActive(false);
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockPermanent")
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            button.GetComponent<SpriteRenderer>().sprite = pressed;
                            playExit.PlayEvent = EmitterGameEvent.None;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
                {
                    button.GetComponent<SpriteRenderer>().sprite = unPressed;
                    //playExit.Play();
                }

                if (hideMoveMethod.ToString() == "MoveAllWalls")
                {
                    button.GetComponent<SpriteRenderer>().sprite = unPressed;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<TrickWallMoveController>().move = false;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockPermanent")
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            button.GetComponent<SpriteRenderer>().sprite = pressed;
                            playExit.PlayEvent = EmitterGameEvent.None;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
                {
                    button.GetComponent<SpriteRenderer>().sprite = unPressed;
                    //playExit.Play();
                }
            }
        }

        if (other.tag == "Minion")
        {
            if (hideMoveMethod.ToString() == "HideAllOnStart")
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                HideWall();
            }

            if (hideMoveMethod.ToString() == "ShowAllOnStart")
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                ShowWall();
            }

            if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        if (walls.GetComponent<TrickWallWallController>().showMe == false)
                        {
                            walls.SetActive(true);
                        }
                        else
                        {
                            walls.SetActive(false);
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "SetByWallLockPermanent")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        button.GetComponent<SpriteRenderer>().sprite = pressed;
                        playExit.PlayEvent = EmitterGameEvent.None;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                //playExit.Play();
            }

            if (hideMoveMethod.ToString() == "MoveAllWalls")
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        walls.GetComponent<TrickWallMoveController>().move = false;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockPermanent")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        button.GetComponent<SpriteRenderer>().sprite = pressed;
                        playExit.PlayEvent = EmitterGameEvent.None;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                //playExit.Play();
            }
        }
    }

    void HideWall()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            foreach (GameObject walls in walls)
            {
                walls.SetActive(false);
            }
        }
    }

    void ShowWall()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            foreach (GameObject walls in walls)
            {
                walls.SetActive(true);
            }
        }
    }

    IEnumerator MoveWallTimer()
    {
        hasMoved = true;

        playEnter.Play();
        playTimerNormal.Play();

        for (int i = 0; i < walls.Length; i++)
        {
            foreach (GameObject walls in walls)
            {
                walls.GetComponent<TrickWallMoveController>().move = true;
            }
        }

        currCountdownValue = timer;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;

            if (currCountdownValue <= 3.1)
            {
                playTimerNormal.Stop();
            }

            if (currCountdownValue <= 3)
            {
                playTimerFast.Play();
            }
        }

        if (currCountdownValue == 0)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    walls.GetComponent<TrickWallMoveController>().move = false;
                }
            }

            playTimerFast.Stop();
            playBong.Play();
            playExit.Play();

            button.GetComponent<SpriteRenderer>().sprite = unPressed;

            yield return new WaitForSeconds(0.5f);

            hasMoved = false;
        }
    }

    IEnumerator HideWallTimer()
    {
        hasHidden = true;

        playEnter.Play();
        playTimerNormal.Play();

        for (int i = 0; i < walls.Length; i++)
        {
            foreach (GameObject walls in walls)
            {
                if (walls.GetComponent<TrickWallWallController>().showMe == false)
                {
                    walls.SetActive(false);
                }
                else
                {
                    walls.SetActive(true);
                }
            }
        }

        currCountdownValue = timer;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;

            if (currCountdownValue <= 3.1)
            {
                playTimerNormal.Stop();
            }

            if (currCountdownValue <= 3)
            {
                playTimerFast.Play();
            }
        }
        

        if (currCountdownValue == 0)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<TrickWallWallController>().showMe == false)
                    {
                        walls.SetActive(true);
                    }
                    else
                    {
                        walls.SetActive(false);
                    }
                }
            }
            
            playTimerFast.Stop();
            playBong.Play();
            playExit.Play();

            button.GetComponent<SpriteRenderer>().sprite = unPressed;

            yield return new WaitForSeconds(0.5f);

            hasHidden = false;
        }
    }
}