using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class represents the HealthBar in the game
public class HealthBar : MonoBehaviour
{
    // Reference to the PlayerControl script
    public PlayerControl player;
    // UI Text component to display the health, assigned in the inspector
    public Text healthText;
    // Image component representing the health bar
    private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Image component from the current game object
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the fill amount of the health bar based on the player's hitpoints
        healthBar.fillAmount = (float)player.hitpoints / 100; // Assuming the player has 100 max hitpoints
        // Update the health text to display the current and max hitpoints
        healthText.text = player.hitpoints.ToString() + "/100";
    }
}