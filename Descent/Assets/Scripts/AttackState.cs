using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerManager player = animator.gameObject.GetComponent<PlayerManager>();
        if (player != null)
        {
            player.canCombo = false;
            animator.SetBool("Attack Combo", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerManager player = animator.gameObject.GetComponent<PlayerManager>();
        if (player != null)
        {
            player.canCombo = false;
        }
    }
}
