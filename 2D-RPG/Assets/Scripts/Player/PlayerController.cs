using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	private Camera cam;
	private float speed = 2.5f;
	public float health = 100.0f;
	private Rigidbody2D rigidbody;
	private Animator anim;
	private Inventory inventory;

	private bool walkingUp;
	private bool walkingDown;
	private bool walkingLeft;
	private bool walkingRight;

	private float vertical;
	private float horizontal;
	private static bool playerExists;

	public string startPoint;
	public Interactable focus;

	void Start() 
	{
		inventory = GetComponent<Inventory>();
		cam = Camera.main;
		rigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		if(!playerExists) 
		{
			playerExists = true;
			DontDestroyOnLoad(transform.gameObject);
		} 
		else
			Destroy(gameObject);
	}

	void Update() 
	{
		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");
		rigidbody.velocity = new Vector2 (horizontal * speed, rigidbody.velocity.y);
		rigidbody.velocity = new Vector2 (rigidbody.velocity.x, vertical * speed);

		if(Input.GetMouseButtonDown(0)) 
		{
			//Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(hit)
			{
				Interactable interactable = hit.collider.GetComponent<Interactable> ();
				if(interactable != null) 
				{
					string intTag = interactable.tag;
					switch (intTag)
					{
					case "Enemy":
						Debug.Log ("Enemy CASE"); //ATTACK
						break;
						case "Item":
						Debug.Log("Item CASE"); //PICK UP
						break;
					default:
						Debug.Log("Default");
						break;
					}

					setFocus (interactable);
				}
			}
		}


		animate();
	
	}

	void setFocus(Interactable newFocus)
	{
		focus = newFocus;
		newFocus.onFocused(transform);
	}

	void removeFocus()
	{
		focus.deFocused ();
		focus = null;
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
