using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 position;
    public bool doorRight, doorLeft, doorTop, doorBottom;

    public Room(Vector2 pos)
    {
        position = pos;
    }
}
