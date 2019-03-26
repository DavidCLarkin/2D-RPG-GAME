using UnityEngine;

public class PlayerAttack : StateMachineBehaviour
{
    private float timer;
    private Player character;

	 //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = stateInfo.length;
        character = animator.GetComponent<Player>();
        character.isAttacking = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    if(timer <= 0)
        {
            animator.SetBool("Attack", false);
            //animator.SetBool(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.isAttacking = false;

    }

}
