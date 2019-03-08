using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Stat StrengthStat { get; set; }
    public Stat StaminaStat { get; set; }
    public Stat HealthStat { get; set; }

    // Use this for initialization
    void Start ()
    {
        HealthStat = new Stat(1, "HealthStat");
        StaminaStat = new Stat(1, "StaminaStat");
        StrengthStat = new Stat(1, "StrengthStat");

        GetComponent<MovementComponent>().maxStamina = 90 + (10 * StaminaStat.StatLevel); // base 100
        GetComponent<HealthComponent>().maxHealth = 90 + (10 * HealthStat.StatLevel);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.H))
            UpdateStat(HealthStat);
        if (Input.GetKeyDown(KeyCode.M))
            UpdateStat(StaminaStat);
	}

    void UpdateStat(Stat statToUpgrade)
    {
        int expCost = CalculateExpCost(statToUpgrade);
        Debug.Log("Need " + expCost + " to level up");
        if (expCost <= GetComponent<ExperienceComponent>().totalExp)
        {
            statToUpgrade.StatLevel++;
            GetComponent<ExperienceComponent>().totalExp -= expCost;

            if (statToUpgrade.StatName.Equals("HealthStat"))
            {
                GetComponent<HealthComponent>().maxHealth = 90 + (10 * HealthStat.StatLevel);
                GetComponent<HealthComponent>().health = GetComponent<HealthComponent>().maxHealth; // set health to max
            }
            if (statToUpgrade.StatName.Equals("StaminaStat"))
            {
                GetComponent<MovementComponent>().maxStamina = 90 + (10 * StaminaStat.StatLevel);
                GetComponent<MovementComponent>().stamina = GetComponent<MovementComponent>().maxStamina; //set stamina to max
            }

            Debug.Log("Leveled Up");

        }
    }

    int CalculateExpCost(Stat stat)
    {
        return 500 * (stat.StatLevel * 2);
    }
}
