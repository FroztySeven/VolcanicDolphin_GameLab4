using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public string[] controllers;

    public int playerId;

    public int controllerId = 0;
    public List<int> controllerNumber = new List<int>();

    private void Start()
    {
        controllers = Input.GetJoystickNames();

        if (controllers.Length > 0)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                if (controllers[i].ToString() == "")
                {
                    controllerId++;
                }
                else
                {
                    controllerNumber.Add(controllerId);
                    controllerId++;
                }

            }
        }
    }

    private void Update()
    {
        controllers = Input.GetJoystickNames();

        if (controllers.Length > 0)
        {
            if (Input.GetButtonDown("Xbox" + (controllerNumber[playerId] + 1)))
            {
                print(playerId);
            }
        }
    }
}
