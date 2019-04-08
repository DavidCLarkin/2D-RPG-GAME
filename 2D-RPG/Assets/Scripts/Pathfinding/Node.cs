using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This Script courtesy of Sebastian Lague, Video series found here: https://www.youtube.com/watch?v=nhiFx28e7JY 
 */
public class Node
{
    public bool walkable;
    public Vector2 position;
    public int gridX, gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool isWalkable, Vector2 pos, int gridXPos, int gridYPos)
    {
        walkable = isWalkable;
        position = pos;
        gridX = gridXPos;
        gridY = gridYPos;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
