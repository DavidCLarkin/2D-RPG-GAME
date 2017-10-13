using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	float speed = 2f;
	Rigidbody2D rigidbody;
	Animator anim;
	bool walking;

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

		if (Input.GetKeyDown (KeyCode.A))
			transform.rotation = Quaternion.Euler (0, -180, 0);
		if (Input.GetKeyDown (KeyCode.D))
			transform.rotation = Quaternion.Euler (0, 0, 0);


	}

	private void animate()
	{
		anim.SetBool ("walking", walking);

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)) 
		{
			walking = true;	
		} else
			walking = false;


	}
		
}
