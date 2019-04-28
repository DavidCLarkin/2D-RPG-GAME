using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItemBehaviour : StateMachineBehaviour
{
    float timer = 0;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        timer = stateInfo.length;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.gameObject.GetComponent<DestructibleItem>().DropRandomItem(); // call method just before destroying
            Destroy(animator.gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
