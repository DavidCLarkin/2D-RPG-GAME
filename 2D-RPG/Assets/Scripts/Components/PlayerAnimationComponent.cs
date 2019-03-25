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
    private string lastMovementDirection;
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
        lastMovementDirection = "Down";
    }

    private void Update()
    {
        //MouseDirectionAttack();
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
            anim.SetBool("knight_slice_left", attackLeft);
            anim.SetBool("knight_slice_right", attackRight);
            anim.SetBool("knight_slice_up", attackUp);

            if (input.Attack && facingDirection == 1)
                attackUp = true;
            else
                attackUp = false;

            if (input.Attack && facingDirection == 2)
                attackLeft = true;
            else
                attackLeft = false;

            if (input.Attack && facingDirection == 3)
                attackDown = true;
            else
                attackDown = false;

            if (input.Attack && facingDirection == 4)
                attackRight = true;
            else
                attackRight = false;


            if (Input.GetAxisRaw("Vertical") > 0)
            {
                walkingUp = true;
                lastMovementDirection = "Up";
            }
            else
                walkingUp = false;

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                walkingLeft = true;
                lastMovementDirection = "Left";
            }
            else
                walkingLeft = false;

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                walkingDown = true;
                lastMovementDirection = "Down";
            }
            else
                walkingDown = false;

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                walkingRight = true;
                lastMovementDirection = "Right";
            }
            else
                walkingRight = false;
        }
    }

    public void MouseDirectionAttack()
    {
        Vector2 dir = cam.ScreenToViewportPoint((Input.mousePosition));
        //Debug.Log(dir);
        //if (dir.y >= 0.65f || lastMovementDirection.Equals("Top"))
        if(lastMovementDirection.Equals("Up"))
        {
            //Debug.Log("Top");
            facingDirection = 1;
        }
        //else if (dir.x <= 0.5f && (dir.y > 0.35f && dir.y < 0.65f) || lastMovementDirection.Equals("Left"))
        else if(lastMovementDirection.Equals("Left"))
        {
            //Debug.Log("Left");
            facingDirection = 2;
        }
        //else if (dir.y <= 0.35f || lastMovementDirection.Equals("Down"))
        else if(lastMovementDirection.Equals("Down"))
        {
            //Debug.Log("Down");
            facingDirection = 3;
        }
        //else if (dir.x >= 0.5f && (dir.y > 0.35f && dir.y < 0.65f) || lastMovementDirection.Equals("Right"))
        else if(lastMovementDirection.Equals("Right"))
        {
            //Debug.Log("Right");
            facingDirection = 4;
        }
        //Debug.Log(dir);
    }
}
