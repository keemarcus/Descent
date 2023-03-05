using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    //Rigidbody2D body;
    PlayerManager playerManager;

    [Header("Movement Variables")]
    public float acceleration;
    public float moveSpeed;
    public float jumpHeight;
    
    

    private void Awake()
    {
        //body = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        
    }

    

    public void HandleMovement(float horizontal)
    {
        if (playerManager.isDead || playerManager.isLedgeHanging) 
        {
            playerManager.body.velocity = Vector2.zero;
            return; 
        }

        playerManager.body.velocity = new Vector2(horizontal * moveSpeed, playerManager.body.velocity.y);
    }

    public void HandleJump(float delta)
    {
        if (playerManager.isDead || playerManager.isLedgeHanging) { return; }

        playerManager.PlayJumpAnimation();
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * playerManager.body.gravityScale));
        playerManager.body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);        
    }
}
