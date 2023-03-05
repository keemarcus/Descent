using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    PlayerLocomotion playerLocomotion;
    public Transform hangingTransform;
    NewLedgeGrab ledgeGrab;

    private bool facingRight;

    public bool isLedgeHanging;

    public Vector3 ledgeClimbPositionOffset;

    //public bool ledgeDetected;
    protected override void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        ledgeGrab = GetComponentInChildren<NewLedgeGrab>();
        facingRight = false;
        isLedgeHanging = false;
        base.Awake();
    }
    protected override void Update()
    {
        inputHandler.TickInput(Time.deltaTime);

        

        if (!isInteracting)
        {
            // update animator
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

            
        }

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

    public void PlayJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    public void HandleLedgeClimb()
    {
        Debug.Log("Ledge Climb");
        isLedgeHanging = false;
        body.bodyType = RigidbodyType2D.Static;
        this.transform.position = (this.transform.position + ledgeGrab.transform.localPosition);
        animator.SetBool("Climb Up", true);
        animator.SetBool("Wall Hang", isLedgeHanging);
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
        Debug.Log("Ledge Drop");
        isLedgeHanging = false;
        animator.SetBool("Climb Up", false);
        animator.SetBool("Wall Hang", isLedgeHanging);
        ledgeGrab.Drop();
    }
}
