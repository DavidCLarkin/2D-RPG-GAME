using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGhostBehaviour : StateMachineBehaviour
{
    private HealthComponent health;
    private Ghost enemy;
    private float timer;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = stateInfo.length;
        health = animator.GetComponent<HealthComponent>();
        enemy = animator.GetComponent<Ghost>();
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (health.health <= 0)
        {
            animator.SetTrigger("Die");
        }

        //if (distance >= enemy.attackRange && distance < enemy.outOfRange) 
        if(enemy.state == Enemy.State.Attacking)
        {
            if(enemy.ATTACK_TIMER <= 0.3)
                animator.SetBool("Attack", true);
        }

        //if (enemy.state == Enemy.State.Moving)
        //{
         //   animator.SetTrigger("Flee");
        //}
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
