using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public List<Transform> tiles;
    private GameObject boss;

    // Add each tile to the list - used in boss script 
	void Start ()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Ground")
            {
                foreach(Transform groundTile in child)
                    tiles.Add(groundTile);
            }
        }

        //boss = GameObject.FindGameObjectWithTag("Boss");
        //boss.GetComponent<HealthComponent>().EnableSpawnRoom += EnableRoom; //delegated to check if boss is dead
    }


    /*
	 * Method to enable the last tile within a Room, which for my design will be the room to exit the dungeon
	 */
    public void EnableRoom()
    {
        tiles[tiles.Count - 1].gameObject.SetActive(true);
    }
}
