using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IAnimateable
{
    private KeyCode[] keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    private Rigidbody2D rigidbody;
    private Camera cam;
    private Animator anim;
    public Interactable focus;

    //private float vertical;
    //private float horizontal;

    //[SerializeField]
    //private float speed = 2.5f;

    private static bool playerExists;

    private bool walkingUp;
    private bool walkingDown;
    private bool walkingLeft;
    private bool walkingRight;
    private bool attackDown;
    private bool attackUp;
    private bool attackLeft;
    private bool attackRight;
    public bool isAttacking;


    private int facingDirection;

    public float dodgeSpeed;
    public float startDodgetime;
    private float dodgeTime;
    private int direction;

    public string startPoint;

    // Use this for initialization
    void Start ()
    {
        //inventory = GetComponent<Inventory>();
        facingDirection = 3; // Set facing down
        cam = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dodgeTime = startDodgetime;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	public void Update ()
    {
        //Debug.Log(facingDirection);
        MouseDirectionAttack();

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    string intTag = interactable.tag;
                    switch (intTag)
                    {
                        case "Enemy": // TODO add attacking, interaction etc.
                            Debug.Log("Enemy CASE"); //ATTACK
                            break;
                        case "Item":
                            Debug.Log("Item CASE"); //PICK UP
                            break;
                        default:
                            Debug.Log("Default");
                            break;
                    }

                    setFocus(interactable);
                }
            }
        }

        Animate();
        //Move();
        Dodge();
    }

    void setFocus(Interactable newFocus)
    {
        focus = newFocus;
        newFocus.onFocused(transform);
    }

    void removeFocus()
    {
        focus.deFocused();
        focus = null;
    }

    public void MouseDirectionAttack()
    {
        Vector2 dir = cam.ScreenToViewportPoint((Input.mousePosition));
        //Debug.Log(dir);
        if (Input.GetMouseButtonDown(0))
        {
            if (dir.y >= 0.65f)
            {
                Debug.Log("Top");
                facingDirection = 1;

            }
            else if (dir.x <= 0.5f && (dir.y > 0.35f && dir.y < 0.65f))
            {
                Debug.Log("Left");
                facingDirection = 2;
            }
            else if (dir.y <= 0.35f)
            {
                Debug.Log("Down");
                facingDirection = 3;
            }
            else if (dir.x >= 0.5f && (dir.y > 0.35f && dir.y < 0.65f))
            {
                Debug.Log("Right");
                facingDirection = 4;
            }
            //Debug.Log(dir);
        }
    }

    //Interface method
    /*public void Move()
    {
        if (isAttacking)
            return;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertical * speed);
    }
    */

    //Interface Method
    public void Animate()
    {
        anim.SetBool("walkingUp", walkingUp);
        anim.SetBool("walkingDown", walkingDown);
        anim.SetBool("walkingLeft", walkingLeft);
        anim.SetBool("walkingRight", walkingRight);
        anim.SetBool("knight_slice_down", attackDown);
        anim.SetBool("attackLeft", attackLeft);
        anim.SetBool("attackRight", attackRight);
        anim.SetBool("attackUp", attackUp);

        if (Input.GetMouseButtonDown(0) && facingDirection == 1)
            attackUp = true;
        else
            attackUp = false;

        if (Input.GetMouseButtonDown(0) && facingDirection == 2)
            attackLeft = true;
        else
            attackLeft = false;

        if (Input.GetMouseButtonDown(0) && facingDirection == 3)
            attackDown = true;
        else
            attackDown = false;

        if (Input.GetMouseButtonDown(0) && facingDirection == 4)
            attackRight = true;
        else
            attackRight = false;

        if (Input.GetKey(KeyCode.W))
        {
            walkingUp = true;
            //facingDirection = 1;
        }
        else
        {
            walkingUp = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            walkingLeft = true;
            //facingDirection = 2;
        }
        else
        {
            walkingLeft = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            walkingDown = true;
            //facingDirection = 3;
        }
        else
        {
            walkingDown = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            walkingRight = true;
            //facingDirection = 4;
        }
        else
        {
            walkingRight = false;
        }
    }

    public void Dodge()
    {
        if (isAttacking)
            return;

        if (direction == 0)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKey(keys[i]) && Input.GetKeyDown(KeyCode.Space))
                {
                    direction = i + 1;
                }
            }
        }
        else
        {
            if (dodgeTime <= 0)
            {
                direction = 0;
                dodgeTime = startDodgetime;
                rigidbody.velocity = Vector2.zero;
            }
            else
            {
                dodgeTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rigidbody.velocity = Vector2.up * dodgeSpeed;
                }
                else if (direction == 2)
                {
                    rigidbody.velocity = Vector2.left * dodgeSpeed;
                }
                else if (direction == 3)
                {
                    rigidbody.velocity = Vector2.down * dodgeSpeed;
                }
                else if (direction == 4)
                {
                    rigidbody.velocity = Vector2.right * dodgeSpeed;
                }
            }
        }
    }
}
