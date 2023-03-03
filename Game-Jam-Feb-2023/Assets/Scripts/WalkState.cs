using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    [Header("Movement Variables")]
    public float moveSpeed;
    public string moveDirection;

    public State idleState;
    public override State Tick(EnemyManager enemyManager)
    {
        HandleDialogue(enemyManager);
        if (enemyManager.isFalling) { return this; }
        if (enemyManager.isTouchingObstacle) { return idleState; }

        float moveDirectionModifier = HandleMoveDirection(moveDirection);

        if (enemyManager.body.velocity != new Vector2(moveDirectionModifier * moveSpeed, 0))
        {
            enemyManager.body.velocity = new Vector2(moveDirectionModifier * moveSpeed, 0);
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
