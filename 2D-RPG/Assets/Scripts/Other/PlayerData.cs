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
    public float volumeLevel;
    public int notesRead;

    public int[] itemIDs;

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
        volumeLevel = GameManagerSingleton.instance.GetComponent<MenuButtonFunctions>().volumeSlider.value;
        notesRead = GameManagerSingleton.instance.GetComponent<NoteHandler>().numberOfNotesFound;

        itemIDs = new int[4];
        for(int i = 0; i < itemIDs.Length; i++)
        {
            if (player.GetComponent<Inventory>().itemSlots[i].itemID != 0)
                itemIDs[i] = player.GetComponent<Inventory>().itemSlots[i].itemID;
            else
                itemIDs[i] = 0;
        }

    }
}

