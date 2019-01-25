using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public List<Transform> tiles;

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

    }
}
