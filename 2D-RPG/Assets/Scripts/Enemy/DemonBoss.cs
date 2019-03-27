using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attack 0 = Normal, Attack 1 = Spawn Projectiles
public class DemonBoss : Enemy
{
    private GameObject bossRoom;
    public GameObject projectile;
    public GameObject tileObj;

    public float SPAWN_PROJECTILE_COOLDOWN;
    public float SPAWN_PROJECTILE_TIMER;
    public float SPAWN_TILES_COOLDOWN;
    public float SPAWN_TILES_TIMER;

    public float closeAttackRange;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        bossRoom = GameObject.FindGameObjectWithTag("BossRoom");
        canBeKnockedBack = false;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();

        if (SPAWN_PROJECTILE_TIMER >= 0)
            SPAWN_PROJECTILE_TIMER -= Time.deltaTime;

        if (SPAWN_TILES_TIMER >= 0)
            SPAWN_TILES_TIMER -= Time.deltaTime;
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
        else if (distance <= attackRange && (ATTACK_TIMER <= 0 ||  SPAWN_PROJECTILE_TIMER <= 0))
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
                    if(dist <= closeAttackRange && (SPAWN_PROJECTILE_TIMER <= 0 && SPAWN_PROJECTILE_TIMER <= 0))
                    {
                        attackChosen = Random.Range(0, NumberOfAttacks);
                    }
                    else if (dist <= attackRange && dist >= closeAttackRange)
                    {
                        Debug.Log("Range for projectile/tiles");
                        if (SPAWN_PROJECTILE_TIMER <= 0)
                            attackChosen = 1;
                        else if (SPAWN_TILES_TIMER <= 0)
                            attackChosen = 2;
                    }
                    else if (dist <= closeAttackRange)
                    {
                        Debug.Log("range for melee");
                        attackChosen = 0;
                    }

                    Attack(attackChosen);
                }
                break;
        }
    }

    public override void ChooseAttack(float timeDelay, int attackChosen)
    {
        base.ChooseAttack(timeDelay, attackChosen);
        if (attackChosen == 1 && SPAWN_PROJECTILE_TIMER <= 0)
        {
            StartCoroutine(SpawnProjectiles(25, 1)); // do with delay to match animation
        }
        else if(attackChosen == 2 && SPAWN_TILES_TIMER <= 0)
        {
            SpawnHarmfulTiles();
        }
        else if(attackChosen == 0)
        {

        }
    }

    /*
     * Method to get all tiles following a pattern
     */
    List<Transform> GetTilePattern()
    {
        List<Transform> tilesToUse = new List<Transform>();
        foreach (Transform tile in bossRoom.GetComponent<Tiles>().tiles)
        {
            if (tile.transform.position.y % 3 == 0)
                tilesToUse.Add(tile);
        }

        return tilesToUse;

    }

    void SpawnHarmfulTiles()
    {
        List<Transform> besideTiles = GetTilePattern();

        foreach (Transform tile in besideTiles)
            Instantiate(tileObj, tile.position, Quaternion.identity);

        SPAWN_TILES_TIMER = SPAWN_TILES_COOLDOWN;
    }


    Vector2 CreateRingOfProjectiles(Vector2 center, float radius, int angle)
    {
        //Debug.Log(a);
        float ang = angle;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        //pos.z = -5; // to keep it above some tiles
        return pos;
    }

    /*
     * Spawn projectiles around the boss
     */ 
    IEnumerator SpawnProjectiles(int numProjectiles, float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < numProjectiles; i++)
        {
            int a = 360 / numProjectiles * i;
            Vector2 pos = CreateRingOfProjectiles(transform.position, 1f, a);
            GameObject obj = Instantiate(projectile, pos, Quaternion.identity);

            // Add force relative to the projectile position in opposite direction from the boss
            Vector2 dir = (obj.transform.position - gameObject.transform.position).normalized;

            obj.GetComponent<Rigidbody2D>().AddForce(dir * 200);
        }

        SPAWN_PROJECTILE_TIMER = SPAWN_PROJECTILE_COOLDOWN;
    }
}
