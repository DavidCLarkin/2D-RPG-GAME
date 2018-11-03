using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Boss : Enemy
{

    private bool walkingUp;
    private bool walkingDown;
    private bool walkingLeft;
    private bool walkingRight;
    private bool attacking;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        //rigi.mass = 1000f; // Higher mass = less knockback
        canBeKnockedBack = false;
        DAMAGE_DELAY = 1.5f;
        ATTACK_DELAY = 1.5f;
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
        //animate();
        Vector3 oldRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);
        Debug.Log("Timer: "+ANIMATION_TIMER);
    }

    //Fix Z position, bug in Freeze Rotation
    void FixedUpdate()
    {
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void DistanceChecking()
    {
        base.DistanceChecking();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void ChooseAttack()
    {
        base.ChooseAttack();
        
    }

    public override void StateDecision()
    {
        base.StateDecision();
    }

    public override void FollowTarget(Transform target)
    {
		if (anim.GetBool("attackDown") == true)
			return;
        base.FollowTarget(target);
    }

    private void animate()
    {
			
        anim.SetBool("walkingUp", walkingUp);
        anim.SetBool("walkingDown", walkingDown);
        anim.SetBool("walkingLeft", walkingLeft);
        anim.SetBool("walkingRight", walkingRight);
        anim.SetBool("attacking", attacking);
        if (Input.GetKey(KeyCode.W))
            walkingUp = true;
        else
            walkingUp = false;

        if (Input.GetKey(KeyCode.S))
            walkingDown = true;
        else
            walkingDown = false;

        if (Input.GetKey(KeyCode.A))
            walkingLeft = true;
        else
            walkingLeft = false;

        if (Input.GetKey(KeyCode.D))
            walkingRight = true;
        else
            walkingRight = false;

        if (ANIMATION_TIMER <= 0)
        {
            if (Vector2.Distance(player.position, transform.position) <= radius)
            {
                if (!attacking)
                {
                    ANIMATION_TIMER = anim.GetCurrentAnimatorStateInfo(0).length;
                    attacking = true;
                    //attacking = true;
                    //ANIMATION_TIMER = anim.GetCurrentAnimatorStateInfo(0).length;
                    //StartCoroutine(StopAnimation());
                    Debug.Log("Attacking");
                }
                // TODO ADD TIMER TO LET BOSS STOP ATTACKING
            }
            else
            {
                attacking = false;
            }
        }

    }

    IEnumerator StopAnimation()
    {
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
    
}
