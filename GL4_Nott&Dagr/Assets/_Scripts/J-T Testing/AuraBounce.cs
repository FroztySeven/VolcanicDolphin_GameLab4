using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBounce : MonoBehaviour
{

    private PlayerMovementTest player;

    //[HideInInspector]
    public Rigidbody2D playerRB;

    private int playerId;

    private int layer;
    private float auraBounceTimer;
    private bool auraBounce;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementTest>();
        playerRB = GetComponentInParent<Rigidbody2D>();

        playerId = player.playerId;

        layer = gameObject.layer;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == layer)
        {
            Debug.Log("Player " + other.GetComponent<AuraBounce>().playerId + " hit me!");
            //other.GetComponent<AuraBounce>().player.canMove = false;
            Vector2 bounceDirection = other.transform.position - transform.position;
            AuraBounceTimer(other.gameObject);
            other.GetComponent<AuraBounce>().playerRB.AddForce(bounceDirection * 500, ForceMode2D.Force);
            //StartCoroutine(EnableMovement(other.gameObject));
        }
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
