using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterManager character = animator.gameObject.GetComponent<CharacterManager>();
        if (character != null)
        {
            character.body.bodyType = RigidbodyType2D.Static;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterManager character = animator.gameObject.GetComponent<CharacterManager>();
        if (character != null)
        {
            character.body.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
