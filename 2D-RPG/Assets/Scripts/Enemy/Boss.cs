using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

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

    private GameObject bossRoom;
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
        MEDIUM_ATTACK_RANGE = 2f;
        HARD_ATTACK_RANGE = 3f;
        LIGHT_ATTACK_RANGE = 2f;
        attackRange = BASE_ATTACK_RANGE; // this changes depending on attacks
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

		//GetCurrentRoom();

    }

    public override void MediumAttack()
    {
        distance = Vector2.Distance(player.position, transform.position); // check distance again to make sure enemy is in range of player - Should be replaced with checking collision
        //if (distance <= attackRange)
        //{
        //    PerformAttack(25f, ATTACK_DELAY);
        //}
    }

    //Fix Z position, bug in Freeze Rotation
    void FixedUpdate()
    {
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
        base.StateDecision();
    }

    public override void FollowTarget(Transform target)
    {
		if (anim.GetBool("attackDown") == true)
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
            if (tile.transform.position.x == currentPos.x || tile.transform.position.y == currentPos.y)
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

        foreach (Transform tile in besideTiles)
            Instantiate(objToSpawn, tile.position, Quaternion.identity);

		SPAWN_TILE_TIMER = SPAWN_TILE_DELAY;
    }
    
	/*
	 * Could maybe be reworked to detect any current room the boss or enemy is in
	 */
	void GetCurrentRoom()
	{
		GameObject room = GameObject.FindGameObjectWithTag ("BossRoom"); // or search for room
		foreach (Transform tile in room.GetComponent<Tiles>().tiles)
		{
			//Debug.Log (ConvertAbsPositionToUnitPos(transform.position) + "Boss Pos: "+transform.position);
			//Debug.Log ("Tile Pos:" + tile.transform.position);
			if (ConvertAbsPositionToUnitPos (transform.position) == (Vector2)tile.transform.position) 
			{
				Debug.Log ("Tile and boss room the same");
			}
		}
	}
}
