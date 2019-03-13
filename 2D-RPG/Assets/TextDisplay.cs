using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;

    private HealthComponent playerHealth;
    private MovementComponent playerStamina;

	// Use this for initialization
	void Start ()
    {
        playerHealth = GameManagerSingleton.instance.player.GetComponent<HealthComponent>();
        playerStamina = GameManagerSingleton.instance.player.GetComponent<MovementComponent>();

        healthBar.value = CalculateFillPercentage(playerHealth.health, playerHealth.maxHealth);
        staminaBar.value = CalculateFillPercentage(playerStamina.stamina, playerStamina.maxStamina);
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.value = CalculateFillPercentage(playerHealth.health, playerHealth.maxHealth);
        staminaBar.value = CalculateFillPercentage(playerStamina.stamina, playerStamina.maxStamina);
	}

    float CalculateFillPercentage(float current, float max)
    {
        return current / max;
    }
}
