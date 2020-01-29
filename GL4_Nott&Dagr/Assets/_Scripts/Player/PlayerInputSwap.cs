using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputSwap : MonoBehaviour
{
    public bool swapInputs;
    
    // Update is called once per frame
    void Update()
    {
        // Xbox controller button Y toggle
        if(Input.GetButtonDown("SwapP1P2"))
        {
            swapInputs = !swapInputs;

            if (swapInputs)
            {
                GameObject.Find("Player1").GetComponent<PlayerMovementTest>().playerId = 2;
                GameObject.Find("Player2").GetComponent<PlayerMovementTest>().playerId = 1;
                swapInputs = true;

                print("Player inputs swapped");
            }
            else if (!swapInputs)
            {
                GameObject.Find("Player1").GetComponent<PlayerMovementTest>().playerId = 1;
                GameObject.Find("Player2").GetComponent<PlayerMovementTest>().playerId = 2;

                print("Player inputs swapped");
            }
        }
    }
}
