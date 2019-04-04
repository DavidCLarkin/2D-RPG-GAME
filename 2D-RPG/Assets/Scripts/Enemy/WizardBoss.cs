using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Attack 0 - Spawn minions, Attack 1 - Shoot at player, Attack 2 - Tiles
public class WizardBoss : Enemy
{
    private GameObject bossRoom;

    public GameObject minion;
    public GameObject projectile;
    public GameObject harmfulTile;

    public float SPAWN_PROJECTILE_COOLDOWN;
    public float SPAWN_PROJECTILE_TIMER;

    public float SPAWN_MINION_COOLDOWN;
    public float SPAWN_MINION_TIMER;

    public float SPAWN_TILE_COOLDOWN;
    public float SPAWN_TILE_TIMER;

    private int tileSpacing = 3;
    private int tileSpawnIterations = 6;
    private int startingXValue = 2;
    private float delayBetweenTileSpawns = 1f;

    private Vector2[] positions;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        bossRoom = GameObject.FindGameObjectWithTag("BossRoom");
        canBeKnockedBack = false;
        positions = new Vector2[4];         

    }

    // Update is called once per frame
    public override void Update ()
    {
        base.Update();

        if (SPAWN_PROJECTILE_TIMER >= 0)
            SPAWN_PROJECTILE_TIMER -= Time.deltaTime;

        if (SPAWN_MINION_TIMER >= 0)
            SPAWN_MINION_TIMER -= Time.deltaTime;

        if (SPAWN_TILE_TIMER >= 0)
            SPAWN_TILE_TIMER -= Time.deltaTime;
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
                    if (dist <= attackRange && (SPAWN_PROJECTILE_TIMER <= 0 || SPAWN_MINION_TIMER <= 0 || SPAWN_TILE_TIMER <= 0))
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
        //base.ChooseAttack(timeDelay, attackChosen);
        if (attackChosen == 1 && SPAWN_PROJECTILE_TIMER <= 0)
        {
            StartCoroutine(SpawnManyProjectiles(Random.Range(6, 8), Random.Range(0.25f,0.4f)));
            ATTACK_TIMER = ATTACK_COOLDOWN;
        }
        else if (attackChosen == 0 && SPAWN_MINION_TIMER <= 0)
        {
            SpawnMinions();
            ATTACK_TIMER = ATTACK_COOLDOWN;
        }
        else if(attackChosen == 2 && SPAWN_TILE_TIMER <= 0)
        {
            StartCoroutine(SpawnManyTiles(startingXValue, delayBetweenTileSpawns, tileSpawnIterations));
            ATTACK_TIMER = ATTACK_COOLDOWN;
        }
    }

    /*
    * Method to get all tiles following a pattern
    */
    List<Transform> GetTilePattern(int tileX)
    {
        List<Transform> tilesToUse = new List<Transform>();
        foreach (Transform tile in bossRoom.GetComponent<Tiles>().tiles)
        {
            if (tile.transform.position.y % 3 == 0 && tile.transform.position.x % 4 == 0)
                tilesToUse.Add(tile);
        }

        return tilesToUse;

    }

    /*
     * Spawn tiles within a timer, so they appear gradually. 
     * xValue : starting x number
     * delay : delay between spawning tiles
     * amountOfIterations: amount of times to spawn
     */
    IEnumerator SpawnManyTiles(int xValue, float delay, int amountOfIterations)
    {
        List<Transform> tilesToSpawn = GetTilePattern(xValue);
        for(int i = 0; i < tilesToSpawn.Count; i++)
        {
            GameObject tile = Instantiate(harmfulTile, tilesToSpawn[i].position, Quaternion.identity);
            if(i % (tilesToSpawn.Count / 3) == 0) // only spawn a third of the tiles at once
                yield return new WaitForSeconds(delay);
        }

        SPAWN_TILE_TIMER = SPAWN_TILE_COOLDOWN;
    }

    void SpawnProjectile(Vector2 pos)
    {
        GameObject obj = Instantiate(projectile, pos, Quaternion.identity);
        Vector2 dir = player.transform.position - obj.transform.position;
        obj.GetComponent<Rigidbody2D>().velocity = dir.normalized * 5;
    }

    IEnumerator SpawnManyProjectiles(int amount, float delay)
    {
        positions[0] = (Vector2)gameObject.transform.position + Vector2.up;
        positions[1] = (Vector2)gameObject.transform.position + Vector2.right;
        positions[2] = (Vector2)gameObject.transform.position + Vector2.down;
        positions[3] = (Vector2)gameObject.transform.position + Vector2.left;

        for(int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnProjectile(positions[Random.Range(0, positions.Length)]);
            SoundManager.instance.PlayRandomOneShot(SoundManager.instance.wizardSpellProjectileSounds);
        }

        SPAWN_PROJECTILE_TIMER = SPAWN_PROJECTILE_COOLDOWN;
    }

    void SpawnMinions()
    {
        //Debug.Log(bossRoom.GetComponent<Tiles>().tiles.Count);
        GameObject minionOne = Instantiate(minion, bossRoom.GetComponent<Tiles>().tiles[34].transform.position, Quaternion.identity);
        GameObject minionTwo = Instantiate(minion, bossRoom.GetComponent<Tiles>().tiles[19].transform.position, Quaternion.identity);
        GameObject minionThree = Instantiate(minion, bossRoom.GetComponent<Tiles>().tiles[268].transform.position, Quaternion.identity);
        GameObject minionFour = Instantiate(minion, bossRoom.GetComponent<Tiles>().tiles[253].transform.position, Quaternion.identity);

        // Play random minion spawn sound
        SoundManager.instance.PlayRandomOneShot(SoundManager.instance.wizardSpawnMinionSounds);

        SPAWN_MINION_TIMER = SPAWN_MINION_COOLDOWN;
    }
}
