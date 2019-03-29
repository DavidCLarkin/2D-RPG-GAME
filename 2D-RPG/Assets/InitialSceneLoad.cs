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

        
	}
	
}
