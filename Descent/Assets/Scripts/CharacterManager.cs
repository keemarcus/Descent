using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    protected Animator animator;
    public Rigidbody2D body;
    public bool isDead;

    public Item heldItem;
    public Transform heldItemTransform;

    public bool isGrounded;
    public bool isFalling;
    public float groundedTime;
    private float groundedTimer;

    protected virtual void Awake()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        isFalling = HandleFallingDetection();
        isGrounded = HandleGroundedDetection(Time.deltaTime);

        // check to see if the character fell off the map
        if(isFalling && this.transform.position.y <= -2f)
        {
            isDead = true;
        }

        // update animator variables
        animator.SetBool("Is Dead", isDead);
    }
    public virtual void DestroyAnimationEvent()
    {
        if(heldItem != null)
        {
            heldItem.Drop();
        }
        
        Destroy(this.gameObject);
    }
    #region Grounded And Falling Detection
    private bool HandleGroundedDetection(float delta)
    {
        // if we're moving in the y direction we're not grounded
        if(Mathf.Abs(body.velocity.y) > .01f) { return false; }
        else
        {
            // check to see if we've been on the ground long enough
            if (groundedTimer <= 0f) { return true; }
            else
            {
                // tick the grounded timer
                groundedTimer -= delta;
                return false;
            }
        }
    }

    private bool HandleFallingDetection()
    {
        if(isGrounded || body.velocity.y >= 0) { return false; }
        else
        {
            groundedTimer = groundedTime;
            return true;
        }
    }
    #endregion
}
