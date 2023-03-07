using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    protected Animator animator;
    public Rigidbody2D body;
    public bool isDead;
    public bool isInteracting;

    public Item heldItem;
    public Transform heldItemTransform;

    public bool isGrounded;
    public bool isFalling;
    public float groundedTime;
    private float groundedTimer;
    public float groundDetectionDistance;
    public LayerMask groundLayer;
    public Transform groundDetectionCastTransform;

    protected virtual void Awake()
    {
        isDead = false;
        isInteracting = false;
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
        // check to see if character is on the ground
        if(!Physics2D.Raycast(groundDetectionCastTransform.position, Vector2.down, groundDetectionDistance, groundLayer)) { return false; }
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
        if(isGrounded || Mathf.Abs(body.velocity.y) < .01f) { return false; }
        else
        {
            groundedTimer = groundedTime;
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundDetectionCastTransform.position, groundDetectionCastTransform.position + (Vector3.down * groundDetectionDistance));
    }
    #endregion
}
