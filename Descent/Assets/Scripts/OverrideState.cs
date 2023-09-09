using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterManager character = animator.gameObject.GetComponent<CharacterManager>();
        if(character != null)
        {
            character.isInteracting = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterManager character = animator.gameObject.GetComponent<CharacterManager>();
        if (character != null)
        {
            character.isInteracting = false;
        } 
    }
}
