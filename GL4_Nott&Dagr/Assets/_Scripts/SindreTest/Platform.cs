using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Platform : MonoBehaviour
{
    public bool upAndDown = false, sideWays = false, seeSaw = false, disappearing = false;
    [Header("Movement:")]
    [Range(0.1f, 10.0f)] public float moveDistance;
    [Range(0.1f, 10.0f)] public float moveSpeed;
    [Header("Seesaw:")]
    [Range(0.1f, 30.0f)] public float seeSawSpeed;
    [Header("Disappearing:")] 
    public bool startDisappeared = false;
    public bool disappearBehindNott = false, disappearBehindDagr = false;
    public bool turnOnByNott = false, turnOnByDagr = false;
    
    
    private float yCenter, xCenter;

    private HingeJoint2D _hingeJoint2D;
    private JointMotor2D _jointMotor2D;
    private JointAngleLimits2D _limits;
    private float startSeeSawSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        yCenter = transform.position.y - (moveDistance / 2);
        xCenter = transform.position.x - (moveDistance / 2);

        _hingeJoint2D = gameObject.GetComponent<HingeJoint2D>();
        _jointMotor2D = _hingeJoint2D.motor;
        startSeeSawSpeed = seeSawSpeed;

        if (disappearing) //turning off the platform if the start disappeared is chosen
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5,3); //make the trigger bigger when dissapearing is chosen
            if (startDisappeared)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.GetComponentInChildren<Renderer>().enabled = false;
                    child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sideWays)
        {
            gameObject.transform.position = new Vector2(xCenter + Mathf.PingPong(Time.time * moveSpeed,moveDistance),transform.position.y);
        }

        if (upAndDown)
        {
            gameObject.transform.position = new Vector2(transform.position.x, yCenter + Mathf.PingPong(Time.time * moveSpeed, moveDistance));
        }

        if (seeSaw)
        {
            _jointMotor2D.motorSpeed = seeSawSpeed;
            _hingeJoint2D.motor = _jointMotor2D;
            _limits.min = -45;
            _limits.max = 45;
            _hingeJoint2D.limits = _limits;

            if (gameObject.transform.rotation.z <= -0.38) //Used debug.log rotation z to find this value
            {
                seeSawSpeed = -startSeeSawSpeed;
            }

            if (gameObject.transform.rotation.z >= 0.38)
            {
                seeSawSpeed = startSeeSawSpeed;
            }
        }
        else //if not seesaw
        {
            _limits.min = 0;
            _limits.max = 359;
            _hingeJoint2D.limits = _limits;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }

        if (disappearing)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.SetParent(null);
                if (disappearBehindNott)
                {
                    if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night")
                    {
                        foreach (Transform child in gameObject.transform)
                        {
                            child.GetComponentInChildren<Renderer>().enabled = false;
                            child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                        }

                        StartCoroutine(turnOnPlatform());
                    }
                }

                if (disappearBehindDagr)
                {
                    if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day")
                    {
                        foreach (Transform child in gameObject.transform)
                        {
                            child.GetComponentInChildren<Renderer>().enabled = false;
                            child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                        }

                        StartCoroutine(turnOnPlatform());
                    }
                }

                if (startDisappeared) //platform start disappeared
                {
                    if (turnOnByNott)
                    {
                        if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Night") //night turns on a platform
                        {
                            foreach (Transform child in gameObject.transform)
                            {
                                child.GetComponentInChildren<Renderer>().enabled = true;
                                child.GetComponentInChildren<BoxCollider2D>().enabled = true;
                            }

                            StartCoroutine(turnOffPlatform()); // platform being turned off again
                        }
                    }

                    if (turnOnByDagr)
                    {
                        if (other.gameObject.GetComponent<PlayerMovementTest>().setPlayer.ToString() == "Day") //day turns on a platform
                        {
                            foreach (Transform child in gameObject.transform)
                            {
                                child.GetComponentInChildren<Renderer>().enabled = true;
                                child.GetComponentInChildren<BoxCollider2D>().enabled = true;
                            }

                            StartCoroutine(turnOffPlatform()); // platform being turned off again
                        }
                    }
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private IEnumerator turnOnPlatform()
    {
        yield return new WaitForSeconds(2);
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponentInChildren<Renderer>().enabled = true;
            child.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
    }

    private IEnumerator turnOffPlatform()
    {
        yield return new WaitForSeconds(2);
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponentInChildren<Renderer>().enabled = false;
            child.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }
}
