using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float moveAmount;
    
    public Vector2 movement;
    public bool jumpInput;
    public bool sneakInput;
    public bool useItemInput;
    public bool attackInput;
    public float selectItemInput;

    PlayerControls inputActions;

    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movement = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Jump.performed += i => jumpInput = true;
            inputActions.PlayerMovement.Sneak.performed += i => sneakInput = true;
            inputActions.PlayerActions.UseItem.performed += i => useItemInput = true;
            inputActions.PlayerActions.Attack.performed += i => attackInput = true;
            inputActions.PlayerActions.SelectItem.performed += inputActions => selectItemInput = inputActions.ReadValue<float>();
        }

        inputActions.Enable();
    }

    public void TickInput(float delta)
    {
        HandleMovementInput(delta);
        HandleAttackInput(delta);
        HandleJumpInput(delta);
        HandleSneakInput(delta);
        HandleUseItemInput(delta);
        HandleSelectItemInput(delta);
    }
    private void HandleAttackInput(float delta)
    {
        if (attackInput)
        {
            playerManager.HandleAttack();
        }
    }
    private void HandleUseItemInput(float delta)
    {
        if (useItemInput)
        {
            playerManager.HandleUseItem();
        }
    }
    private void HandleSneakInput(float delta)
    {
        if (sneakInput)
        {
            playerManager.HandleSneak();
        }
    }

    private void HandleMovementInput(float delta)
    {
        if (!playerManager.isGrounded && !playerManager.isFalling && !playerManager.isLedgeHanging) { return; }

        if (playerManager.isLedgeHanging)
        {
            if(movement.y >= .75f || (movement.x >= .75f && playerManager.facingRight) || (movement.x <= -.75f && !playerManager.facingRight))
            {
                playerManager.HandleLedgeClimb();
            }else if(movement.y <= -.75f || (movement.x >= .75f && !playerManager.facingRight) || (movement.x <= -.75f && playerManager.facingRight))
            {
                playerManager.HandleLedgeDrop();
            }

            return;
        }

        float moveModifier = 1f;
        if (playerManager.isFalling)
        {
            moveModifier = 0.5f;
        }

        if(Mathf.Abs(movement.x) <= .01f)
        {
            horizontal = 0f;
        }
        else
        {
            horizontal = Mathf.Lerp(horizontal, movement.x * moveModifier, delta * playerLocomotion.acceleration);
        }

        moveAmount = Mathf.Abs(horizontal);
        playerLocomotion.HandleMovement(horizontal);
    }

    private void HandleJumpInput(float delta)
    {
        if (!playerManager.isGrounded || playerManager.isInteracting || playerManager.isSneaking) { return; }

        if (jumpInput)
        {
            playerLocomotion.HandleJump(delta);
        }
    }

    private void HandleSelectItemInput(float delta)
    {
        //Debug.Log(selectItemInput);
        if(selectItemInput > 0) { playerManager.playerInventory.SelectItem(1); }
        else if (selectItemInput < 0) { playerManager.playerInventory.SelectItem(-1); }
    }
}
