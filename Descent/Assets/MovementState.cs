using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // update the postion of the parent game object
        animator.gameObject.transform.parent.position += animator.gameObject.transform.localPosition; 
    }
}
