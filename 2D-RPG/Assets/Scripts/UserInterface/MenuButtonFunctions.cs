using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtonFunctions : MonoBehaviour
{
    public AudioMixer mixer;
    public TextMeshProUGUI saveInfo;
    public Slider volumeSlider;

    private float saveInfoLength = 3f;
    private float saveInfoTimer = 0f;

    private void Update()
    {
        HandleTextTimer();
    }

    private void HandleTextTimer()
    {
        if (saveInfoTimer > 0)
            saveInfoTimer -= Time.unscaledDeltaTime;
        else if (saveInfoTimer <= 0)
            saveInfo.gameObject.SetActive(false);
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
            SaveSystem.Save(GameManagerSingleton.instance.player);
        else
        {
            saveInfo.gameObject.SetActive(true);
            saveInfoTimer = saveInfoLength;
        }
    }

    public void Load()
    {
        if (SceneManager.GetActiveScene().name != "Hub") // Load back to hub
            SceneManager.LoadScene("Hub");

        PlayerData data = SaveSystem.Load();

        GameManagerSingleton.instance.player.GetComponent<Stats>().HealthStat.StatLevel = data.healthLevel;
        GameManagerSingleton.instance.player.GetComponent<Stats>().StaminaStat.StatLevel = data.staminaLevel;
        GameManagerSingleton.instance.player.GetComponent<ExperienceComponent>().totalExp = data.totalExp;
        GameManagerSingleton.instance.GetComponent<NoteHandler>().numberOfNotesFound = data.notesRead;
        GameManagerSingleton.instance.hasCompletedTutorial = data.hasCompletedTutorial;

        GameManagerSingleton.instance.player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        for (int i = 0; i < data.itemIDs.Length; i++)
        {
            if (data.itemIDs[i] != 0) // not an empty slot
            {
                foreach (GameObject item in GameManagerSingleton.instance.GetComponent<ItemDatabase>().items)
                {
                    Item dbItem = item.GetComponent<Item>();
                    if (data.itemIDs[i] == (dbItem.itemID))
                    {
                        Slot s = GameManagerSingleton.instance.player.GetComponent<Inventory>().itemSlots[i];
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

        GameManagerSingleton.instance.player.GetComponent<Stats>().UpdateVariables(false);
        // Make sure player loads with max stats
        GameManagerSingleton.instance.player.GetComponent<HealthComponent>().health = GameManagerSingleton.instance.player.GetComponent<HealthComponent>().maxHealth;
        GameManagerSingleton.instance.player.GetComponent<MovementComponent>().stamina = GameManagerSingleton.instance.player.GetComponent<MovementComponent>().maxStamina;

        // Destroy any objects that are on the ground when loading
        GameObject[] inactiveObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject i in inactiveObjects)
        {
            if (i.activeSelf)
                Destroy(i);
        }


        foreach (Slot slot in GameManagerSingleton.instance.player.GetComponent<Inventory>().itemSlots)
        {
            if (slot.item == null)
            {
                Debug.Log("Item Slot Empty");
                slot.RemoveItemCompletely();
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
