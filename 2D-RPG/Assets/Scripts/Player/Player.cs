using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
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
        //MouseDirectionAttack();
        //Animate();
    }

    /*
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
    */

    //Interface Method
    /*
    public void Animate()
    {
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
    }
    */
}
