using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    public float outOfRange;
    public GameObject minion;

	// Use this for initialization
	void Start ()
    {
        base.Start();
        ATTACK_DELAY = 3f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}

    public override void FollowTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, GameManagerSingleton.instance.player.transform.position) >= attackRange) return;

        transform.position = Vector2.MoveTowards(transform.position, GameManagerSingleton.instance.player.transform.position, -1 * speed * Time.deltaTime);
    }

    public override void DistanceChecking()
    {
        distance = Vector2.Distance(player.position, transform.position);

        if (distance < attackRange) // fleeing to get to attack range
        {
            //pathfinding.target = GameManagerSingleton.instance.player.transform;
            state = State.Moving;
        }
        else if (distance >= attackRange && distance < outOfRange)
        {
            state = State.Attacking;
        }
        else if (distance >= outOfRange)
        {
            state = State.Idle;
        }
    }

    public override void StateDecision()
    {
        switch (state)
        {
            case State.Idle:
                Debug.Log("Idle");
                break;
            case State.Moving:
                Debug.Log("Moving");
                FollowTarget(player);
                break;
            case State.Attacking:
                if (ATTACK_TIMER <= 0)
                {
                    Debug.Log("Attacking");
                    attackChosen = Random.Range(0, 2); // only needs to be called once
                    Attack(attackChosen);
                }
                break;
        }

    }

    public override void Attack(int attackChosen)
    {
        distance = Vector2.Distance(player.position, transform.position); //check distance again to make sure enemy is in range of player - Should be replaced with checking collision
        if (distance >= attackRange)
        {
            ATTACK_TIMER = ATTACK_DELAY;
            SpawnMinion(transform.position + transform.right);
            SpawnMinion(transform.position + transform.up);
        }

    }

    private void SpawnMinion(Vector2 position)
    {
        Instantiate(minion, position, Quaternion.identity);
    }
}
