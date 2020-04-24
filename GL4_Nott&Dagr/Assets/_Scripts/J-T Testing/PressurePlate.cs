using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public enum WhoCanUse { Night, Day, Both }

    public WhoCanUse setUser;

    public enum WhatToUse { Wall, Platform, Rope }

    public WhatToUse setItemToControl;

    public GameObject wall;
    public GameObject platform;
    public GameObject rope;
    public GameObject button;
    public int ropeLengthWhenOnPlate;

    [Header("Sprites")]
    public Sprite unPressed;
    public Sprite pressed;

    private Platform thePF;
    private float platformMoveSpeed;

    private GameObject ropeDuplicate;

    private SpriteRenderer theSR;

    private void Start()
    {
        theSR = GetComponentInChildren<SpriteRenderer>();

        if (setItemToControl.ToString() == "Wall")
        {
            wall.SetActive(true);
            platform.SetActive(false);
            rope.SetActive(false);
        }

        if (setItemToControl.ToString() == "Platform")
        {
            wall.SetActive(false);
            platform.SetActive(true);
            rope.SetActive(false);
            thePF = platform.GetComponent<Platform>();
            platformMoveSpeed = thePF.moveSpeed;
            thePF.moveSpeed = 0;
        }

        if (setItemToControl.ToString() == "Rope")
        {
            wall.SetActive(false);
            platform.SetActive(false);
            rope.SetActive(true);
            ropeDuplicate = Instantiate(rope, rope.transform.position, rope.transform.rotation);
            for (int i = 1; i < ropeDuplicate.transform.childCount; i++)
            {
                Destroy(ropeDuplicate.transform.GetChild(i).gameObject);
            }
            ropeDuplicate.GetComponent<SwingingRope>().ropeLength = ropeLengthWhenOnPlate;
            ropeDuplicate.transform.parent = transform.parent;
            ropeDuplicate.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == setUser.ToString())
            {
                theSR.sprite = pressed;

                if (setItemToControl.ToString() == "Wall")
                {
                    wall.SetActive(false);
                }

                if (setItemToControl.ToString() == "Platform")
                {
                    thePF.moveSpeed = platformMoveSpeed;
                }

                if (setItemToControl.ToString() == "Rope")
                {
                    rope.SetActive(false);
                    ropeDuplicate.SetActive(true);
                }
            }

            if (setUser.ToString() == "Both")
            {
                theSR.sprite = pressed;
                if (setItemToControl.ToString() == "Wall")
                {
                    wall.SetActive(false);
                }

                if (setItemToControl.ToString() == "Platform")
                {
                    thePF.moveSpeed = platformMoveSpeed;
                }

                if (setItemToControl.ToString() == "Rope")
                {
                    rope.SetActive(false);
                    ropeDuplicate.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == setUser.ToString())
            {
                theSR.sprite = pressed;
                if (setItemToControl.ToString() == "Wall")
                {
                    wall.SetActive(false);
                }

                if (setItemToControl.ToString() == "Platform")
                {
                    thePF.moveSpeed = platformMoveSpeed;
                }

                if (setItemToControl.ToString() == "Rope")
                {
                    rope.SetActive(false);
                    ropeDuplicate.SetActive(true);
                }
            }

            if (setUser.ToString() == "Both")
            {
                theSR.sprite = pressed;
                if (setItemToControl.ToString() == "Wall")
                {
                    wall.SetActive(false);
                }

                if (setItemToControl.ToString() == "Platform")
                {
                    thePF.moveSpeed = platformMoveSpeed;
                }

                if (setItemToControl.ToString() == "Rope")
                {
                    rope.SetActive(false);
                    ropeDuplicate.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().setPlayer.ToString() == setUser.ToString())
            {
                theSR.sprite = unPressed;
                if (setItemToControl.ToString() == "Wall")
                {
                    wall.SetActive(true);
                }

                if (setItemToControl.ToString() == "Platform")
                {
                    thePF.moveSpeed = 0;
                }

                if (setItemToControl.ToString() == "Rope")
                {
                    rope.SetActive(true);
                    if (ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).transform.childCount > 0)
                    {
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).transform.GetChild(0).GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).transform.DetachChildren();
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).GetComponent<SwingingRopeEndZone>().dagrOnRope = false;
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).GetComponent<SwingingRopeEndZone>().nottOnRope = false;
                        StartCoroutine(ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).GetComponent<SwingingRopeEndZone>().turnOnTriggerAgain());
                    }
                    ropeDuplicate.SetActive(false);
                }
            }

            if (setUser.ToString() == "Both")
            {
                theSR.sprite = unPressed;
                if (setItemToControl.ToString() == "Wall")
                {
                    wall.SetActive(true);
                }

                if (setItemToControl.ToString() == "Platform")
                {
                    thePF.moveSpeed = 0;
                }

                if (setItemToControl.ToString() == "Rope")
                {
                    rope.SetActive(true);
                    if (ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).transform.childCount > 0)
                    {
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).transform.GetChild(0).GetComponent<PlayerController>().theRB.velocity = Vector2.zero;
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).transform.DetachChildren();
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).GetComponent<SwingingRopeEndZone>().dagrOnRope = false;
                        ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).GetComponent<SwingingRopeEndZone>().nottOnRope = false;
                        StartCoroutine(ropeDuplicate.transform.GetChild(ropeDuplicate.transform.childCount - 1).GetChild(0).GetComponent<SwingingRopeEndZone>().turnOnTriggerAgain());
                    }
                    ropeDuplicate.SetActive(false);
                }
            }
        }
    }
}
