using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Stat StrengthStat { get; set; }
    public Stat StaminaStat { get; set; }
    public Stat HealthStat { get; set; }

    private HealthComponent health;
    private MovementComponent stamina;
    private ExperienceComponent exp;

    public Text currentHealthUIDisplay;
    public Text newHealthUIDisplay;
    public Text currentStaminaUIDisplay;
    public Text newStaminaUIDisplay;
    public Text currentExpUIDisplay;
    public Text healthUpgradeCost;
    public Text staminaUpgradeCost;

    // Use this for initialization
    void Start ()
    {
        HealthStat = new Stat(1, "HealthStat");
        StaminaStat = new Stat(1, "StaminaStat");
        StrengthStat = new Stat(1, "StrengthStat");

        health = GetComponent<HealthComponent>();
        stamina = GetComponent<MovementComponent>();
        exp = GetComponent<ExperienceComponent>();

        stamina.maxStamina = 90 + (10 * StaminaStat.StatLevel); // base 100
        health.maxHealth = 90 + (10 * HealthStat.StatLevel);

        currentHealthUIDisplay.text = (health.maxHealth).ToString();
        newHealthUIDisplay.text = (health.maxHealth + 10).ToString();
        currentStaminaUIDisplay.text = (stamina.maxStamina).ToString();
        newStaminaUIDisplay.text = (stamina.maxStamina + 10).ToString();

        StartCoroutine(UpdateUI());

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.H))
            UpdateStat(HealthStat);
        if (Input.GetKeyDown(KeyCode.M))
            UpdateStat(StaminaStat);

	}

    public void UpgradeHealth()
    {
        int expCost = CalculateExpCost(HealthStat);
        if (expCost <= exp.totalExp)
        {
            HealthStat.StatLevel++;
            exp.totalExp -= expCost;

            health.maxHealth = 90 + (10 * HealthStat.StatLevel);
            health.health = health.maxHealth; // set health to max
            currentHealthUIDisplay.text = (health.maxHealth).ToString();
            newHealthUIDisplay.text = (health.maxHealth + 10).ToString();

            Debug.Log("Leveled up");
        }
    }

    public void UpgradeStamina()
    {
        int expCost = CalculateExpCost(StaminaStat);
        if (expCost <= exp.totalExp)
        {
            StaminaStat.StatLevel++;
            exp.totalExp -= expCost;

            stamina.maxStamina = 90 + (10 * StaminaStat.StatLevel);
            stamina.Stamina = stamina.maxStamina; // set health to max
            currentStaminaUIDisplay.text = (stamina.maxStamina).ToString();
            newStaminaUIDisplay.text = (stamina.maxStamina + 10).ToString();

            Debug.Log("Leveled up");
        }
    }

    public void UpdateStat(Stat statToUpgrade)
    {
        int expCost = CalculateExpCost(statToUpgrade);
        Debug.Log("Need " + expCost + " to level up");
        if (expCost <= exp.totalExp)
        {
            statToUpgrade.StatLevel++;
            exp.totalExp -= expCost;

            if (statToUpgrade.StatName.Equals("HealthStat"))
            {
                health.maxHealth = 90 + (10 * HealthStat.StatLevel);
                health.health = health.maxHealth; // set health to max
                currentHealthUIDisplay.text = (health.maxHealth).ToString();
                newHealthUIDisplay.text = (health.maxHealth + 10).ToString();
            }
            if (statToUpgrade.StatName.Equals("StaminaStat"))
            {
                stamina.maxStamina = 90 + (10 * StaminaStat.StatLevel);
                stamina.Stamina = stamina.maxStamina; //set stamina to max
            }

            Debug.Log("Leveled Up");

        }
    }


    int CalculateExpCost(Stat stat)
    {
        return 500 * (stat.StatLevel * 2);
    }

    // Coroutine to update the UI every 1/10th second instead of every frame
    IEnumerator UpdateUI()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            int healthCost = CalculateExpCost(HealthStat);
            int staminaCost = CalculateExpCost(StaminaStat);

            currentExpUIDisplay.text = (exp.totalExp).ToString();
            healthUpgradeCost.text = healthCost.ToString();
            staminaUpgradeCost.text = staminaCost.ToString();

            // Set color of text to indicate whether stat can be upgraded or not
            if (exp.totalExp < healthCost)
                healthUpgradeCost.color = Color.red;
            else
                healthUpgradeCost.color = Color.green;

            if (exp.totalExp < staminaCost)
                staminaUpgradeCost.color = Color.red;
            else
                staminaUpgradeCost.color = Color.green;
        }
    }
}
