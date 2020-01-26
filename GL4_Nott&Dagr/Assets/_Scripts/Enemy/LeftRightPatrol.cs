using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightPatrol : MonoBehaviour
{
    public float speed;
    public float rayDist;
    private bool _moveRight;
    public Transform groundDetection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDist);

        if (groundCheck.collider == false)
        {
            if (_moveRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _moveRight = true;
            }
        }
    }
}
