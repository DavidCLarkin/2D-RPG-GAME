using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Interactable
{
	private float DAMAGE_DELAY = 1f;
	private float ATTACK_DELAY = 1.5f;
	private float DAMAGE_TIMER;
	private float ATTACK_TIMER;
	private float testTimer;

 	public float speed = 1.5f;
	public float health = 100.0f;
	public float followRange;
	private Rigidbody2D rigidbody;
	private bool moving;
	private float distance;

	void Start() 
	{
		player = GameManagerSingleton.instance.player.transform;
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
			
		if(ATTACK_TIMER <= 0)
		{
			Attack();
		}
			
		FollowTarget(player);

	}

	public override void Interact() //called when Interactable Interact() method is called
	{
		//base.Interact();
		if (DAMAGE_TIMER <= 0)
		{
			health -= 25;
			Knockback();
			DAMAGE_TIMER = DAMAGE_DELAY;
		}
	}

	/*
	 * Method that makes te enemy follow the player around.
	 * NEED TO IMPLEMENT COLLISION DETECTION
	 */
	public void FollowTarget(Transform target)
	{
		distance = Vector2.Distance(player.position, transform.position);
		if(distance <= followRange)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			Vector3 vectorToTarget = player.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			//print(angle);
			Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * 0.5f);
		}
	}

	/*
	 * Attack method which checks distance, and if distance is less than Enemy radius
	 * it attacks the player (deducts health from the player), then sets the ATTACK_TIMER
	 */
	public void Attack()
	{
		distance = Vector2.Distance(player.position, transform.position);
		if(distance <= radius)
		{
			player.GetComponent<PlayerController>().health -= 25f;
			ATTACK_TIMER = ATTACK_DELAY; 
		}
	}

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
		
}
