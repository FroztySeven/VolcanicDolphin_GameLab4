using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MinionIdleMovement : MonoBehaviour
{
    [HideInInspector] public bool onPath = false;
    
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!onPath)
        {
            transform.localPosition = new Vector2(Mathf.PingPong(Time.time, 1.5f), transform.localPosition.y);
        }
    }
}