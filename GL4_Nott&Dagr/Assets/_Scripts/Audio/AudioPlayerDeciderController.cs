using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerDeciderController : MonoBehaviour
{
    private GameObject player1, player2;

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
        }
        else if (this.gameObject == player2)
        {
            isDagr = false;
            isNott = true;
            player2.transform.Find("AudioTriggerDagr").gameObject.SetActive(false);
            player2.transform.Find("AudioTriggerNott").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
