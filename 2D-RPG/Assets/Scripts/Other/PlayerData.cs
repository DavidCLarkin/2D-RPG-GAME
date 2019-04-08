using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Data class to hold all necessary variables to save from the players components etc.
[Serializable]
public class PlayerData
{
    public float[] position; // Position the player saved at
    public int maxStamina; // Max stamina level of the player
    public int maxHealth; // Max health level of the player
    public int totalExp; // Total exp of the player
    public int healthLevel; // Level of the player's health stat
    public int staminaLevel; // Level of the player's stamina stat
    public float volumeLevel; // The volume level the player left it at
    public int notesRead; // Number of notes the player read
    public bool hasCompletedTutorial; // Has the player completed the tutorial?

    public int[] itemIDs; // ID's of the item's the player had in their inventory, to add to inventory when loaded

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
        hasCompletedTutorial = GameManagerSingleton.instance.hasCompletedTutorial;

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

