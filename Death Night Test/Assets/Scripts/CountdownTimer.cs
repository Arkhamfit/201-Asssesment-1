using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This class represents a countdown timer
public class CountdownTimer : MonoBehaviour
{
    // Time left for the countdown, initially set to 2 minutes
    public float timeLeft = 120.0f;
    // UI Text element to display the countdown, assigned in the inspector
    public Text countdownText;


    void Update()
    {
        // If there's still time left
        if (timeLeft > 0)
        {
            // Decrease the time left by the time passed since the last frame
            timeLeft -= Time.deltaTime;
            // Update the countdown text to the rounded down time left
            countdownText.text = Mathf.Round(timeLeft).ToString();
        }
        else
        {
            // If the time is up, display "You Won!" and stop the game
            countdownText.text = "You Won!";
            Time.timeScale = 0; // This stops the game
        }
    }
}