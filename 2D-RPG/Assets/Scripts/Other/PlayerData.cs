using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int maxStamina;
    public int maxHealth;
    public int totalExp;
    public int healthLevel;
    public int staminaLevel;


    public PlayerData(GameObject player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        //maxStamina = player.GetComponent<MovementComponent>().maxStamina;
        //maxHealth = player.GetComponent<HealthComponent>().maxHealth;
        healthLevel = player.GetComponent<Stats>().HealthStat.StatLevel;
        staminaLevel = player.GetComponent<Stats>().StaminaStat.StatLevel;
        totalExp = player.GetComponent<ExperienceComponent>().totalExp;
    }
}

