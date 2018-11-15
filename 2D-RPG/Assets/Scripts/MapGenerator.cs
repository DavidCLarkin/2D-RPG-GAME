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
    private Vector2 endPos;
	private Vector2[] directions = 
	{
		new Vector2(1, 0),
		new Vector2(0, 1), 
		new Vector2(-1, 0), 
		new Vector2(0, -1)
	};

    [SerializeField]
    private GameObject[] bossRooms; // Rooms to choose a boss room from

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

        for (int count = 0; count < numOfTunnels; count++)
        {
            randDirection = directions[Random.Range(0, directions.Length)];
            //Testing that the direction is not set to go backwards or is the same, more variance if not the same
            while ((randDirection.x == -lastDirection.x && randDirection.y == -lastDirection.y) || (randDirection.x == lastDirection.x && randDirection.y == lastDirection.y))
            {
                randDirection = directions[Random.Range(0, directions.Length)];
            }

            int tunnelRandLen = Random.Range(minTunnelLength, maxTunnelLength);
            for (int i = 0; i < tunnelRandLen; i++)
            {
                //Testing if on the edge of the grid
                if ((currentPos.x == 0) && (randDirection.x == -1)) { break; }
                if ((currentPos.x == rows - 1) && (randDirection.x == 1)) { break; }
                if ((currentPos.y == 0) && (randDirection.y == -1)) { break; }
                if ((currentPos.y == cols - 1) && (randDirection.y == 1)) { break; }

                grid[(int)currentPos.x, (int)currentPos.y] = 1;
                currentPos += randDirection;
                //print(currentPos);
            }

            /*if (count == numOfTunnels)
            {
                Instantiate(bossRooms[0], new Vector2(currentPos.x*2, currentPos.y*2), Quaternion.identity);
            }
            */

            lastDirection = randDirection;
        }

        RemoveSingleWalls();
        RemoveDoubleWalls();

        int test = 10;
        /*randDirection = directions[Random.Range(0, directions.Length)];
        while (grid[(int)currentPos.x + (int)randDirection.x * test, (int)currentPos.y + (int)randDirection.y * test] == 1)
        {
            randDirection = directions[Random.Range(0, directions.Length)];
            test++;
        }
        */

        // TODO: WORKS SMOETIMES, but obviously sometimes cant get a spot to place, so i need to make it so it chooses a differnt position to try available slots in
        //Debug.Log(test);
        Vector2 savedPos = currentPos;
        currentPos += randDirection;
        bool goodPath = false;
        bool hasRoom = false;
        do
        {
            for (int i = 0; i < test; i++)
            {
                if (currentPos.x > rows - 1 || currentPos.x <= 0 || currentPos.y > cols - 1 || currentPos.y <= 0)
                {
                    Debug.Log("Out of bounds, changing currentpost to saved Pos: curPos: " + currentPos + "savedPos: " + savedPos);
                    currentPos = savedPos;
                    break;
                }

                if (grid[(int)currentPos.x, (int)currentPos.y] != 1)
                {
                    Instantiate(groundTiles[0], currentPos, Quaternion.identity);
                    currentPos += randDirection;
                    if (i == test - 1)
                    {
                        endPos = currentPos;
                        for (int j = (int)endPos.x; j < endPos.x + 8; j++) // 8 instead of 7 for more room
                        {
                            for (int k = (int)endPos.y; k < endPos.y + 5; k++) // 5 instead of 4 more room
                            {
                                // Debug.Log("i: " + i + ",j: " + j);
                                if (grid[j, k] == 1)
                                {
                                    continue;
                                    //endPos += directions[Random.Range(0, directions.Length)];
                                }
                            }
                        }
                    }
                }
                else
                {
                    randDirection = directions[Random.Range(0, directions.Length)];
                    currentPos += randDirection;
                    continue;
                }
            }
            goodPath = true;
            hasRoom = true;
        } while (!goodPath && !hasRoom);


        /*
        endPos = currentPos;
        //Debug.Log("Last Direction: " + lastDirection);
        Debug.Log("Last Pos: " +endPos);
        bool hasRoom = true;
        //TODO sometimes doesnt even instantiate any dungeon, but otherwise works pretty well
        // bossRooms[0].GetComponent<Collider2D>().bounds.size.y - Used to calculate size of room
        for (int i = (int)endPos.x; i < endPos.x + 8; i++) // 8 instead of 7 for more room
        {
            for (int j = (int)endPos.y; j < endPos.y + 5; j++) // 5 instead of 4 more room
            {
               // Debug.Log("i: " + i + ",j: " + j);
                if (grid[i, j] == 1)
                {
                    //randDirection = directions[Random.Range(0, directions.Length)];
                    //endPos += randDirection;
                    hasRoom = false;
                }
            }
        }
        */

        if (hasRoom)
            Instantiate(bossRooms[0], endPos, Quaternion.identity);
        else
            Debug.Log("No room");
        //Instantiate(bossRooms[0], new Vector2(endPos.x + bossRooms[0].GetComponent<Collider2D>().bounds.size.x/2, endPos.y + endPos.y + bossRooms[0].GetComponent<Collider2D>().bounds.size.y/2), Quaternion.identity);


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == 1)
                {
                    grid[i, j] = 1;
                    Instantiate(groundTiles[0], new Vector2(i, j), Quaternion.identity); // - new Vector2(i,j) for NOT 2x2 -- new Vector2(i *2, j*2) for 2x2
                }
                else
                {
                    grid[i, j] = 0;
                }
            }
        }

        //Checking neighbouring grid spaces to place walls
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (grid[x, y] == 1)
                {
                    //if(i + 1 > rows)
                    //	break;
                    if (x < rows - 1)
                        if (grid[x + 1, y] == 0)
                            Instantiate(groundTiles[1], new Vector2(x + 0.81f, y), Quaternion.identity); //Change to Right side Wall - 2x2 :new Vector2((x + 0.71f)*2, (y*2)+0.3f) NORMAL://new Vector2(x+1, y)
                    if (y < cols - 1)
                        if (grid[x, y + 1] == 0)
                            Instantiate(groundTiles[4], new Vector2(x, y + 1), Quaternion.identity); //Change to Top wall - 2x2 : new Vector2((x*2)+0.03f, (y + 1)*2+0.35f) NORMAL://new Vector2(x, y+1)
                    if (x > 1)
                        if (grid[x - 1, y] == 0)
                            Instantiate(groundTiles[2], new Vector2(x - 0.81f, y), Quaternion.identity); //Change to Left Wall - 2x2:  new Vector2((x-0.34f)*2, (y*2)+0.28f) NORMAL://new Vector2(x-1, y)
                    if (y > 1)
                        if (grid[x, y - 1] == 0)
                            Instantiate(groundTiles[3], new Vector2(x, y - 0.3f), Quaternion.identity); //Change to bottom wall - 2x2: new Vector2((x*2)-2.44f, (y*2)-0.92f)  NORMAL://new Vector2(x, y-1)
                }
            }
        }
        //lastDirection = directions[0];
        //randDirection = directions[Random.Range(0, directions.Length)];
    }

    private void RemoveSingleWalls()
    {
        for (int x = 0; x < rows - 1; x++)
        {
            for (int y = 0; y < cols - 1; y++)
            {
                if (grid[x, y] == 0)
                {
                    if (x + 1 > rows || x - 1 < 0 || y + 1 > cols || y - 1 < 0)
                        continue;

                    if (grid[x + 1, y] == 1 && grid[x, y + 1] == 1 && grid[x - 1, y] == 1 && grid[x, y - 1] == 1)
                    {
                        //Debug.Log("Filled in" + x + ", " + y);
                        grid[x, y] = 1;
                    }
                }
            }
        }
    }

    private void RemoveDoubleWalls()
    {
        for (int x = 0; x < rows - 2; x++)
        {
            for (int y = 0; y < cols - 2; y++)
            {
                if (grid[x, y] == 1)
                {
                    if (x + 3 >= rows || x - 3 <= 0 || y + 3 >= cols || y - 3 <= 0)
                        continue;

                    // sideways double walls (Checking if a tile is not a ground, then if tiles around it ARE floor, then fill it in)
                    if ((grid[x + 1, y] == 0 && grid[x + 1, y + 1] == 1 && grid[x + 1, y-1]==1) && (grid[x+2, y] == 0 && grid[x+2, y+1] == 1 && grid[x+2, y-1] == 1 && grid[x+3, y]==1))
                    {
                        //Debug.Log("Filled in double walls : " + x + ", " + y);
                        //Instantiate(groundTiles[5], new Vector2(x + 1, y), Quaternion.identity);
                        //Instantiate(groundTiles[5], new Vector2(x + 2, y), Quaternion.identity);
                        grid[x + 1, y] = 1;
                        grid[x + 2, y] = 1;
                    }
                    // upwards/downwards double walls (Same as above)
                    if((grid[x, y+1] == 0 && grid[x+1, y+1] == 1 && grid[x - 1, y + 1] == 1) && (grid[x, y+2] == 0 && grid[x+1, y+2] == 1 && grid[x-1, y+2] == 1 && grid[x, y+3]== 1))
                    {
                        //Instantiate(groundTiles[5], new Vector2(x, y + 1), Quaternion.identity);
                        //Instantiate(groundTiles[5], new Vector2(x, y + 2), Quaternion.identity);
                        grid[x, y + 1] = 1;
                        grid[x, y + 2] = 1;
                    }
                }
            }
        }
    }
}
