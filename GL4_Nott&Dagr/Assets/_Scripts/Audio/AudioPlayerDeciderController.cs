using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerDeciderController : MonoBehaviour
{
    public AudioPlayerController _apl;

    public GameObject player1, player2;

    public bool isDagr, isNott;

    // Start is called before the first frame update
    void Start()
    {


        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");


        if (this.gameObject == player1)
        {
            isDagr = true;
            isNott = false;
            player1.transform.Find("AudioTriggerDagr").gameObject.SetActive(true);
            player1.transform.Find("AudioTriggerNott").gameObject.SetActive(false);

            _apl = player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>();
        }
        else if (this.gameObject == player2)
        {
            isDagr = false;
            isNott = true;
            player2.transform.Find("AudioTriggerDagr").gameObject.SetActive(false);
            player2.transform.Find("AudioTriggerNott").gameObject.SetActive(true);

            _apl = player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        {
            if (isDagr == true)
            {
                //player1.transform.Find("AudioTriggerDagr").gameObject.SetActive(false);
                //player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>().isMoving = false;
                player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>().isGrounded =
                    false;
                player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>().hasLanded =
                    false;
            }

            if (isNott == true)
            {
                //player2.transform.Find("AudioTriggerNott").gameObject.SetActive(false);
                //player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>().isMoving = false;
                player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>().isGrounded =
                    false;
                player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>().hasLanded =
                    false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        {
            if (isDagr == true)
            {
                player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>().isMoving =
                    false;
                player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>().isGrounded =
                    false;
                player1.transform.Find("AudioTriggerDagr").gameObject.GetComponent<AudioPlayerController>().hasLanded =
                    false;
                player1.transform.Find("AudioTriggerDagr").gameObject.SetActive(false);
            }

            if (isNott == true)
            {
                player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>().isMoving =
                    false;
                player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>().isGrounded =
                    false;
                player2.transform.Find("AudioTriggerNott").gameObject.GetComponent<AudioPlayerController>().hasLanded =
                    false;
                player2.transform.Find("AudioTriggerNott").gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        { 
            if (isDagr == true)
            {
                player1.transform.Find("AudioTriggerDagr").gameObject.SetActive(true);
            }

            if (isNott == true)
            {
                player2.transform.Find("AudioTriggerNott").gameObject.SetActive(true);
            }
        }
        
    }
}
