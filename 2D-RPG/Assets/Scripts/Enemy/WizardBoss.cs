using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : Enemy
{
    private GameObject bossRoom;
    public GameObject projectile;

    public float SPAWN_PROJECTILE_COOLDOWN;
    public float SPAWN_PROJECTILE_TIMER;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        bossRoom = GameObject.FindGameObjectWithTag("BossRoom");
        canBeKnockedBack = false;
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

    public override void StateDecision()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        //base.StateDecision();
        switch (state)
        {
            case State.Idle:
                //Debug.Log("Idle");
                break;
            case State.Moving:
                //Debug.Log("Moving");
                FollowTarget(player);
                break;
            case State.Attacking:
                if (ATTACK_TIMER <= 0)
                {
                    // if close and timers 0, choose random so it's not always the same
                    if (dist <= attackRange && (SPAWN_PROJECTILE_TIMER <= 0 && SPAWN_PROJECTILE_TIMER <= 0))
                    {
                        attackChosen = Random.Range(0, NumberOfAttacks);
                    }

                    Attack(attackChosen);
                }
                break;
        }
    }

    public override void ChooseAttack(float timeDelay, int attackChosen)
    {
        base.ChooseAttack(timeDelay, attackChosen);
        if ((attackChosen == 1 || attackChosen == 0) && SPAWN_PROJECTILE_TIMER <= 0)
        {
            StartCoroutine(SpawnManyProjectiles(Random.Range(5, 8), Random.Range(0.25f,0.5f)));
            //StartCoroutine(SpawnProjectiles(25, 1)); // do with delay to match animation
        }
        //else if (attackChosen == 1)
        //{
            //SpawnHarmfulTiles();
        //}
    }

    void SpawnProjectile()
    {
        GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 dir = player.transform.position - obj.transform.position;
        obj.GetComponent<Rigidbody2D>().velocity = dir.normalized * 7;
    }

    IEnumerator SpawnManyProjectiles(int amount, float delay)
    {
        for(int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnProjectile();
        }
    }
}
