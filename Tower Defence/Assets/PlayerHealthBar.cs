using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Player player; // Reference to the Player script
    public Slider healthSlider; // Reference to the UI Slider component

    void Start()
    {
        // Initialize the health bar
        InitializeHealthBar();
    }

    void InitializeHealthBar()
    {
        // Set the maximum value of the slider to the player's max health
        healthSlider.maxValue = player.maxHealth;
        // Set the initial value of the slider to the player's current health
        healthSlider.value = player.currentHealth;

        // Subscribe to the OnHealthChanged event to update the health bar
        player.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(int newHealth)
    {
        // Update the value of the slider when the player's health changes
        healthSlider.value = newHealth;
    }
}
