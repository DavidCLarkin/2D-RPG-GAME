using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalkBehaviour : StateMachineBehaviour
{
    private Skeleton enemy;
    private Transform player;
    private bool isPlayerToTheRight;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Skeleton>();
        player = GameManagerSingleton.instance.player.transform;
	}

	 //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerToTheRight = (player.position.x >= animator.transform.position.x);

        if (isPlayerToTheRight)
        {
            animator.SetBool("WalkRight", true);
            animator.SetBool("WalkLeft", false);
        }
        else
        {
            animator.SetBool("WalkLeft", true);
            animator.SetBool("WalkRight", false);
        }
            
        //if (distance >= enemy.attackRange && distance < enemy.outOfRange) 
        if (enemy.state == Enemy.State.Attacking)
        {
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkRight", false);
            //animator.SetTrigger("Walk");

            if (player.position.x >= animator.transform.position.x)
            {
                //Debug.Log("player to right");
                animator.SetBool("AttackRight", true);
            }
            else if (player.position.x < animator.transform.position.x)
            {
                //Debug.Log("Player to left");
                animator.SetBool("AttackLeft", true);
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
