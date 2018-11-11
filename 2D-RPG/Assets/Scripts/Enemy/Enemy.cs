using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : Interactable
{
	protected float DAMAGE_DELAY = 1f;
	protected float ATTACK_DELAY = 1.5f; // base for enemies
	protected float DAMAGE_TIMER;
	public float ATTACK_TIMER;
    protected float ANIMATION_DELAY;
    protected float ANIMATION_TIMER;

    public float BASE_ATTACK_RANGE;
    public float MEDIUM_ATTACK_RANGE;
    public float HARD_ATTACK_RANGE;
    public float LIGHT_ATTACK_RANGE;

 	public float speed = 1.5f;
	public float health = 100.0f;
	public float followRange;
	public float attackRange;

	protected Rigidbody2D rigi;
    protected Animator anim;

    protected bool canBeKnockedBack;
	private bool moving;
	protected float distance;

	protected enum State { Moving, Attacking, Idle };
	State state = State.Idle;

	protected virtual void Start() 
	{
        // Set the player object - For Pathfinding etc.
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
    }

	public override void Update() 
	{
		base.Update();

		if(health <= 0)
		{
			//Play death Animation
			Destroy (gameObject);
			GameManagerSingleton.instance.tooltip.gameObject.SetActive(false); //Otherwise, tooltip stays
		}

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
			health -= 25;
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
			state = State.Moving;
		} 
		else if(distance <= attackRange)
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
                //Debug.Log("Attacking");
				Attack();
				break;
		}

	}

	/*
	 * Method that makes the enemy follow the player around.
	 * NEED TO IMPLEMENT COLLISION DETECTION
	 */
	public virtual void FollowTarget(Transform target)
	{
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("knight_slice_down"))
          //  return;
		distance = Vector2.Distance(target.position, transform.position);
		if(distance <= followRange)
		{
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
			Vector3 vectorToTarget = target.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * 0.5f);
		}
	}

	/*
	 * Attack method which checks distance, and if distance is less than Enemy radius
	 * it attacks the player (deducts health from the player), then sets the ATTACK_TIMER
	 */
	public virtual void Attack()
	{
        if (ATTACK_TIMER > 0) return;

		distance = Vector2.Distance(player.position, transform.position); //check distance again to make sure enemy is in range of player - Should be replaced with checking collision
		if(distance <= attackRange)
		{
            PerformAttack(25, ATTACK_DELAY);
		}
	}

    public virtual void PerformAttack(int damage, float timeDelay)
    {
        ChooseAttack();
        player.GetComponent<Player>().health -= damage;
        ATTACK_TIMER = timeDelay;
    }

    public virtual void ChooseAttack()
    {

    }

    public virtual void LightAttack()
    { }

    public virtual void MediumAttack()
    { }

    public virtual void HeavyAttack()
    { }


	/*
	public void Knockback()
	{
		float xPos = 0.5f;
		float yPos = 0.5f;

		if(player.position.x > transform.position.x)
		{
			xPos = transform.position.x - 0.5f;
		} 
		else if(player.position.x < transform.position.x)
		{
			xPos = transform.position.x + 0.5f;
		}

		if(player.position.y > transform.position.y)
		{
			yPos = transform.position.y - 0.5f;
		} 
		else if(player.position.y < transform.position.y)
		{
			yPos = transform.position.y + 0.5f;
		}

		transform.position = new Vector2(xPos, yPos);

	}
	*///Not Used 

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
		
}
