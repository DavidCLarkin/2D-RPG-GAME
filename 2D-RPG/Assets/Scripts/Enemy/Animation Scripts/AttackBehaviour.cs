using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
	public float timer;
    private Enemy enemy;
    public string animationToEnd;
    public string animationToReturnTo;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		timer = stateInfo.length;
        enemy = animator.GetComponent<Enemy>();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		if(timer <= 0)
		{
			animator.SetBool (animationToEnd, false);
            //Instead of choosing idle, change this to decide random triggers - random other attack
			animator.SetTrigger(animationToReturnTo);
		}
		else
		{
			timer -= Time.deltaTime;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //enemy.attackRange = enemy.BASE_ATTACK_RANGE; // CURRENTLY USING ENABLE/DISABLE COLLIDERS
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
