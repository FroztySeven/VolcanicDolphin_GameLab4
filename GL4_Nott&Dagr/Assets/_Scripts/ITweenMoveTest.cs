using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITweenMoveTest : MonoBehaviour
{
    private Vector3 startPos;
    private float speed;
    private float[] velocityCheck = new float[2];
    private Rigidbody2D theRB;

    private void Start()
    {
        startPos = transform.position;
        theRB = GetComponent<Rigidbody2D>();
        velocityCheck[0] = 0f;
        velocityCheck[1] = 0f;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        iTween.Stop();
    //        transform.position = startPos;
    //        PathOne();
    //    }
    //}

    private void FixedUpdate()
    {
        velocityCheck[1] = velocityCheck[0];
        velocityCheck[0] = theRB.position.x;

        //speed = velocityCheck[1] - velocityCheck[0];
    }

    private void PathOne()
    {
        TurnOffRigidbody();
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("TestPath"), "time", 1, "easetype", iTween.EaseType.easeInSine, "oncomplete", "PathTwo"));
    }

    private void PathTwo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("TestPath2"), "time", 1, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "TurnOnRigidbody"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "StartBounce")
        {
            PathOne();
        }
    }

    private void TurnOffRigidbody()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void TurnOnRigidbody()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
