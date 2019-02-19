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
}
