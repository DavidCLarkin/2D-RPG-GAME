using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int width, height; // width and height of where rooms can be placed
    public int numbOfRooms;
    public int minRooms; // 5 is good
    public int maxRooms; // 12 is good
    public Vector2 finalRoomPos; // keep final room as it's going to be the boss room spawn location

    [Range(1,3)]
    public int roomDensityAllowance = 2; // essentially a higher value makes the dungeon appear more gridlike - 1/2 is ideal.

    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();

    // all rooms
    public GameObject roomR, roomL, roomU, roomD, roomDL, roomDR, roomDLR, roomLR, roomUD, roomUDL, roomUDLR, roomUDR, roomUL, roomULR, roomUR;
    // array of boss rooms to choose from
    public GameObject[] bossRooms;

	// Use this for initialization
	void Awake ()
    {
        numbOfRooms = Random.Range(minRooms, maxRooms); // random dungeon size

        CreateRooms(); // The main function to set the positions of each room in the dungeon
        SetRoomDoors(); // Decide which room has what kind of doors depending on neighbouring rooms 
        InstantiateRooms(); // Spawn the rooms and boos room at the end
	}

    /*
     * Set up the initial room position at (0,0), choose room positions for each room,
     * insert each room into the takenPositions list. 
     */ 
    void CreateRooms()
    {
        rooms = new Room[width * 2, height* 2];
        rooms[width, height] = new Room(new Vector2(0,0)); // first room set to center
        takenPositions.Insert(0, new Vector2(0,0)); //insert a taken position to list
        Vector2 checkPos = new Vector2(0,0);

        //add rooms
        for (int i = 0; i < numbOfRooms - 1; i++)
        {
            //grab new pos
            checkPos = NewPosition();

            //test new pos
            if(NumberOfNeighbours(checkPos, takenPositions) > roomDensityAllowance)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbours(checkPos, takenPositions) > roomDensityAllowance && iterations < 100);
            }

            //finalise pos
            rooms[(int)checkPos.x + width, (int)checkPos.y + height] = new Room(checkPos);
            takenPositions.Insert(0, checkPos);

            if (i == numbOfRooms - 2)
                finalRoomPos = checkPos; // setting boss room position as last room

        }

    }

    /*
     * Method to retrieve the next position to add a room
     */ 
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = new Vector2(0,0);

        // Attempt to made a new position at a position that isn't already in takenPositions list
        do
        {
            int index = Random.Range(0, takenPositions.Count - 1); // choose random taken position

            // Retrieve x and y from chosen room
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            // 50% random values - essentially checking random direction to add a room
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
        } while (takenPositions.Contains(checkingPos) || x >= width || x < -width || y >= height || y < -height);

        return checkingPos;
    }

    /*
     * Method similar to NewPosition but instead of choosing random room in takenPositions,
     * I choose a room with less than roomDensityAllowance neighbouring rooms, to be more selective
     */ 
    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0, x= 0, y = 0;
        Vector2 checkingPos = new Vector2(0,0);

        while (takenPositions.Contains(checkingPos) || x >= width || x < -width || y >= height || y < -height)
        {
            inc = 0;
            // Keep choosing rooms until a room is not beside 3 or more neighbours
            while(NumberOfNeighbours(takenPositions[index], takenPositions) > roomDensityAllowance && inc < 100)
            {
                index = Random.Range(0, takenPositions.Count - 1);
                inc++;
            }
            
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            bool vertical = (Random.value < 0.5f); // place vertically if true (y axis), horizontally if false (x axis)
            bool positive = (Random.value < 0.5f); // if positive, increment, else decrement

            if (vertical) // up 
            {
                if (positive) // up
                    y += 1;
                else // down
                    y -= 1;
            }
            else // down
            {
                if (positive) // right
                    x += 1;
                else // left
                    x -= 1;
            }

            checkingPos = new Vector2(x, y);
        }

        return checkingPos;
    }

    // Helper to get the number of rooms beside a position, checking up, down, left and right of the room
    int NumberOfNeighbours(Vector2 roomPos, List<Vector2> usedPositions)
    {
        int numbOfNeighbours = 0;
        if (usedPositions.Contains(roomPos + Vector2.right))
            numbOfNeighbours++;
        if (usedPositions.Contains(roomPos + Vector2.left))
            numbOfNeighbours++;
        if (usedPositions.Contains(roomPos + Vector2.up))
            numbOfNeighbours++;
        if (usedPositions.Contains(roomPos + Vector2.down))
            numbOfNeighbours++;

        return numbOfNeighbours;
    }

    void SetRoomDoors()
    {
        for(int x = 0; x < width * 2; x++)
        {
            for (int y = 0; y < height * 2; y++)
            {
                if (rooms[x, y] == null) // if room position not taken
                    continue;

                //Check above
                if (y - 1 < 0)
                    rooms[x, y].doorBottom = false;
                else
                    rooms[x, y].doorBottom = (rooms[x, y - 1] != null); // if theres a room below, then has door at bottom

                //Check below
                if (y + 1 >= height * 2)
                    rooms[x, y].doorTop = false;
                else
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null); // if theres a room above, then has door at top

                //check left
                if (x - 1 < 0)
                    rooms[x, y].doorLeft = false;
                else
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null); // if theres a room to the left, then has door to the left

                //check right
                if (x + 1 >= width * 2)
                    rooms[x, y].doorRight = false;
                else
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null); // if theres a room to the right, then has door to the right
            }
        }
    }

    /*
     * Method to finally Instantiate each room at their positions with an offset
     * so they instantiate correctly
     */ 
    void InstantiateRooms()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
                continue;

            Vector2 drawPos = room.position;
            drawPos.x *= Mathf.RoundToInt(20);
            drawPos.y *= Mathf.RoundToInt(20);

            // Spawn boss room at the end
            if (room.position == finalRoomPos)
            {
                // Have to be set up in inspector in this order : Up, Right, Down, Left
                if (room.doorTop)
                    Instantiate(bossRooms[0], drawPos, Quaternion.identity);
                else if(room.doorRight)
                    Instantiate(bossRooms[1], drawPos, Quaternion.identity);
                else if(room.doorBottom)
                    Instantiate(bossRooms[2], drawPos, Quaternion.identity);
                else if(room.doorLeft)
                    Instantiate(bossRooms[3], drawPos, Quaternion.identity);
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
