using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : StateMachineBehaviour
{

    private HealthComponent health;
    private Skeleton enemy;
    private float timer;
    private Transform player;
    private bool isPlayerToTheRight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = stateInfo.length;
        health = animator.GetComponent<HealthComponent>();
        enemy = animator.GetComponent<Skeleton>();
        player = GameManagerSingleton.instance.player.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerToTheRight = (player.position.x >= animator.transform.position.x);

        float distance = Vector2.Distance(animator.transform.position, GameManagerSingleton.instance.player.transform.position);

        if (health.health <= 0)
        {
            animator.SetTrigger("Die");
        }

        if (enemy.state == Enemy.State.Moving)
        {
            if (isPlayerToTheRight)
            {
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", true);
            }
            else if (!isPlayerToTheRight)
            {
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", true);
            }
            
        }

        if (enemy.state == Enemy.State.Attacking)
        {
            if (isPlayerToTheRight)
            {
                //Debug.Log("player to right");
                animator.SetBool("AttackRight", true);
            }
            else if (!isPlayerToTheRight)
            {
                //Debug.Log("Player to left");
                animator.SetBool("AttackLeft", true);
            }
        }
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

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
