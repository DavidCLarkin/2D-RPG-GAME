using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehaviour : StateMachineBehaviour
{
    private Enemy enemy;
    private GameObject player;
    private bool isPlayerToRight;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        player = GameManagerSingleton.instance.player;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerToRight = (player.transform.position.x > animator.transform.position.x);

        if (isPlayerToRight)
        {
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }
        else if (!isPlayerToRight)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }

	    if(enemy.state == Enemy.State.Attacking)
        {
            if (enemy.attackChosen == 1) // Spawn projectiles
            {
                if (isPlayerToRight)
                {
                    animator.SetBool("ChargeRight", true);
                    enemy.attackChosen = -1; // reset it so doesnt' repeat
                }
                else
                {
                    animator.SetBool("ChargeLeft", true);
                    enemy.attackChosen = -1;
                }
            }
            else if (enemy.attackChosen == 2) // Spawn tiles
            {
                animator.SetBool("Spawn", true);
                enemy.attackChosen = -1;
            }
        }

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
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
