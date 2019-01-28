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
    private GameObject bossRoom;
    public GameObject objToSpawn;

    //protected PolygonCollider2D[] weaponColliders;
    protected Weapon weapon;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        bossRoom = GameObject.Find("BossRoom1");

        hasWeapon = (GetComponentInChildren<Weapon>() != null);

        if(hasWeapon)
            weapon = GetComponentInChildren<Weapon>(); // Get Colliders for the weapon

        //rigi.mass = 1000f; // Higher mass = less knockback
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

        if(Input.GetKeyDown(KeyCode.B))
        {
            SpawnHarmfulTiles();
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

    public override void Attack()
    {
        //Don't want to use this, as attacking times are determined in animation Behaviours
        return;
    }
    

    public override void ChooseAttack()
    {
        base.ChooseAttack();
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

    void SpawnHarmfulTiles()
    {
        List<Transform> besideTiles = GetAllTilesStraightLines();

        foreach (Transform tile in besideTiles)
            Instantiate(objToSpawn, tile.position, Quaternion.identity);
    }
    
}
