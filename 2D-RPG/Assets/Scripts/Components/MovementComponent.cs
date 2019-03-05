using System;
using System.Collections;
using UnityEngine;

public class MovementComponent : MonoBehaviour, IMoveable
{
    private InputComponent input;
	private MovementComponent movement;
    private Rigidbody2D rigidbody;
    private Player player;

    public float dodgeSpeed;
    public float startDodgetime;
    public float dodgeTime;
	public int dashCost = 30;
    private int direction;

    public int maxStamina;
    public int stamina;

    [SerializeField]
    private float speed;
    public event Action AnimateMovement = delegate { };

    private void Awake()
    {
        direction = 1;
        stamina = maxStamina;
        input = GetComponent<InputComponent>();
		movement = GetComponent<MovementComponent> ();
		GetComponent<StaminaComponent> ().OnUse += UseStamina;
        input.OnDodge += Dodge;
        rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

        StartCoroutine(RegenerateStamina());
    }

    // Update is called once per frame
    void Update ()
    {
        AnimateMovement();
        Move();

        if (Input.GetAxisRaw("Vertical") > 0)
            direction = 1;
        if (Input.GetAxisRaw("Horizontal") < 0)
            direction = 2;
        if (Input.GetAxisRaw("Vertical") < 0)
            direction = 3;
        if (Input.GetAxisRaw("Horizontal") > 0)
            direction = 4;

        //Debug.Log(direction);
    }

    public void Move()
    {
        if (player != null && player.isAttacking)
            return;

        rigidbody.velocity = new Vector2(input.Horizontal * speed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, input.Vertical * speed);
    }

	public void UseStamina()
	{
        stamina -= dashCost;
	}

    void Dodge()
    {
        if (dodgeTime <= 0)
        {
            if (stamina >= dashCost)
            {
                if (direction == 1)
                {
                    rigidbody.velocity = Vector2.up * dodgeSpeed;
                    UseStamina();
                }
                else if (direction == 2)
                {
                    rigidbody.velocity = Vector2.left * dodgeSpeed;
                    UseStamina();
                }
                else if (direction == 3)
                {
                    rigidbody.velocity = Vector2.down * dodgeSpeed;
                    UseStamina();
                }
                else if (direction == 4)
                {
                    rigidbody.velocity = Vector2.right * dodgeSpeed;
                    UseStamina();
                }
            }
        }

        /*
        if (player != null && player.isAttacking)
            return;
        if (GetComponent<StaminaComponent>().stamina < 30)
            return;

        if (direction == 0)
        {
            if(Input.GetAxisRaw("Vertical") > 0 && input.Dodge)
                direction = 1;
            if(Input.GetAxisRaw("Horizontal") < 0 && input.Dodge)
                direction = 2;
            if(Input.GetAxisRaw("Vertical") < 0 && input.Dodge)
                direction = 3;
            if(Input.GetAxisRaw("Horizontal") > 0 && input.Dodge)
                direction = 4;
            /*for (int i = 0; i < input.keys.Length; i++)
            {
                if (Input.GetKey(input.keys[i]) && input.Dodge)
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
				UseStamina ();
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
    }*/
    }

    IEnumerator RegenerateStamina()
    {
        while (true)
        {
            stamina += 5;

            if (stamina > maxStamina)
                stamina = maxStamina;

            yield return new WaitForSeconds(1);
        }
    }

}
