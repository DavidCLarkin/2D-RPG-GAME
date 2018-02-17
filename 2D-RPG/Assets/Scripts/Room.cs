using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
	public int width;
	public int height;
	public int xPos;
	public int yPos;

	public enum Direction
	{
		North, East, South, West
	}

	//public GameObject[,] roomCoords;


	public void SetupRoom(int roomWidth, int roomHeight, int cols, int rows, GameObject[] groundTiles)
	{
		width = roomWidth;
		height = roomHeight;

		//roomCoords = new GameObject[(xPos +roomWidth), (yPos + roomHeight)];

		xPos = Random.Range(width, rows - width);
		yPos = Random.Range(height, cols - height);
	}

	public void AddCorrider(int randDir)
	{
		Direction direction = (Direction)randDir;
		Debug.Log(direction);

		switch(direction)
		{
			case Direction.North:
				Debug.Log("Go North");
				break;
			case Direction.East:
				Debug.Log("Go East");
				break;
			case Direction.South:
				Debug.Log("Go South");
				break;
			case Direction.West:
				Debug.Log("Go West");
				break;

		}
	}
}
