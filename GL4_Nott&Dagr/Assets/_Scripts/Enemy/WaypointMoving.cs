using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMoving : MonoBehaviour
{

    [SerializeField] private Transform[] Waypoints;

    [SerializeField] private float moveSpeed;

    private int waypointIndex = 0;

    private void Start()
    {
        transform.position = Waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, Waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

        if (transform.position == Waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == Waypoints.Length) waypointIndex = 0;
    }
}
