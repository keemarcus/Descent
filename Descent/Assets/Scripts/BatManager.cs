using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    [Header("Detection Range")]
    public float normalDetectionRange;
    public float sneakingDetectionRange;

    public BatState currentState;
    public float diveCooldown;
    public float moveSpeed;

    public Rigidbody2D body;
    public Animator animator;
    public PlayerManager player;

    public float diveTimer;
    private void Awake()
    {
        // look for player
        player = FindObjectOfType<PlayerManager>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        diveTimer = 0f;
    }
    private void Update()
    {
        if(diveTimer > 0f)
        {
            diveTimer -= Time.deltaTime;
        }
        currentState = currentState.Tick(this);  
    }

    private void OnDrawGizmos()
    {
        // draw detection range gizmo
        Gizmos.DrawWireSphere(transform.position, normalDetectionRange);
        Gizmos.DrawWireSphere(transform.position, sneakingDetectionRange);
    }
}
