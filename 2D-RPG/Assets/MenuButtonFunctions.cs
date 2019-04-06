using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider volumeSlider;

    private GameObject player;

    private void Start()
    {
        player = GameManagerSingleton.instance.player;
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("masterVolume", volume);
        if (volume <= -20)
            mixer.SetFloat("masterVolume", -80f); // mute
    }

    public void Save()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
            SaveSystem.Save(player);
        else
            Debug.Log("Can only save in Hub");
    }

    public void Load()
    {
        if (SceneManager.GetActiveScene().name != "Hub") // Load back to hub
            SceneManager.LoadScene("Hub");

        PlayerData data = SaveSystem.Load();

        player.GetComponent<Stats>().HealthStat.StatLevel = data.healthLevel;
        player.GetComponent<Stats>().StaminaStat.StatLevel = data.staminaLevel;
        player.GetComponent<ExperienceComponent>().totalExp = data.totalExp;
        GameManagerSingleton.instance.GetComponent<NoteHandler>().numberOfNotesFound = data.notesRead;

        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        for (int i = 0; i < data.itemIDs.Length; i++)
        {
            if (data.itemIDs[i] != 0) // not an empty slot
            {
                foreach (GameObject item in GameManagerSingleton.instance.GetComponent<ItemDatabase>().items)
                {
                    Item dbItem = item.GetComponent<Item>();
                    if (data.itemIDs[i] == (dbItem.itemID))
                    {
                        Slot s = player.GetComponent<Inventory>().itemSlots[i];
                        s.item = dbItem.GetComponent<Item>();
                        s.icon = dbItem.icon;
                        s.isEmpty = false;
                        s.itemName = dbItem.itemName;
                        s.description = dbItem.description;
                        s.itemID = dbItem.itemID;

                    }
                }
            }
        }

        SetVolume(data.volumeLevel); // set volume that player chose
        volumeSlider.value = data.volumeLevel;

        player.GetComponent<Stats>().UpdateVariables(false);
        // Make sure player loads with max stats
        player.GetComponent<HealthComponent>().health = player.GetComponent<HealthComponent>().maxHealth;
        player.GetComponent<MovementComponent>().stamina = player.GetComponent<StaminaComponent>().maxStamina;

        // Destroy any objects that are on the ground when loading
        GameObject[] inactiveObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject i in inactiveObjects)
        {
            if (i.activeSelf)
                Destroy(i);
        }


        foreach (Slot s in player.GetComponent<Inventory>().itemSlots)
        {
            if (s.item == null)
            {
                Debug.Log("Item Slot Empty");
                s.RemoveItemCompletely();
            }
        }

        //GameManagerSingleton.instance.isPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        GameManagerSingleton.instance.OpenControlsPanel();
    }
}
