using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitialSceneLoad : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (File.Exists(Application.persistentDataPath + "/player.bin"))
            SaveSystem.Load();
        else
        {
            Debug.Log("Initial Save");
            SaveSystem.Save(GameManagerSingleton.instance.player.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
