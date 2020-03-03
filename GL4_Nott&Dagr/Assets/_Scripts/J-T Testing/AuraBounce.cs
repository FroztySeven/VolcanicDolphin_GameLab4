using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraBounce : MonoBehaviour
{
    private PlayerMovementTest player;

    private float auraBounceTimer;
    private bool auraBounce;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementTest>();
    }

    private void Update()
    {
        if (auraBounce)
        {
            auraBounceTimer += Time.deltaTime;

            if (auraBounceTimer >= 1)
            {
                auraBounceTimer = 0;
                auraBounce = false;
                player.auraBounce = false;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.layer == layer)
    //    {
    //        //float angle = Quaternion.Angle(other.transform.rotation, GetComponent<Collider2D>().transform.rotation);
    //        Vector3 angle = other.transform.position - GetComponent<Collider2D>().transform.position;
    //        //angle.y = Mathf.Rad2Deg;
    //        //angle = Mathf.Rad2Deg;
    //        Debug.Log("Angle P: " + playerId + " " + angle.y);
    //        //other.GetComponent<AuraBounce>().player.canMove = false;
    //        Vector2 bounceDirection = other.transform.position - GetComponent<Collider2D>().transform.position;
    //        if (angle.y > 1.4f)
    //        {
    //            other.GetComponent<AuraBounce>().playerRB.AddForce(bounceDirection * 500, ForceMode2D.Force);
    //        }
    //        else
    //        {
    //            other.GetComponent<AuraBounce>().playerRB.AddForce(bounceDirection * 1000, ForceMode2D.Force);
    //        }

    //        AuraBounceTimer(other.gameObject);
    //        //StartCoroutine(EnableMovement(other.gameObject));
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    Debug.Log("Boom!");
    //    //if (other.gameObject.layer == layer)
    //    //{
    //        Vector3 normal = other.contacts[0].normal;

    //    //Debug.Log(other.gameObject.GetComponentInChildren<AuraBounce>());
    //        Vector3 vel = other.gameObject.GetComponentInChildren<AuraBounce>().playerRB.velocity;
    //        //float angle = Quaternion.Angle(other.transform.rotation, GetComponent<Collider2D>().transform.rotation);
    //        //Vector3 angle = other.transform.position - GetComponent<Collider2D>().transform.position;
    //        //angle.y = Mathf.Rad2Deg;
    //        //angle = Mathf.Rad2Deg;
    //        Debug.Log("Angle P" + playerId + ": " + Vector3.Angle(vel, -normal));
    //        //other.GetComponent<AuraBounce>().player.canMove = false;
    //        Vector2 bounceDirection = other.transform.position - GetComponent<Collider2D>().transform.position;
    //        if (Vector3.Angle(other.gameObject.GetComponentInChildren<AuraBounce>().playerRB.velocity, -normal) > 1.4f)
    //        {
    //            other.gameObject.GetComponentInChildren<AuraBounce>().playerRB.AddForce(bounceDirection * 20, ForceMode2D.Impulse);
    //        }
    //        else
    //        {
    //            other.gameObject.GetComponentInChildren<AuraBounce>().playerRB.AddForce(bounceDirection * 10, ForceMode2D.Impulse);
    //        }

    //        AuraBounceTimer(other.gameObject);
    //        //StartCoroutine(EnableMovement(other.gameObject));
    //   // }
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        AuraBounceTimer(other.gameObject.GetComponentInChildren<AuraBounce>().gameObject);
    }

    private void AuraBounceTimer(GameObject player)
    {
        player.GetComponent<AuraBounce>().player.auraBounce = true;
        player.GetComponent<AuraBounce>().auraBounce = true;
    }

    //private IEnumerator EnableMovement(GameObject player)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    if (player.GetComponent<AuraBounce>().player.isGrounded)
    //    {
    //        player.GetComponent<AuraBounce>().player.canMove = true;
    //    }
    //}
}
