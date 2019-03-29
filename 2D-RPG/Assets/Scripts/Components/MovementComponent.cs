using System;
using System.Collections;
using UnityEngine;

public class MovementComponent : MonoBehaviour, IMoveable
{
    private InputComponent input;
	private MovementComponent movement;
    private Rigidbody2D rb;
    private Player player;

    public float dodgeSpeed;
    public float startDodgetime;
    public float dodgeTime;
	public int dashCost = 30;
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

    [SerializeField]
    private float speed;
    public event Action AnimateMovement = delegate { };

    private void Awake()
    {
        direction = 1;
        input = GetComponent<InputComponent>();
		movement = GetComponent<MovementComponent> ();
		GetComponent<StaminaComponent> ().OnUse += UseStamina;
        input.OnDodge += Dodge;
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

    }

    void Start()
    {
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

    public void Move()
    {
        if (player == null)// && player.isAttacking)
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

	public void UseStamina()
	{
        stamina -= dashCost;
	}

    void Dodge()
    {
        if (input.Attack) return;

        if (dodgeTime <= 0)
        {
            if (stamina >= dashCost)
            {

                if (direction == 1)
                {
                    rb.AddForce(Vector2.up * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                }
                else if (direction == 2)
                {
                    rb.AddForce(Vector2.left * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                }
                else if (direction == 3)
                {
                    rb.AddForce(Vector2.down * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                }
                else if (direction == 4)
                {
                    rb.AddForce(Vector2.right * dodgeSpeed, ForceMode2D.Force);
                    UseStamina();
                }
            }
        }
    }

    IEnumerator RegenerateStamina()
    {
        while (true)
        {
            stamina += 5;

            if (stamina > maxStamina)
                stamina = maxStamina;

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Footsteps()
    {
        while (true)
        {
            if(isMoving)
                SoundManager.instance.RandomizeSfx(step1, step2, step3);

            yield return new WaitForSeconds(walkTimer);
        }
    }


}
