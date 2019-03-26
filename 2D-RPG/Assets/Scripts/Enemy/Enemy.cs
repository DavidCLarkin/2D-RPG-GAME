using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public abstract class Enemy : Interactable
{
	protected float DAMAGE_DELAY = 1f;
	public float ATTACK_COOLDOWN; // set in inspector for time between attacks
	protected float DAMAGE_TIMER;
	public float ATTACK_TIMER;
    protected float ANIMATION_DELAY;
    protected float ANIMATION_TIMER;

    public int NumberOfAttacks;

    public float BASE_ATTACK_RANGE;

 	public float speed = 1.5f;
	public float followRange;
	public float attackRange;

	public int Experience;

    private LevelGrid grid;
    public List<Node> path = new List<Node>();
    protected Rigidbody2D rigi;
    protected Animator anim;
    protected Pathfinding pathfinding;

    protected bool canBeKnockedBack;
	protected bool moving;
	protected float distance;
	public int attackChosen;

    [HideInInspector]
	public enum State { Moving, Attacking, Idle };
    [HideInInspector]
	public State state = State.Idle;

	protected virtual void Start() 
	{
        // Set the player object - For Pathfinding etc.
		GetComponent<HealthComponent> ().OnDie += IncreasePlayerExp;
        SetUp();
        canBeKnockedBack = true;
	}

    // Simple method to set up necessary variables - Used so I can have different variables for different enemies 
    // e.g,. setting canBeKnockedBack to false for bosses
    private void SetUp()
    {
        player = GameManagerSingleton.instance.player.transform;
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grid = GameObject.Find("A*").GetComponent<LevelGrid>();
        pathfinding = GetComponent<Pathfinding>();
    }

	public override void Update() 
	{
		base.Update();

		//Timers
		if(DAMAGE_TIMER >= 0)
		{
			DAMAGE_TIMER -= Time.deltaTime;
		}

		if(ATTACK_TIMER >= 0)
		{
			ATTACK_TIMER -= Time.deltaTime;
		}
			
        if(ANIMATION_TIMER >= 0)
        {
            ANIMATION_TIMER -= Time.deltaTime;
        }

		DistanceChecking();
		StateDecision();

	}

	public override void Interact() //called when Interactable Interact() method is called
	{
		//base.Interact();
		if (DAMAGE_TIMER <= 0)
		{
            if(canBeKnockedBack)
			    Knockback(10000f);
			DAMAGE_TIMER = DAMAGE_DELAY;
		}
	}

	/*
	 * Checking the distance between player and AI.
	 * Changes the state of the FSM
	 */ 
	public virtual void DistanceChecking()
	{
		distance = Vector2.Distance(player.position, transform.position);
		if(distance > followRange)
		{
			state = State.Idle;
		}
		else if(distance <= followRange && distance > attackRange)
		{
            pathfinding.target = GameManagerSingleton.instance.player.transform;
			state = State.Moving;
		} 
		else if(distance <= attackRange && ATTACK_TIMER <= 0)
		{
			state = State.Attacking;
		} 
	}

	/*
	 * Deciding what methods to use for the AI
	 */
	public virtual void StateDecision()
	{
		switch(state)
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
                    //Debug.Log("Attacking");
                    if (NumberOfAttacks > 1)
                    {
                        attackChosen = Random.Range(0, NumberOfAttacks); // only needs to be called once
                        Attack(attackChosen);
                    }
                    else
                        ATTACK_TIMER = ATTACK_COOLDOWN;
                }
				break;
		}

	}

    // Pathfinding method
	public virtual void FollowTarget(Transform target)
	{
        // Pathfinding
        //path = grid.path;
        if (path.Count > 0)
        {
            if ((Vector2)transform.position != path[0].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, path[0].position, speed * Time.deltaTime);
            }
            else
            {
                path.RemoveAt(0); // Remove the path node and continue to the next one
            }
        }
    }

	/*
	 * Attack method which checks distance, and if distance is less than Enemy radius
	 * it attacks the player (deducts health from the player), then sets the ATTACK_TIMER
	 */
	public virtual void Attack(int attackChosen)
	{
        //distance = Vector2.Distance(player.position, transform.position); //check distance again to make sure enemy is in range of player - Should be replaced with checking collision
        //if (distance <= attackRange)
        //{
            ChooseAttack(ATTACK_COOLDOWN, attackChosen);
        //}

	}

    public virtual void ChooseAttack(float timeDelay, int attackChosen)
    {
        int damage = PerformAttack(attackChosen);
        ATTACK_TIMER = timeDelay; // + Random.Range(1,3);
    }

    public virtual int PerformAttack(int attackChosen)
    {
        int dmg = 0;
        switch(attackChosen)
        {
            case 0: dmg = 20;
                break;
            case 1: dmg = 25;
                break;
            default: dmg = 0;
                Debug.Log("No damage, default case");
                break;
        }

        return dmg;
    }

	public virtual void Knockback(float force)
	{
		float xPos = force;
		float yPos = force;

		if(player.position.x > transform.position.x)
		{
			xPos = transform.position.x - force;
		} 
		else if(player.position.x < transform.position.x)
		{
			xPos = transform.position.x + force;
		}

		if(player.position.y > transform.position.y)
		{
			yPos = transform.position.y - force;
		} 
		else if(player.position.y < transform.position.y)
		{
			yPos = transform.position.y + force;
		}

		rigi.AddForce(new Vector2(xPos, yPos));
	}

	void IncreasePlayerExp()
	{
		GameManagerSingleton.instance.player.GetComponent<ExperienceComponent> ().IncreaseExp (Experience);	
	}
}
