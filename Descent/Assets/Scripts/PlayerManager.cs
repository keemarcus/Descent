using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    PlayerLocomotion playerLocomotion;

    private bool facingRight;

    protected override void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        facingRight = true;
        base.Awake();
    }
    protected override void Update()
    {
        inputHandler.TickInput(Time.deltaTime);

        if (!isInteracting)
        {
            // update animator
            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("Falling", isFalling);

            float movement = body.velocity.x;
            if (movement == 0f)
            {
                animator.SetBool("Running", false);
            }
            else
            {
                animator.SetFloat("X", movement);
                animator.SetBool("Running", true);
            }

            if (inputHandler.horizontal > .01f)
            {
                if (!facingRight)
                {
                    if (isGrounded)
                    {
                        animator.SetTrigger("Turn");
                    }

                    facingRight = true;
                }
            }
            else if (inputHandler.horizontal < -.01f)
            {
                if (facingRight)
                {
                    if (isGrounded)
                    {
                        animator.SetTrigger("Turn");
                    }

                    facingRight = false;
                }
            }
        }

        base.Update();
    }
    private void LateUpdate()
    {
        // reset all the input values
        inputHandler.jumpInput = false;
        inputHandler.useItemInput = false;
    }
    public void HandleUseItem()
    {
        if (heldItem != null)
        {
            heldItem.Use();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            // destroy the player
            if (!isFalling)
            {
                this.isDead = true;
            }
        }
    }

    public void PlayJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }
}
