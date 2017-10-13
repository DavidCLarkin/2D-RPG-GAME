using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	private float speed = 3f;
	private Rigidbody2D rigidbody;
	private Animator anim;
	private bool walkingUp;
	private bool walkingDown;
	private bool walkingLeft;
	private bool walkingRight;

	void Start () 
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Update () 
	{
		
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		rigidbody.velocity = new Vector2 (horizontal * speed, rigidbody.velocity.y);
		rigidbody.velocity = new Vector2 (rigidbody.velocity.x, vertical * speed);

		animate ();
	
	}

	private void animate()
	{
		anim.SetBool ("walkingUp", walkingUp);
		anim.SetBool ("walkingDown", walkingDown);
		anim.SetBool ("walkingLeft", walkingLeft);
		anim.SetBool ("walkingRight", walkingRight);

		if (Input.GetKey(KeyCode.W)) 
			walkingUp = true;	
		else
			walkingUp = false;
		
		if (Input.GetKey (KeyCode.S))
			walkingDown = true;
		else
			walkingDown = false;

		if (Input.GetKey (KeyCode.A))
			walkingLeft = true;
		else
			walkingLeft = false;

		if (Input.GetKey (KeyCode.D))
			walkingRight = true;
		else
			walkingRight = false;
	}
		
}
