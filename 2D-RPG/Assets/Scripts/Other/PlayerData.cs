using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public float[] position;
    public int maxStamina;
    public int maxHealth;
    public int totalExp;
    public int healthLevel;
    public int staminaLevel;

    public string[] itemNames;

    // Save player data to object to serialize
    public PlayerData(GameObject player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        healthLevel = player.GetComponent<Stats>().HealthStat.StatLevel;
        staminaLevel = player.GetComponent<Stats>().StaminaStat.StatLevel;
        totalExp = player.GetComponent<ExperienceComponent>().totalExp;

        itemNames = new string[4];
        for(int i = 0; i < itemNames.Length; i++)
        {
            if (!player.GetComponent<Inventory>().itemSlots[i].itemName.Equals(""))
                itemNames[i] = player.GetComponent<Inventory>().itemSlots[i].itemName;
            else
                itemNames[i] = "";
        }

    }
}

