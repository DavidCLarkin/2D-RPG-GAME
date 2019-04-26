using System;
using System.Collections;
using UnityEngine;

public class MovementComponent : MonoBehaviour, IMoveable
{
    private InputComponent input;
	private MovementComponent movement;
    private Rigidbody2D rb;
    private Player player;

    // Particles
    public GameObject dashDownParticles;
    public GameObject dashUpParticles;
    public GameObject dashRightParticles;
    public GameObject dashLeftParticles;


    public float dodgeSpeed;
    public float startDodgetime;
    public float dodgeTime;
	public int dashCost = 30;
    public int staminaIncreaseAmount;
    private float walkTimer = 0.54f;

    public AudioClip step1;
    public AudioClip step2;
    public AudioClip step3;

    private int direction;
    private bool isMoving;

    public int maxStamina;
    public float stamina;
    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina += (int)value;
            if (stamina >= maxStamina) stamina = maxStamina;
        }
    }

    [HideInInspector]
    public float baseSpeed = 2.5f;
    public float speed;
    public event Action AnimateMovement = delegate { };

    private void Awake()
    {
        direction = 1;
        input = GetComponent<InputComponent>();
		movement = GetComponent<MovementComponent> ();
        input.OnDodge += Dodge; // subscribe to Dodge delegate
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

    }

    void Start()
    {
        // set max stamina and start sound coroutines
        stamina = maxStamina;
        StartCoroutine(Footsteps());
        StartCoroutine(RegenerateStamina());
    }

    // Update is called once per frame
    void Update ()
    {
        isMoving = (rb.velocity.magnitude > 0); // whether is moving or not

        AnimateMovement();
        Move();
    }

    /*
     * Helper for perks to reset speed
     */ 
    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    /*
     * Move using axes (controller and keys)
     */
    public void Move()
    {
        if (player == null)
            return;

        rb.velocity = new Vector2(input.Horizontal * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, input.Vertical * speed);

        if (Input.GetAxisRaw("Vertical") > 0)
            direction = 1;
        if (Input.GetAxisRaw("Horizontal") < 0)
            direction = 2;
        if (Input.GetAxisRaw("Vertical") < 0)
            direction = 3;
        if (Input.GetAxisRaw("Horizontal") > 0)
            direction = 4;
    }

    /*
     * Use stamina when dash
     */ 
	public void UseStamina()
	{
        stamina -= dashCost;
	}

    /*
     * Dodge method that handles moving the player in a certain direction via appling Force to rigidbody
     */ 
    void Dodge()
    {
        if (input.Attack) return;

        if (dodgeTime <= 0) // only dodge when timer 0
        {
            if (stamina >= dashCost) // and if has enough stamina
            {

                if (direction == 1) // dodge up
                {
                    rb.AddForce(Vector2.up * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                    SpawnDashParticles(direction);
                    
                }
                else if (direction == 2) // dodge left
                {
                    rb.AddForce(Vector2.left * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                    SpawnDashParticles(direction);
                }
                else if (direction == 3) // dodge down
                {
                    rb.AddForce(Vector2.down * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                    SpawnDashParticles(direction);
                }
                else if (direction == 4) // dodge right
                {
                    rb.AddForce(Vector2.right * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                    SpawnDashParticles(direction);
                }
            }
        }
    }

    /*
     * Simple method to spawn particles in the opposite direction the player is dashing
     */ 
    GameObject SpawnDashParticles(int dir)
    {
        GameObject dashParticles = null;
        switch (dir)
        {
            case 1:
                dashParticles = dashUpParticles;
                break;
            case 2:
                dashParticles = dashLeftParticles;
                break;
            case 3:
                dashParticles = dashDownParticles;
                break;
            case 4:
                dashParticles = dashRightParticles;
                break;
        }

        return Instantiate(dashParticles, transform.position, Quaternion.identity); // spawn the particles
    }

    /*
     * Disable footsteps and regeneration of stamina when they load etc. or sounds will overlap
     */ 
    void OnDisable()
    {
        StopCoroutine(RegenerateStamina());
        StopCoroutine(Footsteps());
    }

    /*
     * Continuously regenerate stamina while playing
     */ 
    IEnumerator RegenerateStamina()
    {
        while (true)
        {
            stamina += staminaIncreaseAmount;

            if (stamina > maxStamina)
                stamina = maxStamina;

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    /*
     * When moving, play a random footstep sound
     */ 
    public IEnumerator Footsteps()
    {
        while (true)
        {
            if(isMoving)
                SoundManager.instance.RandomizeSfx(step1, step2, step3);

            yield return new WaitForSeconds(walkTimer);
        }
    }


}
