using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitialSceneLoad : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GameManagerSingleton.instance.statVendor = GameObject.Find("Stat Vendor NPC").GetComponent<StatVendor>();
        if (File.Exists(Application.persistentDataPath + "/player.bin"))
            SaveSystem.Load();
        else
        {
            Debug.Log("Initial Save");
            SaveSystem.Save(GameManagerSingleton.instance.player.gameObject);
        }

        
        // Remove any stray items that appera to be in inventory
        foreach(Slot s in Inventory.instance.itemSlots)
        {
            if(s.item == null)
            {
                s.RemoveItemCompletely();
            }
        }

        GameManagerSingleton.instance.player.GetComponent<HealthComponent>().health = GameManagerSingleton.instance.player.GetComponent<HealthComponent>().maxHealth;
        GameManagerSingleton.instance.player.GetComponent<MovementComponent>().stamina = GameManagerSingleton.instance.player.GetComponent<MovementComponent>().maxStamina;




    }

}
