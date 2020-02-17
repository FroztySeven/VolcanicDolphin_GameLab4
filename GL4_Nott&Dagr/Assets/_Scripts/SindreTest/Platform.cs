using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Platform : MonoBehaviour
{
    public bool upAndDown = false, sideWays = false, seeSaw = false, disappearing = false;
    [Range(0.1f, 10.0f)] public float moveDistance;
    [Range(0.1f, 10.0f)] public float moveSpeed;
    [Range(0.1f, 30.0f)] public float seeSawSpeed;
    
    private float yCenter, xCenter;

    private HingeJoint2D _hingeJoint2D;
    private JointMotor2D _jointMotor2D;
    private JointAngleLimits2D _limits;
    private float startSeeSawSpeed;
    
    private bool fadeInIsRunning = false, fadeOutIsRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        yCenter = transform.position.y - (moveDistance / 2);
        xCenter = transform.position.x - (moveDistance / 2);

        _hingeJoint2D = gameObject.GetComponent<HingeJoint2D>();
        _jointMotor2D = _hingeJoint2D.motor;
        startSeeSawSpeed = seeSawSpeed;

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

        if (disappearing)
        {
            //gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
