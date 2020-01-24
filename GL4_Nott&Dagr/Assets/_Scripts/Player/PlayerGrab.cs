using UnityEngine;
using System.Collections;

public class PlayerGrab : MonoBehaviour {

	public bool isGrabbing;
	RaycastHit2D hit;
	public float distance = 2f;
	public Transform GrabbingPoint;
	public float ThrowForce;
	public LayerMask NotGrabbed;

	// Use this for initialization
	void Start ()
    {
        //GrabbingPoint = GameObject.Find("GrabbingPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown("joystick button 2"))
        {
			if (!isGrabbing)
			{
				Physics2D.queriesStartInColliders = false;

			    hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

				if (hit.collider!= null && hit.collider.tag == "AllowGrab")
				{
					isGrabbing=true;
				}

			// Grabbing mechanic
			}
                else if (!Physics2D.OverlapPoint(GrabbingPoint.position, NotGrabbed))
            {
				isGrabbing = false;

				if(hit.collider.gameObject.GetComponent<Rigidbody2D>()!= null)
				{
					hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(transform.localScale.x, 1) * ThrowForce;
				}
			}
		}

        //Throwing mechanic
        if (isGrabbing)
            hit.collider.gameObject.transform.position = GrabbingPoint.position;
	}

    //Draw raycast line
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
	}
}
