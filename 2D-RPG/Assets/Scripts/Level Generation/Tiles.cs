using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public List<Transform> tiles;

	void Start ()
    {
        GameObject.FindGameObjectWithTag("Boss").GetComponent<HealthComponent>().EnableSpawnRoom += EnableRoom; //delegated to check if boss is dead

        foreach (Transform child in transform)
        {
            if (child.name == "Ground")
            {
                foreach(Transform groundTile in child)
                    tiles.Add(groundTile);
            }
        }
    }

	/*
	 * Method to enable the last tile within a Room, which for my design will be the room to exit the dungeon
	 */
    void EnableRoom()
    {
        tiles[tiles.Count - 1].gameObject.SetActive(true);
    }
}
