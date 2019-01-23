using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Vector2 mapSize = new Vector2(100, 100);
    public int numbOfRooms = 30;
    public Vector2 finalRoomPos; // keep final room as it's going to be the boss room spawn location

    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY;

    public GameObject roomR, roomL, roomU, roomD, roomDL, roomDR, roomDLR, roomLR, roomUD, roomUDL, roomUDLR, roomUDR, roomUL, roomULR, roomUR;
    public GameObject[] bossRooms;

	// Use this for initialization
	void Start ()
    {
		if(numbOfRooms >= (mapSize.x * 2) * (mapSize.y * 2))
        {
            numbOfRooms = Mathf.RoundToInt((mapSize.x * 2) * (mapSize.y * 2));
        }

        gridSizeX = Mathf.RoundToInt(mapSize.x);
        gridSizeY = Mathf.RoundToInt(mapSize.y);

        CreateRooms();
        SetRoomDoors();
        DrawMap();
	}

    void CreateRooms()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(new Vector2(0,0), 1); // first room set to center
        takenPositions.Insert(0, new Vector2(0,0));
        Vector2 checkPos = new Vector2(0,0);

        //magic numb
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        //add rooms
        for (int i = 0; i < numbOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / (((float)numbOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            //grab new pos
            checkPos = NewPosition();

            //test new pos
            if(NumberOfNeighbours(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbours(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("couldn't create fewer neighbours than :" + NumberOfNeighbours(checkPos, takenPositions));
            }

            //finalise pos
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);

            if (i == numbOfRooms - 2)
                finalRoomPos = checkPos;

            //Debug.Log(finalRoomPos);
        }

    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = new Vector2(0,0);

        do
        {
            int index = Random.Range(0, takenPositions.Count - 1);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            bool UpOrDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpOrDown)
            {
                if (positive)
                    y += 1;
                else
                    y -= 1;
            }
            else
            {
                if (positive)
                    x += 1;
                else
                    x -= 1;
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = new Vector2(0,0);

        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY)
        {
            inc = 0;
            while(NumberOfNeighbours(takenPositions[index], takenPositions) > 1 && inc < 100)
            {
                index = Random.Range(0, takenPositions.Count - 1);
                inc++;
            }
            
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            bool UpOrDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpOrDown)
            {
                if (positive)
                    y += 1;
                else
                    y -= 1;
            }
            else
            {
                if (positive)
                    x += 1;
                else
                    x -= 1;
            }
            checkingPos = new Vector2(x, y);
        }

        return checkingPos;
    }

    // Helper to get the number of rooms beside a position
    int NumberOfNeighbours(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int numbOfNeighbours = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
            numbOfNeighbours++;
        if (usedPositions.Contains(checkingPos + Vector2.left))
            numbOfNeighbours++;
        if (usedPositions.Contains(checkingPos + Vector2.up))
            numbOfNeighbours++;
        if (usedPositions.Contains(checkingPos + Vector2.down))
            numbOfNeighbours++;

        return numbOfNeighbours;
    }

    void SetRoomDoors()
    {
        for(int x = 0; x < gridSizeX * 2; x++)
        {
            for (int y = 0; y < gridSizeY * 2; y++)
            {
                if (rooms[x, y] == null)
                    continue;

                //Check above
                if (y - 1 < 0)
                    rooms[x, y].doorBottom = false;
                else
                    rooms[x, y].doorBottom = (rooms[x, y - 1] != null);

                //Check below
                if (y + 1 >= gridSizeY * 2)
                    rooms[x, y].doorTop = false;
                else
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);

                //check left
                if (x - 1 < 0)
                    rooms[x, y].doorLeft = false;
                else
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);

                //check right
                if (x + 1 >= gridSizeX * 2)
                    rooms[x, y].doorRight = false;
                else
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
            }
        }
    }

    void DrawMap()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
                continue;

            Vector2 drawPos = room.position;
            drawPos.x *= 20;
            drawPos.y *= 20;

            //Spawn boss room at the end
            Debug.Log(room.position);
            if (room.position == finalRoomPos)
            {
                Instantiate(bossRooms[Random.Range(0, bossRooms.Length)], drawPos, Quaternion.identity);
                continue;
            }

            // Basically checking which specific room to place - not complicated, just long
            if(room.doorTop)
            {
                if(room.doorBottom)
                {
                    if (room.doorRight)
                    {
                        if (room.doorLeft)
                            Instantiate(roomUDLR, drawPos, Quaternion.identity);
                        else
                            Instantiate(roomUDR, drawPos, Quaternion.identity);
                    }
                    else if (room.doorLeft)
                    {
                        Instantiate(roomUDL, drawPos, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(roomUD, drawPos, Quaternion.identity);
                    }
                }
                else
                {
                    if (room.doorRight)
                    {
                        if (room.doorLeft)
                            Instantiate(roomULR, drawPos, Quaternion.identity);
                        else
                            Instantiate(roomUR, drawPos, Quaternion.identity);
                    }
                    else if (room.doorLeft)
                    {
                        Instantiate(roomUL, drawPos, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(roomU, drawPos, Quaternion.identity);
                    }
                }
                continue;
            }

            
            if(room.doorBottom)
            {
                if (room.doorRight)
                {
                    if (room.doorLeft)
                        Instantiate(roomDLR, drawPos, Quaternion.identity);
                    else
                        Instantiate(roomDR, drawPos, Quaternion.identity);
                }
                else if (room.doorLeft)
                {
                    Instantiate(roomDL, drawPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(roomD, drawPos, Quaternion.identity);
                }
                continue;
            }

            if (room.doorRight)
            {
                if (room.doorLeft)
                    Instantiate(roomLR, drawPos, Quaternion.identity);
                else
                    Instantiate(roomR, drawPos, Quaternion.identity);
            }
            else
            {
                Instantiate(roomL, drawPos, Quaternion.identity);
            }
        }
    }
}
