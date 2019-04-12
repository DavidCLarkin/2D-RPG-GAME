using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextDisplay : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public Slider bossHealthBar;
    public Text bossName;
    public Text healthInfo;
    public Text staminaInfo;

    private HealthComponent playerHealth;
    private MovementComponent playerStamina;

    [HideInInspector]
    public GameObject boss;
    [HideInInspector]
    public HealthComponent bossHealth; // Set in Boss Room boss selection

	// Use this for initialization
	void Start ()
    {
        playerHealth = GameManagerSingleton.instance.player.GetComponent<HealthComponent>();
        playerStamina = GameManagerSingleton.instance.player.GetComponent<MovementComponent>();

        healthBar.value = CalculateFillPercentage(playerHealth.health, playerHealth.maxHealth);
        staminaBar.value = CalculateFillPercentage(playerStamina.Stamina, playerStamina.maxStamina);

        bossHealthBar.gameObject.SetActive(false);

        UpdateHealthAndStaminaInfo();

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetBarValues();
        UpdateHealthAndStaminaInfo();
	}

    private void UpdateHealthAndStaminaInfo()
    {
        healthInfo.text = playerHealth.health + "/" + playerHealth.maxHealth;
        staminaInfo.text = playerStamina.stamina + "/" + playerStamina.maxStamina;
    }

    void SetBarValues()
    {
        healthBar.value = CalculateFillPercentage(playerHealth.health, playerHealth.maxHealth);
        staminaBar.value = CalculateFillPercentage(playerStamina.Stamina, playerStamina.maxStamina);

        // Handling up boss health bar
        if (boss)
        {
            if (Vector2.Distance(boss.transform.position, GameManagerSingleton.instance.player.transform.position) < 12f)
            {
                bossHealthBar.gameObject.SetActive(true);
            }
            else
                bossHealthBar.gameObject.SetActive(false);

            if (bossHealthBar.IsActive())
                bossHealthBar.value = CalculateFillPercentage(bossHealth.health, bossHealth.maxHealth);
        }
        else
            bossHealthBar.gameObject.SetActive(false);


    }

    float CalculateFillPercentage(float current, float max)
    {
        return current / max;
    }
}
