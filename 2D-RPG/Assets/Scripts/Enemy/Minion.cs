using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{

	// Use this for initialization
	void Start ()
    {
        base.Start();
        pathfinding.target = player;
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
        Debug.Log("Minion path:" + path.Count);
	}

    public override void DistanceChecking()
    {
        distance = Vector2.Distance(player.position, transform.position);
        if (distance < followRange)
        {
            state = State.Moving;
        }
    }
}
