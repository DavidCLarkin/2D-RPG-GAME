using System;
using UnityEngine;

public class MovementComponent : MonoBehaviour, IMoveable
{
    private InputComponent input;
    private Rigidbody2D rigidbody;
    private Player player;

    public float dodgeSpeed;
    public float startDodgetime;
    public float dodgeTime;
    private int direction;

    [SerializeField]
    private float speed;
    public event Action AnimateMovement = delegate { };

    private void Awake()
    {
        direction = 1;
        input = GetComponent<InputComponent>();
        input.OnDodge += Dodge;
        rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update ()
    {
        AnimateMovement();
        Move();
    }

    public void Move()
    {
        if (player != null && player.isAttacking)
            return;

        rigidbody.velocity = new Vector2(input.Horizontal * speed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, input.Vertical * speed);
    }

    void Dodge()
    {
        if (player != null && player.isAttacking)
            return;

        if (direction == 0)
        {
            for (int i = 0; i < input.keys.Length; i++)
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
