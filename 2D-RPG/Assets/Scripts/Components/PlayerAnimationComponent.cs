using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    private InputComponent input;
    private Animator anim;
    private Camera cam;

    private bool walkingUp;
    private bool walkingDown;
    private bool walkingLeft;
    private bool walkingRight;
    private bool attackDown;
    private bool attackLeft;
    private bool attackRight;
    private bool attackUp;
    private int facingDirection;

    private void Awake()
    {
        cam = Camera.main;
        GetComponent<MovementComponent>().AnimateMovement += Animate;
        input = GetComponent<InputComponent>();
        input.OnAttack += MouseDirectionAttack;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseDirectionAttack();
    }
    void Animate()
    {
        if(input != null && anim != null)
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
                walkingUp = true;
            else
                walkingUp = false;

            if (Input.GetKey(KeyCode.A))
                walkingLeft = true;
            else
                walkingLeft = false;

            if (Input.GetKey(KeyCode.S))
                walkingDown = true;
            else
                walkingDown = false;

            if (Input.GetKey(KeyCode.D))
                walkingRight = true;
            else
                walkingRight = false;
        }
    }

    public void MouseDirectionAttack()
    {
        Vector2 dir = cam.ScreenToViewportPoint((Input.mousePosition));
        //Debug.Log(dir);
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
