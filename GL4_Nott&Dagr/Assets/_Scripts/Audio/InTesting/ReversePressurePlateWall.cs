using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using FMODUnity;
using UnityEngine;

public class ReversePressurePlateWall : MonoBehaviour
{
    //... Notice! This script was originally made by Jan-Tore.
    //... Original scripts used were PressurePlates and PressurePlatesWall. 
    //... I (Gunnar), made a copy those scripts to make alterations to them, add more variants of wall behaviours that I wanted.

    public enum WhoCanUse { Night, Day, Both }

    public WhoCanUse setUser;

    public enum HideMoveMethod { HideAllOnStart, ShowAllOnStart, SetByWall, SetByWallLockPermanent, SetByWallLockUnlockOnEnter, SetByWallLockTimer, MoveAllWalls, MoveLockPermanent, MoveLockUnlockOnEnter, MoveLockOnTimer}

    public HideMoveMethod hideMoveMethod;

    public GameObject[] walls;
    public GameObject button;

    public StudioEventEmitter playEnter, playExit, playTimerNormal, playTimerFast, playBong;

    private Vector3 startPos;
    private Vector3 pressedPos;

    private int playEventCount = 0;
    private int lastPressed = 0;

    public float timer;
    private float currCountdownValue;
    

    public bool hasMoved, hasHidden;

    private void Start()
    {
        if (setUser == WhoCanUse.Day)
        {
            button.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (setUser == WhoCanUse.Night)
        {
            button.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else if (setUser == WhoCanUse.Both)
        {
            button.GetComponent<SpriteRenderer>().color = Color.green;
        }

        startPos = button.transform.position;
        pressedPos = startPos - new Vector3(0f, 0.08f, 0f);

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
                    if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                    if (walls.GetComponent<ReverseWallController>().showMe == false)
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

                    if (walls.GetComponent<ReverseWallController>().showMe == false)
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
            button.transform.position = startPos;

            playEnter.PlayEvent = EmitterGameEvent.None;
            playExit.PlayEvent = EmitterGameEvent.None;

            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                button.transform.position = pressedPos;
                
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
                            if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                            if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                                if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                                if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                    button.transform.position = pressedPos;

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
                            walls.GetComponent<ReversePressurePlateMover>().move = true;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockPermanent") 
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<ReversePressurePlateMover>().move = true;
                            
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
                                walls.GetComponent<ReversePressurePlateMover>().move = true;
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
                                walls.GetComponent<ReversePressurePlateMover>().move = false;
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockOnTimer")
                {
                    button.transform.position = pressedPos;

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
            button.transform.position = pressedPos;

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
                        if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                        if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                            if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                            if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                button.transform.position = pressedPos;

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
                        walls.GetComponent<ReversePressurePlateMover>().move = true;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockPermanent")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        walls.GetComponent<ReversePressurePlateMover>().move = true;

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
                            walls.GetComponent<ReversePressurePlateMover>().move = true;
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
                            walls.GetComponent<ReversePressurePlateMover>().move = false;
                        }
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockOnTimer")
            {
                button.transform.position = pressedPos;

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
                    button.transform.position = startPos;
                    HideWall();
                }

                if (hideMoveMethod.ToString() == "ShowAllOnStart")
                {
                    button.transform.position = startPos;
                    ShowWall();
                }

                if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
                {
                    button.transform.position = startPos;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                            button.transform.position = pressedPos;
                            playExit.PlayEvent = EmitterGameEvent.None;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
                {
                    button.transform.position = startPos;
                    //playExit.Play();
                }

                if (hideMoveMethod.ToString() == "MoveAllWalls")
                {
                    button.transform.position = startPos;
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<ReversePressurePlateMover>().move = false;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockPermanent")
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            button.transform.position = pressedPos;
                            playExit.PlayEvent = EmitterGameEvent.None;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
                {
                    button.transform.position = startPos;
                    //playExit.Play();
                }

            }
        }

        if (other.tag == "Minion")
        {
            if (hideMoveMethod.ToString() == "HideAllOnStart")
            {
                button.transform.position = startPos;
                HideWall();
            }

            if (hideMoveMethod.ToString() == "ShowAllOnStart")
            {
                button.transform.position = startPos;
                ShowWall();
            }

            if (hideMoveMethod.ToString() == "SetByWall") // Set Reverse on or off on Wall object
            {
                button.transform.position = startPos;
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                        button.transform.position = pressedPos;
                        playExit.PlayEvent = EmitterGameEvent.None;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")
            {
                button.transform.position = startPos;
                //playExit.Play();
            }

            if (hideMoveMethod.ToString() == "MoveAllWalls")
            {
                button.transform.position = startPos;
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        walls.GetComponent<ReversePressurePlateMover>().move = false;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockPermanent")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        button.transform.position = pressedPos;
                        playExit.PlayEvent = EmitterGameEvent.None;
                    }
                }
            }

            if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")
            {
                button.transform.position = startPos;
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
                walls.GetComponent<ReversePressurePlateMover>().move = true;
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
                    walls.GetComponent<ReversePressurePlateMover>().move = false;
                }
            }

            playTimerFast.Stop();
            playBong.Play();
            playExit.Play();

            button.transform.position = startPos;

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
                if (walls.GetComponent<ReverseWallController>().showMe == false)
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
                    if (walls.GetComponent<ReverseWallController>().showMe == false)
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

            button.transform.position = startPos;

            yield return new WaitForSeconds(0.5f);

            hasHidden = false;
        }
    }
}