using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float constantSpeed = 10f; //speed of cookie

    public bool playerCanDie = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * constantSpeed;
        StartCoroutine(EnablePlayerDeath()); //stop cookie from immediately killing player
    }

    void Update()
    {
        // Maintain constant velocity in the forward direction
        Vector3 currentVelocity = rb.velocity;
        rb.velocity = currentVelocity.normalized * constantSpeed;
    }

    private IEnumerator EnablePlayerDeath() {
        yield return new WaitForSeconds(0.25f); //wait for 1/4 of second
        playerCanDie = true; //player can now die
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //hit enemy?

        if (collision.gameObject.CompareTag("Enemy"))
        { //|| collision.gameObject.CompareTag("Player")
            Destroy(gameObject); // destroy cookie
            Destroy(collision.gameObject); // kill enemy/player
        }

        if (collision.gameObject.CompareTag("Player") && playerCanDie)
        { //|| collision.gameObject.CompareTag("Player")
            Destroy(gameObject); // destroy cookie
            Destroy(collision.gameObject); // kill enemy/player
        }

    }
}


