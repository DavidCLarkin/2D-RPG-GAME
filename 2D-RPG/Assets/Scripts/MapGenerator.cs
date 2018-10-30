using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	public GameObject[] groundTiles; // Set in inspector
	public int rows = 50; // Set in inspector
	public int cols = 50; // Set in inspector
	private int[,] grid;
	private Vector2 startPos;
	private Vector2 currentPos;
	private Vector2[] directions = 
	{
		new Vector2(1, 0),
		new Vector2(0, 1), 
		new Vector2(-1, 0), 
		new Vector2(0, -1)
	};
	private Vector2 randDirection;
	private Vector2 lastDirection = new Vector2(0,0);
	public int numOfTunnels = 10; // Set in inspector
	public int maxTunnelLength = 5; // Set in inspector
	public int minTunnelLength = 2; // Set in inspector


	// Use this for initialization
	void Start () 
	{
		grid = new int[rows, cols];
		int startX, startY = 0;
		startX = Random.Range(0, rows);
		startY = Random.Range(0, cols);
		startPos = new Vector2(startX, startY);

		currentPos = startPos;

		for(int count = 0; count < numOfTunnels; count++)
		{
			randDirection = directions[Random.Range(0, directions.Length)];
			//Testing that the direction is not set to go backwards or is the same, more variance if not the same
			while((randDirection.x == -lastDirection.x && randDirection.y == -lastDirection.y) || (randDirection.x == lastDirection.x && randDirection.y == lastDirection.y))
			{
				randDirection = directions[Random.Range(0, directions.Length)];
			}

			int tunnelRandLen = Random.Range(minTunnelLength, maxTunnelLength);
			for(int i = 0; i < tunnelRandLen; i++)
			{
				//Testing if on the edge of the grid
				if((currentPos.x == 0) && (randDirection.x == -1))			{	break;	}
				if((currentPos.x == rows - 1) && (randDirection.x == 1))	{	break;	}
				if((currentPos.y == 0) && (randDirection.y == -1))			{	break;	}
				if((currentPos.y == cols - 1) && (randDirection.y == 1))	{	break;	}

				grid[(int)currentPos.x, (int)currentPos.y] = 1;
				currentPos += randDirection;
				//print(currentPos);
			}
				
			lastDirection = randDirection;
		}
			
		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < cols; j++)
			{
				if(grid[i, j] == 1)
				{
					grid[i, j] = 1;
					Instantiate(groundTiles[0], new Vector2(i*2, j*2), Quaternion.identity); // - new Vector2(i,j) for NOT 2x2
				} 
				else
				{
					grid[i, j] = 0;
				}
			}
		}

		//Checking neighbouring grid spaces to place walls
		for(int x = 0; x < rows; x++)
		{
			for(int y = 0; y < cols; y++)
			{
				if(grid[x, y] == 1)
				{
					//if(i + 1 > rows)
					//	break;
					if(x < rows - 1)
						if(grid[x + 1, y] == 0)
						Instantiate(groundTiles[1], new Vector2((x + 0.71f)*2, (y*2)+0.3f), Quaternion.identity); //Change to Right side Wall - 2x2 :new Vector2((x + 0.71f)*2, (y*2)+0.3f) NORMAL://new Vector2(x+1, y)
					if(y < cols - 1) 
						if(grid[x, y + 1] == 0)
						Instantiate(groundTiles[4], new Vector2((x*2)+0.03f, (y + 1)*2+0.35f), Quaternion.identity); //Change to Top wall - 2x2 : new Vector2((x*2)+0.03f, (y + 1)*2+0.35f) NORMAL://new Vector2(x, y+1)
					if(x > 1) 
						if(grid[x - 1, y] == 0)
						Instantiate(groundTiles[2], new Vector2((x-0.34f)*2, (y*2)+0.28f), Quaternion.identity); //Change to Left Wall - 2x2:  new Vector2((x-0.34f)*2, (y*2)+0.28f) NORMAL://new Vector2(x-1, y)
					if(y > 1)
						if(grid[x, y - 1] == 0)
						Instantiate(groundTiles[3], new Vector2((x*2)-2.44f, (y*2)-0.92f), Quaternion.identity); //Change to bottom wall - 2x2: new Vector2((x*2)-2.44f, (y*2)-0.92f)  NORMAL://new Vector2(x, y-1)
				}
			}
		}
		//lastDirection = directions[0];
		//randDirection = directions[Random.Range(0, directions.Length)];
	}


}
