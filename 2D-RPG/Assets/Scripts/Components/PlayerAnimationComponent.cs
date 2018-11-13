using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    private InputComponent input;
    private Animator anim;

    private bool walkingUp;
    private bool walkingDown;
    private bool walkingLeft;
    private bool walkingRight;

    private void Awake()
    {
        GetComponent<MovementComponent>().AnimateMovement += Animate;
        input = GetComponent<InputComponent>();
        anim = GetComponent<Animator>();
    }

    void Animate()
    {
        if(input != null && anim != null)
        {
            anim.SetBool("walkingUp", walkingUp);
            anim.SetBool("walkingDown", walkingDown);
            anim.SetBool("walkingLeft", walkingLeft);
            anim.SetBool("walkingRight", walkingRight);


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
}
