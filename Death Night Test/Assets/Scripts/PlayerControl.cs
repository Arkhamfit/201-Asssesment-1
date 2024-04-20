using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class represents the PlayerControl in the game
public class PlayerControl : MonoBehaviour
{
    // Singleton instance of PlayerControl
    public static PlayerControl instance;

    // Player's speed and state
    public float speed = 10.0f;
    public bool hasArcaneBolt = false;
    public GameObject arcaneBoltProjectile;
    private Animator animator;
    public int hitpoints = 100;
    private bool isFacingRight = true;
    public Text gameOverText;

    // Player's sword collider
    public Collider2D swordCollider;
    public float knockbackForce = 2.0f;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        // Disable the sword collider initially
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement vector and set the velocity
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;

        // Set the IsWalking animation state
        animator.SetBool("IsWalking", movement.magnitude > 0);

        // Flip the player's direction if necessary
        if ((moveHorizontal > 0 && !isFacingRight) || (moveHorizontal < 0 && isFacingRight))
        {
            Flip();
        }

        // Attack or fire an Arcane Bolt if the Fire1 button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");

            if (hasArcaneBolt)
            {
                // Instantiate an Arcane Bolt and set its direction
                GameObject bolt = Instantiate(arcaneBoltProjectile, transform.position, Quaternion.identity);
                if (isFacingRight)
                {
                    bolt.GetComponent<ArcaneBoltProjectile>().direction = Vector2.right;
                }
                else
                {
                    bolt.GetComponent<ArcaneBoltProjectile>().direction = Vector2.left;
                }
                // Ignore collision between the Arcane Bolt and the player
                Physics2D.IgnoreCollision(bolt.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

        // If the player's hitpoints reach 0, trigger the Death animation and end the game
        if (hitpoints <= 0)
        {
            animator.SetTrigger("Death");
            StartCoroutine(EndGameAfterDelay(1f));
        }
    }

    // Flip the player's direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Enable the sword collider
    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }

    // Disable the sword collider
    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }

    // End the game after a delay
    IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOverText.enabled = true;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}