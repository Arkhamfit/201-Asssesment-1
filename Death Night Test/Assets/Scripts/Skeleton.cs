using System.Collections;
using UnityEngine;

// This class represents the EnemyController in the game
public class EnemyController : MonoBehaviour
{
    // Enemy's speed and hitpoints
    public float speed = 3.0f;
    public int hitpoints = 100;
    // References to the player and various components
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the player and various components
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 1.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction towards the player and set the velocity
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Flip the enemy's sprite based on the direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Triggered when the enemy collides with another object
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Change the enemy's color to red
            spriteRenderer.color = Color.red;
            // Revert the color after a delay
            StartCoroutine(RevertColor());

            // If the player's collider is a BoxCollider2D
            if (collision.collider is BoxCollider2D)
            {
                // Decrease the player's hitpoints
                collision.gameObject.GetComponent<PlayerControl>().hitpoints -= 10;

                // Add a small knockback effect
                Vector2 knockbackDirection = (transform.position - player.position).normalized;
                rb.AddForce(knockbackDirection * 150f, ForceMode2D.Impulse);
            }
            // If the player's collider is a CircleCollider2D
            else if (collision.collider is CircleCollider2D)
            {
                // Call the OnHitByPlayer method
                OnHitByPlayer();
            }
        }
    }

    // Coroutine to revert the enemy's color after a delay
    IEnumerator RevertColor()
    {
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.color = Color.white;
    }

    // Called when the enemy is hit by the player
    public void OnHitByPlayer()
    {
        // Decrease the enemy's hitpoints
        hitpoints -= 50;
        // Change the enemy's color to red
        spriteRenderer.color = Color.red;
        // Revert the color after a delay
        StartCoroutine(RevertColor());

        // If the enemy's hitpoints reach 0
        if (hitpoints <= 0)
        {
            // Trigger the Death animation
            animator.SetTrigger("Death");
            // Destroy the enemy after a delay
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }

    // Coroutine to destroy the enemy after a delay
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // This function is called when the enemy is hit by the Arcane Bolt
    public void TakeDamage(int damage)
    {
        // Decrease the enemy's hitpoints
        hitpoints -= damage;

        // If the enemy's hitpoints reach 0
        if (hitpoints <= 0)
        {
            // Trigger the Death animation
            animator.SetTrigger("Death");
            // Destroy the enemy after a delay
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }
}