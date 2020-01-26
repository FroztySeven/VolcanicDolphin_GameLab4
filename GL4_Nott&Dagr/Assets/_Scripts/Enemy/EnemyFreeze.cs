using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    public bool startUnfreezing = false;
    public float unfreezeTimer;
    private LeftRightPatrol _leftRightPatrol;


    private void Start()
    {
        _leftRightPatrol = gameObject.GetComponentInParent<LeftRightPatrol>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            Debug.Log("Enemy frozen!");
            gameObject.GetComponentInParent<LeftRightPatrol>().speed = 0f;
            transform.GetChild(0).gameObject.SetActive(true);
            unfreezeTimer = 5f;
            startUnfreezing = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponentInParent<LeftRightPatrol>().speed = gameObject.GetComponentInParent<LeftRightPatrol>().speed ;
        startUnfreezing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startUnfreezing)
        {
            unfreezeTimer -= Time.deltaTime;
        }

        if (unfreezeTimer < 0)
        {
            startUnfreezing = false;
            _leftRightPatrol.speed = 2f;
            unfreezeTimer = 5f;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
