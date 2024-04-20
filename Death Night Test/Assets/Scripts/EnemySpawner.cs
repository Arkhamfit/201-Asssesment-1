using System.Collections;
using UnityEngine;

// This class manages the spawning of skeletons in the game
public class SpawnManager : MonoBehaviour
{
    // The skeleton prefab to be spawned, assigned in the inspector
    public GameObject skeletonPrefab;
    // The delay before the first spawn
    private float startDelay = 2;
    // The interval between spawning single skeletons
    private float spawnInterval = 1.5f;
    // The interval between spawning waves of skeletons
    private float waveInterval = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning single skeletons and waves of skeletons at regular intervals
        InvokeRepeating("SpawnSkeleton", startDelay, spawnInterval); // Single skeletons
        InvokeRepeating("SpawnSkeletons", startDelay, waveInterval); // Waves of skeletons
    }

    
    void Update()
    {
        // Any updates to the spawn manager can go here
    }

    // Method for spawning a single skeleton
    void SpawnSkeleton()
    {
        // Set the dimensions of the background
        float backgroundWidth = 23;
        float backgroundHeight = 50;

        // Calculate the spawn position within the background dimensions
        Vector3 spawnPos = new Vector3(Random.Range(-backgroundWidth / 2, backgroundWidth / 2), Random.Range(-backgroundHeight / 2, backgroundHeight / 2), 0);

        // Instantiate a new skeleton at the spawn position
        Instantiate(skeletonPrefab, spawnPos, skeletonPrefab.transform.rotation);
    }

    // Method for spawning a wave of skeletons
    void SpawnSkeletons()
    {
        // Set the dimensions of the background
        float backgroundWidth = 23;
        float backgroundHeight = 50;

        // Loop to spawn 50 skeletons
        for (int i = 0; i < 50; i++)
        {
            // Calculate the spawn position within the background dimensions
            Vector3 spawnPos = new Vector3(Random.Range(-backgroundWidth / 2, backgroundWidth / 2), Random.Range(-backgroundHeight / 2, backgroundHeight / 2), 0);

            // Instantiate a new skeleton at the spawn position
            Instantiate(skeletonPrefab, spawnPos, skeletonPrefab.transform.rotation);
        }
    }
}