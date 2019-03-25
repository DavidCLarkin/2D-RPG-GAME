using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    
	// Use this for initialization
	public void Start ()
    {
        base.Start();
        //ATTACK_DELAY = 1f; set in inspector
        ATTACK_TIMER = ATTACK_DELAY;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
	}

    public override void DistanceChecking()
    {
        distance = Vector2.Distance(player.position, transform.position);
        if (distance > followRange)
        {
            state = State.Idle;
        }
        else if (distance <= followRange && distance > attackRange)
        {
            pathfinding.target = GameManagerSingleton.instance.player.transform;
            state = State.Moving;
        }
        else if (distance <= attackRange && ATTACK_TIMER <= 0)
        {
            state = State.Attacking;
        }
    }
}
