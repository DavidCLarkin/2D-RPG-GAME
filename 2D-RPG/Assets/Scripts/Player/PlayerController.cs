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

    private KeyCode[] keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };


	private bool walkingUp;
	private bool walkingDown;
	private bool walkingLeft;
	private bool walkingRight;

    public float dodgeSpeed;
    public float startDodgetime;
    private float dodgeTime;
    private int direction;


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
        dodgeTime = startDodgetime;

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
					case "Enemy": // TODO add attacking, interaction etc.
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

        Dodge();


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

    public void Dodge()
    {
        if(direction == 0)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKey(keys[i]) && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    direction = i + 1;
                }
            }
        }
        else
        {
            if(dodgeTime <= 0)
            {
                direction = 0;
                dodgeTime = startDodgetime;
                rigidbody.velocity = Vector2.zero;
            }
            else
            {
                dodgeTime -= Time.deltaTime;

                if(direction == 1)
                {
                    rigidbody.velocity = Vector2.up * dodgeSpeed;
                }
                else if(direction == 2)
                {
                    rigidbody.velocity = Vector2.left * dodgeSpeed;
                }
                else if(direction == 3)
                {
                    rigidbody.velocity = Vector2.down * dodgeSpeed;
                }
                else if(direction == 4)
                {
                    rigidbody.velocity = Vector2.right * dodgeSpeed;
                }
            }
        }
    }
		
}
