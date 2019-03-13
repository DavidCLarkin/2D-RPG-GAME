using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

// Attack 0 - Default Attack, Attack 1 - Spawn Dangerous Tiles

public class Boss : Enemy
{
    private bool walkingUp;
    private bool walkingDown;
    private bool walkingLeft;
    private bool walkingRight;
    private bool attacking;
    protected bool hasWeapon;
    public float SPAWN_TILE_DELAY = 5f;
    public float SPAWN_TILE_TIMER;
    float CHARGE_ATTACK_DELAY = 5f;
    public float CHARGE_ATTACK_TIMER;

    private GameObject bossRoom;
    bool isCoroutineRunning = false;
    public GameObject objToSpawn;

    protected Weapon weapon;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

		bossRoom = GameObject.FindGameObjectWithTag("BossRoom");

        hasWeapon = (GetComponentInChildren<Weapon>() != null);
        
        if(hasWeapon)
            weapon = GetComponentInChildren<Weapon>(); // Get Colliders for the weapon

        canBeKnockedBack = false;
        DAMAGE_DELAY = 1.5f;
        BASE_ATTACK_RANGE = 1.5f;
        //MEDIUM_ATTACK_RANGE = 2f;
        //HARD_ATTACK_RANGE = 3f;
        //LIGHT_ATTACK_RANGE = 2f;
        attackRange = BASE_ATTACK_RANGE; // this changes depending on attacks
        CHARGE_ATTACK_TIMER = CHARGE_ATTACK_DELAY; // So boss can't charge immediately at start of fight
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();

        //animate();
        Vector3 oldRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);

        if(SPAWN_TILE_TIMER >= 0)
        {
            SPAWN_TILE_TIMER -= Time.deltaTime;
        }

        if(CHARGE_ATTACK_TIMER >= 0)
        {
            CHARGE_ATTACK_TIMER -= Time.deltaTime;
        }

    }

    public override void MediumAttack()
    {
        distance = Vector2.Distance(player.position, transform.position); // check distance again to make sure enemy is in range of player - Should be replaced with checking collision
        //if (distance <= attackRange)
        //{
        //    PerformAttack(25f, ATTACK_DELAY);
        //}
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void DistanceChecking()
    {
        base.DistanceChecking();
    }

    public override void ChooseAttack(float timeDelay, int attackChosen)
    {
        base.ChooseAttack(timeDelay, attackChosen);
		if(attackChosen == 1 && SPAWN_TILE_TIMER <= 0)
        {
            SpawnHarmfulTiles();
        }
    }

    public override void StateDecision()
    {
        //base.StateDecision();
        switch (state)
        {
            case State.Idle:
                Debug.Log("Idle");
                break;
            case State.Moving:
                Debug.Log("Moving");
                FollowTarget(player);
                // Used to get closer to player. Not really an attack, just a gap closer
                if (distance >= 6f && CHARGE_ATTACK_TIMER <= 0)
                    SprintTowardsPlayer();
                break;
            case State.Attacking:
                if (ATTACK_TIMER <= 0)
                {
                    if (SPAWN_TILE_TIMER <= 0)
                        attackChosen = 1;
                    else
                        attackChosen = 0;
                    Debug.Log("Attacking");
                    //attackChosen = Random.Range(0, 2); // only needs to be called once
                    Attack(attackChosen);
                }
                break;
        }
    }

    public override void FollowTarget(Transform target)
    {
		if (anim.GetBool("attackDown") || anim.GetBool("attackUp"))
			return;
        base.FollowTarget(target);

    }

    IEnumerator StopAnimation()
    {
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    /*
     * Method to convert transform position to relative position (fixed) 
     */
    Vector2 ConvertAbsPositionToUnitPos(Vector2 pos)
    {
        return new Vector2(Mathf.RoundToInt(transform.position.x % 32), Mathf.RoundToInt(transform.position.y % 32));
    }

    /*
     * Method to get all tiles to the right, left, above and below transform position
     */
    List<Transform> GetAllTilesStraightLines()
    {
        Vector2 currentPos = ConvertAbsPositionToUnitPos(transform.position);

        List<Transform> tilesToUse = new List<Transform>();
        foreach(Transform tile in bossRoom.GetComponent<Tiles>().tiles)
        {
            if (tile.transform.position.x % 32 == currentPos.x % 32 || tile.transform.position.y % 32 == currentPos.y % 32)
                tilesToUse.Add(tile);
        }

        return tilesToUse;

    }

    /*
     * Method to spawn an object at all adjacent ground tiles in straight lines in each direction
     *  REMEMBER: ATTACH A DESTROY SCRIPT TO THE TILES SPAWNED SO THEY DESTROY
     */
    void SpawnHarmfulTiles()
    {
        List<Transform> besideTiles = GetAllTilesStraightLines();
        Debug.Log("tiles to spawn size: " + besideTiles.Count);

        foreach (Transform tile in besideTiles)
            Instantiate(objToSpawn, tile.position, Quaternion.identity);

		SPAWN_TILE_TIMER = SPAWN_TILE_DELAY;
        Debug.Log("Spawned tiles");
    }
    
	/*
	 * Could maybe be reworked to detect any current room the boss or enemy is in
	 */
	void GetCurrentRoom()
	{
		GameObject room = GameObject.FindGameObjectWithTag ("BossRoom"); // or search for room
		foreach (Transform tile in room.GetComponent<Tiles>().tiles)
		{
			if (ConvertAbsPositionToUnitPos (transform.position) == (Vector2)tile.transform.position) 
			{
				Debug.Log ("Tile and boss room the same");
			}
		}
	}

    void SprintTowardsPlayer()
    {
        Debug.Log("Charge at enemy");
        Vector2 playerPos = player.transform.position;

        StartCoroutine(DashToLocation(playerPos, 1));         
    }

    /*
     * Only dash if this is not already running
     */
    IEnumerator DashToLocation(Vector2 location, int seconds)
    {
        if (isCoroutineRunning)
            yield break;
        isCoroutineRunning = true;

        yield return new WaitForSeconds(seconds);

        transform.position = location;
        CHARGE_ATTACK_TIMER = CHARGE_ATTACK_DELAY;
        isCoroutineRunning = false;

    }
}
