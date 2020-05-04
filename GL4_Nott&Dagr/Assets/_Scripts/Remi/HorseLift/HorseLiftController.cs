using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


[SelectionBase]
public class HorseLiftController : MonoBehaviour
{
    
    //This part can be used through the Inspector
    [Header("HorseLift Settings")] [SerializeField]
    public float moveSpeed = 1f;
    private int currentWayPointId;

    [SerializeField] private float reachDistance = 0.1f;

    [Header("Movement Path Setup")]
    [SerializeField] private Transform currentLift;
    [SerializeField] private Transform movePathChild;
    [SerializeField] private HorseLiftPath horseLiftPath;
    //
    
    //This part is not to be used through the Inspector anymore
    [Obsolete]
    [HideInInspector] public enum SpeedModifier
    {
        ConsistentSpeed,
        SplitSpeed
    }
    [Header("Bubble Settings")] private SpeedModifier pathSpeedModifier;

    [Range(0f, 10.0f)] private float consistentSpeed = 0.4f;

    [Space(20)] [Range(0f, 10.0f)] private float splitForwardSpeed = 0.4f;
    [Range(0f, 10.0f)] private float splitBackwardsSpeed = 0.4f;
    //
    
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 startPos;
    private Vector3 endPos;

    private PlayerController _playerDay;
    private PlayerController _playerNight;
    private readonly HashSet<PlayerController> _playersOnLift = new HashSet<PlayerController>();

    private void Start()
    {
        _playerDay = GameObject.Find("Player1").GetComponent<PlayerController>();
        _playerNight = GameObject.Find("Player2").GetComponent<PlayerController>();

        posA = currentLift.localPosition;
        posB = movePathChild.localPosition;
        endPos = posB;
        startPos = transform.position;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Get the PlayerController component of the object for the trigger
        //If there's no PlayerController, it will be 'null'
        var triggeredCollider = other.GetComponent<PlayerController>();

        //Skip if the object doesn't have the PlayerController component or the tag is not 'Player'
        if (triggeredCollider == null || !other.gameObject.CompareTag("Player"))
            return;

        //Switch statement for handling which player is staying on the trigger
        switch (triggeredCollider.setPlayer.ToString())
        {
            case "Night":
                AddPlayerOnPlatform(triggeredCollider);
                break;
            case "Day":
                AddPlayerOnPlatform(triggeredCollider);
                break;
        }
    }
    private void AddPlayerOnPlatform(PlayerController playerController)
    {
        playerController.transform.parent = this.transform;
        playerController.theRB.interpolation = RigidbodyInterpolation2D.None;
        playerController.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = false;
        _playersOnLift.Add(playerController);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Get the PlayerController component of the object for the trigger
        //If there's no PlayerController, it will be 'null'
        var triggeredCollider = other.GetComponent<PlayerController>();

        //Skip if the object doesn't have PlayerController component or the tag is not 'Player'
        if (triggeredCollider == null || !other.gameObject.CompareTag("Player"))
            return;

        //Switch statement for handling which player has left the trigger
        switch (triggeredCollider.setPlayer.ToString())
        {
            case "Night":
                RemovePlayerOfLift(triggeredCollider);
                break;
            case "Day":
                RemovePlayerOfLift(triggeredCollider);
                break;
        }
    }

    private void RemovePlayerOfLift(PlayerController playerController)
    {
        playerController.transform.parent = null;
        playerController.theRB.interpolation = RigidbodyInterpolation2D.Interpolate;
        playerController.transform.Find("Aura").GetComponent<CapsuleCollider2D>().enabled = true;
        _playersOnLift.Remove(playerController);
    }

    private void Update()
    {
        //If `Day` is on the Lift
        if (_playersOnLift.Contains(_playerDay))
        {
            MoveForward();
        }
        //If `Night` is on the Lift but `Day` isn't
        else if (_playersOnLift.Contains(_playerNight) && !_playersOnLift.Contains(_playerDay))
        {
            return;
        }
        //All the other cases
        else
        {
            MoveBackwards();
        }
    }

    private void MoveForward()
    {
        //Lined-way
        if (currentLift != null && movePathChild != null)
        {
            MoveTo(endPos, splitForwardSpeed);
        }

        //HorseLiftPath-way
        if (horseLiftPath != null)
        {
            MoveHorseLift(true);
        }
    }

    private void MoveBackwards()
    {
        //Lined-way
        if (currentLift != null && movePathChild != null)
        {
            MoveTo(startPos, splitBackwardsSpeed);
        }

        //HorseLiftPath-way
        if (horseLiftPath != null)
        {
            MoveHorseLift(false);
        }
    }

    private void MoveHorseLift(bool moveForward = true)
    {
        if (moveForward)
        {
            float moveDistance =
                Vector3.Distance(horseLiftPath.path_objs[currentWayPointId].position, transform.position);

            if (moveDistance <= reachDistance)
            {
                transform.position = horseLiftPath.path_objs[currentWayPointId].position;
                if (currentWayPointId < horseLiftPath.path_objs.Count - 1)
                {
                    currentWayPointId++;
                }
            }

            MoveTowardsWayPoint(currentWayPointId);
        }
        else
        {
            int newId = currentWayPointId > 0 ? currentWayPointId - 1 : 0;

            float moveDistance =
                Vector3.Distance(horseLiftPath.path_objs[newId].position, transform.position);

            if (moveDistance <= reachDistance)
            {
                transform.position = horseLiftPath.path_objs[newId].position;
                if (currentWayPointId > 0)
                {
                    currentWayPointId--;
                }
            }

            MoveTowardsWayPoint(newId);
        }
    }

    private void MoveTowardsWayPoint(int wayPointId)
    {
        transform.position = Vector3.MoveTowards(transform.position,
            horseLiftPath.path_objs[wayPointId].position, Time.deltaTime * moveSpeed);
    }


    //Another obsolete part, don't mind it
    [Obsolete]
    private void MoveTo(Vector3 toPoint, float speed)
    {
        if (pathSpeedModifier.ToString() == "ConsistentSpeed")
        {
            currentLift.localPosition =
                Vector3.MoveTowards(currentLift.localPosition, toPoint, speed * Time.deltaTime);
        }
        else if (pathSpeedModifier.ToString() == "SplitSpeed")
        {
            currentLift.localPosition =
                Vector3.MoveTowards(currentLift.localPosition, toPoint, speed * Time.deltaTime);
        }
    }
    //
}