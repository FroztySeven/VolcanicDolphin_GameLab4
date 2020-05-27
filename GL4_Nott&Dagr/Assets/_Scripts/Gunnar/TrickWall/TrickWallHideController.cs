using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrickWallHideController : MonoBehaviour
{
    //... Notice! This script was originally made by Jan-Tore.
    //... Original scripts used were PressurePlates and PressurePlatesWall. 
    //... I (Gunnar), made a copy those scripts to make alterations to them, add more variants of wall behaviours that I wanted.

    // I wanted more options on using the walls, mostly it started by wanting more then one pressure plate to control the same walls, then I wanted to mix and match walls with the plates, to
    // move them or hide them, even with a timer. By using the multiplates, it makes sure that all the plates in multiplates update the wall functions equally, so if one plate is pressed
    // and it changes 0 to 1 all the plates in multiplates should have changed 0 to 1.

    public enum WhoCanUse { Night, Day, Both }

    public WhoCanUse setUser;

    public enum HideMoveMethod { HideAllOnStart, ShowAllOnStart, SetByWall, SetByWallLockPermanent, SetByWallLockUnlockOnEnter, SetByWallLockTimer, MoveAllWalls, MoveLockPermanent, MoveLockUnlockOnEnter, MoveLockOnTimer }

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

    //---- Testing ----//

    public List<GameObject> clones = new List<GameObject>();

    private void Start()
    {
        //Invoke("FindChildren", 0.25f);

        //---- Testing ----//

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

        if (hideMoveMethod.ToString() == "HideAllOnStart") // This function hides all the walls when game starts and shows them when the plate is pressed.
        {
            HideWall();
        }

        if (hideMoveMethod.ToString() == "ShowAllOnStart") // This function shows all the walls when game starts and hides them when plate is pressed.
        {
            ShowWall();
        }

        if (hideMoveMethod.ToString() == "SetByWall") // With this you can select which wall gameobject should hide on start, if you want to show one wall and hide another.
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

        if (hideMoveMethod.ToString() == "SetByWallLockPermanent") // This is single use, it permanently shows or hides walls, then locks the button.
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

        if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter") // This is to make it so the plate needs to be pressed two times to turn on/off the hide or show instead of it happening
                                                                       // automatically when stepping off the plates.
        {
            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {
                    playEnter.PlayEvent = EmitterGameEvent.None;
                    playExit.PlayEvent = EmitterGameEvent.None;

                    Invoke("FindChildren", 0.25f);
                }
            }
        }

        if (hideMoveMethod.ToString() == "SetByWallLockTimer") // This makes the walls hide/show on a timer. Button can not be pressed again while the timer is on.
        {
            playEnter.PlayEvent = EmitterGameEvent.None;
            playExit.PlayEvent = EmitterGameEvent.None;

            for (int i = 0; i < walls.Length; i++)
            {
                foreach (GameObject walls in walls)
                {

                    if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

        if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")   // This is to make it so the plate needs to be pressed two times to move in the set direction and back instead of it happening
                                                                    // automatically when stepping off the plates.
        {
            playEnter.PlayEvent = EmitterGameEvent.None;
            playExit.PlayEvent = EmitterGameEvent.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // All the chosen function that will happen/start when player/minon enter the plate collider and sets the plate sprite to pressed state.
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == setUser.ToString() || setUser.ToString() == "Both")
            {
                button.GetComponent<SpriteRenderer>().sprite = pressed;

                if (hideMoveMethod.ToString() == "HideAllOnStart") // This function hides all the walls when game starts and shows them when the plate is pressed.
                {
                    ShowWall();
                }

                if (hideMoveMethod.ToString() == "ShowAllOnStart") // This function shows all the walls when game starts and hides them when plate is pressed.
                {
                    HideWall();
                }

                if (hideMoveMethod.ToString() == "SetByWall") // With this you can select which wall gameobject should hide on start, if you want to show one wall and hide another.
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

                if (hideMoveMethod.ToString() == "SetByWallLockPermanent") // This is single use, it permanently shows or hides walls, then locks the button.
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

                if (hideMoveMethod.ToString() == "SetByWallLockUnlockOnEnter")  // This is to make it so the plate needs to be pressed two times to move in the set direction and back instead of it happening
                                                                                // automatically when stepping off the plates.
                {
                    playEnter.Play();

                    if (lastPressed != 1)
                    {
                        foreach (var plate in multiPlates)
                        {
                            plate.GetComponent<TrickWallHideController>().lastPressed = 1;
                        }

                        foreach (var clone in clones)
                        {
                            if (clone.GetComponent<TrickWallBoolChecker>().imHidden == true)
                            {
                                clone.gameObject.SetActive(true);
                            }
                            else
                            {
                                clone.gameObject.SetActive(false);
                            }
                        }
                        /*
                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                if (walls.GetComponent<TrickWallWallController>().hideMe == false)
                                {
                                    //walls.SetActive(false);
                                    
                                    foreach (var clone in clones)
                                    {
                                        clone.SetActive(false);
                                    }
                                    
                                }
                                else
                                {
                                    //walls.SetActive(true);
                                    
                                    foreach (var clone in clones)
                                    {
                                        clone.SetActive(true);
                                    }
                                }
                            }
                        }*/
                    }
                    else
                    {
                        foreach (var plate in multiPlates)
                        {
                            plate.GetComponent<TrickWallHideController>().lastPressed = 0;
                        }

                        foreach (var clone in clones) // Hides all the clones insted of hiding the parent, this was needed to make all the parents with multiplates would update their on/off position correctly.
                        {
                            if (clone.GetComponent<TrickWallBoolChecker>().imHidden == true)
                            {
                                clone.gameObject.SetActive(false);
                            }
                            else
                            {
                                clone.gameObject.SetActive(true);
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "SetByWallLockTimer") // This makes the walls hide/show on a timer. Button can not be pressed again while the timer is on.
                {
                    button.GetComponent<SpriteRenderer>().sprite = pressed;

                    if (hasHidden == false)
                    {
                        StartCoroutine(HideWallTimer());
                    }

                }

                if (hideMoveMethod.ToString() == "MoveAllWalls") // This moves all the walls assigned to their direction when plate is pressed and back when off the plate.
                {
                    for (int i = 0; i < walls.Length; i++)
                    {
                        foreach (GameObject walls in walls)
                        {
                            walls.GetComponent<TrickWallMoveController>().move = true;
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockPermanent") // Moves the walls and then locks the plate permanently.
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

                if (hideMoveMethod.ToString() == "MoveLockUnlockOnEnter")   // This is to make it so the plate needs to be pressed two times to move in the set direction and back instead of it happening
                                                                            // automatically when stepping off the plates.
                {
                    playEnter.Play();

                    if (lastPressed != 1)
                    {
                        foreach (var plate in multiPlates)
                        {
                            plate.GetComponent<TrickWallHideController>().lastPressed = 1;

                            for (int i = 0; i < walls.Length; i++)
                            {
                                foreach (GameObject walls in walls)
                                {
                                    walls.GetComponent<TrickWallMoveController>().move = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var plate in multiPlates)
                        {
                            plate.GetComponent<TrickWallHideController>().lastPressed = 0;

                            for (int i = 0; i < walls.Length; i++)
                            {
                                foreach (GameObject walls in walls)
                                {
                                    walls.GetComponent<TrickWallMoveController>().move = false;
                                }
                            }
                        }
                    }
                }

                if (hideMoveMethod.ToString() == "MoveLockOnTimer") // This makes the walls move on a timer and back when time is up. Button can not be pressed again while the timer is on.
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

        if (other.tag == "Minion") // The minion functions are the same as for the players.
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

            if (hideMoveMethod.ToString() == "SetByWall")
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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
                        if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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
                    foreach (var plate in multiPlates)
                    {
                        plate.GetComponent<TrickWallHideController>().lastPressed = 1;
                    }

                    foreach (var clone in clones)
                    {
                        if (clone.GetComponent<TrickWallBoolChecker>().imHidden == true)
                        {
                            clone.gameObject.SetActive(true);
                        }
                        else
                        {
                            clone.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    foreach (var plate in multiPlates)
                    {
                        plate.GetComponent<TrickWallHideController>().lastPressed = 0;
                    }

                    foreach (var clone in clones)
                    {
                        if (clone.GetComponent<TrickWallBoolChecker>().imHidden == true)
                        {
                            clone.gameObject.SetActive(false);
                        }
                        else
                        {
                            clone.gameObject.SetActive(true);
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

                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                walls.GetComponent<TrickWallMoveController>().move = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var plate in multiPlates)
                    {
                        plate.GetComponent<TrickWallHideController>().lastPressed = 0;

                        for (int i = 0; i < walls.Length; i++)
                        {
                            foreach (GameObject walls in walls)
                            {
                                walls.GetComponent<TrickWallMoveController>().move = false;
                            }
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

    private void OnTriggerExit2D(Collider2D other) // All the chosen function that will happen/stop when player/minon exit the plate collider and resets the plate sprite to unpressed state.
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
                            if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

            if (hideMoveMethod.ToString() == "SetByWall")
            {
                button.GetComponent<SpriteRenderer>().sprite = unPressed;
                for (int i = 0; i < walls.Length; i++)
                {
                    foreach (GameObject walls in walls)
                    {
                        if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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

    void FindChildren()
    {
        foreach (var clone in clones)
        {
            if (clone.GetComponent<TrickWallBoolChecker>().imHidden == true)
            {
                clone.gameObject.SetActive(false);
            }
            else
            {
                clone.gameObject.SetActive(true);
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
                if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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
                    if (walls.GetComponent<TrickWallWallController>().hideMe == false)
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