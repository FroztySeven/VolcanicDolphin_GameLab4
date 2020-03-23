using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITweenMoveTest : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D theRB;

    private iTweenPath[] paths;
    private iTweenPath firstPath;

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        paths = GetComponents<iTweenPath>();

        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].pathName == "Path1")
            {
                firstPath = paths[i];
            }
        }
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

    private void PathOne()
    {
        TurnOffRigidbody();
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path1"), "time", speed, "easetype", iTween.EaseType.easeInSine, "oncomplete", "PathTwo"));
    }

    private void PathTwo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path2"), "time", speed, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "TurnOnRigidbody"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "StartBounce")
        {
            firstPath.nodes[0] = transform.position;
            PathOne();
        }
    }

    private void TurnOffRigidbody()
    {
        theRB.isKinematic = true;
    }

    private void TurnOnRigidbody()
    {
        theRB.velocity = Vector2.zero;
        theRB.isKinematic = false;
    }
}
