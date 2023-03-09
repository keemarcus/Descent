using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    LedgeGrab ledgeGrab;

    public Vector3 ledgeClimbPositionOffset;

    protected override void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        ledgeGrab = GetComponentInChildren<LedgeGrab>();
        base.Awake();
    }
    protected override void Update()
    {
        inputHandler.TickInput(Time.deltaTime);

        // update animator
        HandleAnimator(body.velocity.x);

        ledgeGrab.SetDirection(facingRight);

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

    public void HandleAnimator(float movement)
    {
        if (isInteracting) { return; }

        animator.SetBool("Wall Hang", isLedgeHanging);

        if (isLedgeHanging)
        {
            if (facingRight)
            {
                animator.SetFloat("X", 1f);
            }
            else
            {
                animator.SetFloat("X", -1f);
            }
        }
        else
        {
            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("Falling", isFalling);
            animator.SetBool("Running", (movement != 0f));
            

            if (movement > .01f)
            {
                animator.SetFloat("X", movement);
                if (!facingRight && isGrounded)
                {
                    animator.SetTrigger("Turn");
                }
            }
            else if (movement < -.01f)
            {
                animator.SetFloat("X", movement);
                if (facingRight && isGrounded)
                {
                    animator.SetTrigger("Turn");
                }
            }
        }

    }
    public void PlayJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    public void SetCanClimb()
    {
        canClimb = true;
        if (!isLedgeHanging) { isLedgeHanging = true; }
    }
    public void HandleLedgeClimb()
    {
        if (!canClimb) { return; }
        isLedgeHanging = false;
        canClimb = false;
        body.bodyType = RigidbodyType2D.Static;
        this.transform.position = (this.transform.position + ledgeGrab.transform.localPosition);
        animator.SetBool("Climb Up", true);
        animator.SetBool("Wall Hang", false);
        ledgeGrab.Drop();
    }

    public void SetPostionLedgeClimb()
    {
        Vector3 posOffset = ledgeClimbPositionOffset;

        if (!facingRight)
        {
            posOffset.x *= -1f;
        }

        this.transform.position = this.transform.position + posOffset;
    }
    public void HandleLedgeDrop()
    {
        isLedgeHanging = false;
        animator.SetBool("Climb Up", false);
        animator.SetBool("Wall Hang", false);
        ledgeGrab.Drop();
    }
}
