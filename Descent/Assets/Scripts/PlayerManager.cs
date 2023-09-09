using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    LedgeGrab ledgeGrab;

    public Vector3 ledgeClimbPositionOffset;
    public bool isSneaking;
    public bool canCombo;

    public GameObject weaponCollider;
    public PlayerInventory playerInventory;

    protected override void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        ledgeGrab = GetComponentInChildren<LedgeGrab>();
        isSneaking = false;
        canCombo = false;
        weaponCollider.SetActive(false);
        base.Awake();
        if (flashlightTransform != null)
        {
            if (facingRight) { flashlightTransform.eulerAngles = new Vector3(0f, 0f, 0f); }
            else { flashlightTransform.eulerAngles = new Vector3(0f, 180f, 0f); }
        }
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
        inputHandler.sneakInput = false;
        inputHandler.useItemInput = false;
        inputHandler.attackInput = false;

        // make sure flashlight is turned around completely
        if (flashlightTransform != null)
        {
            if(flashlightTransform.rotation.eulerAngles.y == 180f || flashlightTransform.rotation.eulerAngles.y == 0f) { return; }
            if (flashlightTransform.rotation.eulerAngles.y <= 80f)
            {
                flashlightTransform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (flashlightTransform.rotation.eulerAngles.y >= 100f)
            {
                flashlightTransform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }
    public void HandleUseItem()
    {
        if (heldItem != null)
        {
            heldItem.Use();
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
                if (!facingRight && isGrounded && !isSneaking)
                {
                    animator.SetTrigger("Turn");
                }
            }
            else if (movement < -.01f)
            {
                animator.SetFloat("X", movement);
                if (facingRight && isGrounded && !isSneaking)
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

    public void SetCanCombo()
    {
        canCombo = true;
    }
    public void DisableCanCombo()
    {
        canCombo = false;
        //animator.SetBool("Attack Combo", false);
    }
    public void EnableWeaponCollider()
    {
        weaponCollider.SetActive(true);
    }
    public void DisableWeaponCollider()
    {
        weaponCollider.SetActive(false);
    }
    public void HandleAttack()
    {
        if (canCombo)
        {
            animator.SetBool("Attack Combo", true);
            return;
        }

        if (isInteracting || isLedgeHanging || isSneaking || !isGrounded || isDead || isFalling) { return; }

        animator.SetTrigger("Attack");
    }
    public void HandleAttackCombo()
    {
        if (!canCombo || isInteracting || isLedgeHanging || isSneaking || !isGrounded || isDead || isFalling) { return; }
        
    }
    public void HandleSneak()
    {
        isSneaking = !isSneaking;
        animator.SetBool("Sneaking", isSneaking);
        animator.SetTrigger("Sneak");
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
