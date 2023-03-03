using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{

    public bool isTouchingObstacle;
    public State currentState;
    public Transform dialogueTransform;
    protected override void Awake()
    {
        base.Awake();
        isTouchingObstacle = false;
    }
    protected override void Update()
    {
        base.Update();
        HandleStateMachine();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();
        if(player != null)
        {
            // destroy the enemy
            if (player.isFalling) 
            {
                isDead = true;
            }
        }
    }
    public override void DestroyAnimationEvent()
    {
        
        base.DestroyAnimationEvent();
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }

    #region Obstacle Detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            isTouchingObstacle = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            isTouchingObstacle = false;
        }
    }
    #endregion
}
