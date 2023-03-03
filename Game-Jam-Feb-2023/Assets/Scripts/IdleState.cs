using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override State Tick(EnemyManager enemyManager)
    {
        HandleDialogue(enemyManager);
        if(enemyManager.body.velocity != Vector2.zero)
        {
            enemyManager.body.velocity = Vector2.zero;
        }
        return this;
    }
    public override void HandleDialogue(EnemyManager enemyManager)
    {
        if (dialogueTimer == -1f) { return; }

        if (dialogueShown)
        {
            MoveDialogue(enemyManager.dialogueTransform);
            if (dialogueTimer <= 0f)
            {
                HideDialogue();
                dialogueTimer = -1f;
            }
            else
            {
                dialogueTimer -= Time.deltaTime;
            }
        }
        else
        {
            PlayerManager player = FindObjectOfType<PlayerManager>();
            if (player != null && Vector2.Distance(player.transform.position, this.transform.position) <= dialogueDistance)
            {
                ShowDialogue();
            }
        }
    }
}
