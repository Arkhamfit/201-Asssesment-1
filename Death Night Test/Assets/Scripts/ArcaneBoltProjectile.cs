using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents the ArcaneBoltProjectile in the game
public class ArcaneBoltProjectile : MonoBehaviour
{
    // Speed of the projectile
    public float speed = 10.0f;
    // Damage dealt by the projectile
    public int damage = 100;
    // Direction of the projectile
    public Vector2 direction;
    // Target of the projectile
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        // Set the velocity of the projectile
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        // Find the closest enemy as the initial target
        target = FindClosestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // If there's no target, find the closest enemy
        if (target == null)
        {
            target = FindClosestEnemy();
        }
        else
        {
            // Update the direction towards the target and set the velocity
            direction = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }

        // Make the Arcane Bolt face its moving direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Triggered when the projectile collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the projectile hits the target
        if (other.gameObject == target)
        {
            // Call the OnHitByPlayer method of the EnemyController script
            target.GetComponent<EnemyController>().OnHitByPlayer();
            // Destroy the Arcane Bolt
            Destroy(gameObject);
        }
    }

    // Find the closest enemy
    public GameObject FindClosestEnemy()
    {
        // Get all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        // search through each enemy to find the closest one
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float distance = diff.sqrMagnitude;
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    // Set a new target for the projectile
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            // Update the direction and velocity towards the new target
            Vector2 direction = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}