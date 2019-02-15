using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSpawns : MonoBehaviour 
{

	public List<Transform> spawnPoints;
	public List<GameObject> objects;

	void Start () 
	{
		foreach (Transform child in transform)
		{
			if (child.name == "SpawnPoints")
			{
				foreach(Transform spawnPoint in child)
					spawnPoints.Add(spawnPoint);
			}
		}

		SpawnObjects();
	}

	void SpawnObjects()
	{
		int numObjToSpawn = Random.Range (1, 4);
		Debug.Log (numObjToSpawn);
		for (int i = 0; i < numObjToSpawn; i++) 
		{
			int chosenPoint = Random.Range (0, spawnPoints.Count); // save this to remove it from the list later
			Instantiate (objects[Random.Range (0, objects.Count)], spawnPoints[chosenPoint].transform.position, Quaternion.identity);
			spawnPoints.Remove (spawnPoints [chosenPoint]);
		}

	}

}
