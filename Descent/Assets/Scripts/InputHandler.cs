using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float moveAmount;

    public bool rightInput;
    public bool leftInput;
    public bool jumpInput;
    public bool useItemInput;

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
            inputActions.PlayerMovement.Right.performed += i => rightInput = true;
            inputActions.PlayerMovement.Right.canceled += i => rightInput = false;
            inputActions.PlayerMovement.Left.performed += i => leftInput = true;
            inputActions.PlayerMovement.Left.canceled += i => leftInput = false;
            inputActions.PlayerMovement.Jump.performed += i => jumpInput = true;
            inputActions.PlayerActions.UseItem.performed += i => useItemInput = true;
        }

        inputActions.Enable();
    }

    public void TickInput(float delta)
    {
        HandleMovementInput(delta);
        HandleJumpInput(delta);
        HandleUseItemInput(delta);
    }
    private void HandleUseItemInput(float delta)
    {
        if (useItemInput)
        {
            playerManager.HandleUseItem();
        }
    }

    private void HandleMovementInput(float delta)
    {
        if (!playerManager.isGrounded && !playerManager.isFalling) { return; }

        float moveModifier = 1f;
        if (playerManager.isFalling)
        {
            moveModifier = 0.5f;
        }

        if(rightInput ^ leftInput)
        {
            // handle movement
            if (rightInput)
            {
                horizontal = Mathf.Lerp(horizontal, 1f * moveModifier, delta * playerLocomotion.acceleration);
            }
            else
            {
                horizontal = Mathf.Lerp(horizontal, -1f * moveModifier, delta * playerLocomotion.acceleration);
            }
        }
        else
        {
            horizontal = 0f;
        }

        moveAmount = Mathf.Abs(horizontal);
        playerLocomotion.HandleMovement(horizontal);
    }

    private void HandleJumpInput(float delta)
    {
        if (!playerManager.isGrounded) { return; }

        if (jumpInput)
        {
            playerLocomotion.HandleJump(delta);
        }
    }
}
