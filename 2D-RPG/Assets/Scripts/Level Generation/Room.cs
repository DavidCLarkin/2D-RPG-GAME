using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 position;
    public int type; // type of room, could be ENUM but more code to check room type later
    public bool doorRight, doorLeft, doorTop, doorBottom;

    public Room(Vector2 pos, int rType)
    {
        position = pos;
        rType = type;
    }
}
