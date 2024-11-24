using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform player; // Drag the player GameObject into this field in the inspector
    public float moveSpeed = 10f; // Movement speed of the zombie

    private Rigidbody2D rb;

    private GameManager gameManager;
    void Start()
    {
        //player = GetComponent
        rb = GetComponent<Rigidbody2D>(); 
        gameManager = FindObjectOfType<GameManager>();
        //ignore collisions with walls
        int walls = LayerMask.NameToLayer("Wall");
        Physics2D.IgnoreLayerCollision(gameObject.layer, walls, true);

        float randomScale = Random.Range(1.5f, 3f); // Adjust the range as needed
        transform.localScale = new Vector3(randomScale, randomScale, 1); // Set random size
    }

    public void SetMoveSpeed(float difficulty)
    {
        //make enemies progressively faster 
        moveSpeed *= difficulty;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            float currentScaleX = Mathf.Abs(transform.localScale.x);

            // Flip the sprite based on the direction
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(currentScaleX, transform.localScale.y, transform.localScale.z); // Face left
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-currentScaleX, transform.localScale.y, transform.localScale.z); // Face right
            }

            // Move the enemy towards the player
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Destroy(collision.gameObject); //kill player
        }
    }

    private void OnDestroy() {
        respawnEnemy();
    }

    IEnumerator respawnEnemy() {
        yield return new WaitForSeconds(0.2f); //wait for 1/5 of second
        gameManager.spawnEnemy(); //spawn enemy when this one is destroyed
    }
    
}

