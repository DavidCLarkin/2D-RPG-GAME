using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour 
{
	public GameObject[] groundTiles; 
	public GameObject[] wallTiles;
	public Room[] rooms;
	public int rows = 10;
	public int cols = 10;
	public int numOfRooms = 5;
	GameObject[,] grid;

	private GameObject dungeon;

	// Use this for initialization
	void Start() 
	{
		dungeon = new GameObject("Dungeon");

		grid = new GameObject[rows, cols];
		float tileWidth = groundTiles[0].GetComponentInChildren<SpriteRenderer>().bounds.size.x;
		float tileHeight = groundTiles[0].GetComponentInChildren<SpriteRenderer>().bounds.size.y;

		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < cols; j++)
			{
				if(j == 0 || j == rows - 1)
				{
					grid[i, j] = (GameObject)Instantiate(wallTiles[1], new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
				}
				if(i == 0 || i == rows - 1)
				{
					//Order in layer higher for walls/fences
					grid[i, j] = (GameObject)Instantiate(wallTiles[0], new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
				} 


				int chosenTile = Random.Range(0, groundTiles.Length);
				grid[i,j] = (GameObject) Instantiate(groundTiles[1], new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
				//setting a random tile for the ground

			}
		}

		AddRooms();

		foreach(Room room in rooms)
		{
			for(int i = room.xPos; i < (room.xPos + room.width); i++)
			{
				for(int j = room.yPos; j < (room.yPos + room.height); j++)
				{
					if(j == room.yPos || j == (room.yPos + room.height) - 1)
					{
						grid[i,j] = Instantiate(wallTiles[1], new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
					}
					if(i == room.xPos || i == (room.xPos + room.width) - 1)
					{
						//Order in layer higher for walls/fences
						grid[i,j] = Instantiate(wallTiles[0], new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
					}

					grid[i,j] = Instantiate(groundTiles[0], new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
				}
			}
		}
		/*
		Room shortestDistRoom;
		for(int i = 0; i < rooms.Length; i++)
		{
			for(int j = i+1; j < rooms.Length; j++)
			{
				print("Room i : " + rooms[i]);
				print("Room j : " + rooms[j]);
			}
				
		}
		*/




	}

	public void AddRooms()
	{
		rooms = new Room[numOfRooms];
		for(int i = 0; i < rooms.Length; i++)
		{
			rooms[i] = new Room();
			rooms[i].AddCorrider(Random.Range(1,4));
			rooms[i].SetupRoom(Random.Range(6,10), Random.Range(6,10), cols, rows, wallTiles, groundTiles);
			/*foreach(Room room in rooms)
			{
				if(rooms[i].xPos == room.xPos)
				{
					
				}
			}
			*/
		}
	}

	// Update is called once per frame
	void Update() 
	{
		
	}
}
