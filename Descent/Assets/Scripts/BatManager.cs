using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    [Header("Detection Range")]
    public float normalDetectionRange;
    public float sneakingDetectionRange;

    [Header("Combat Stats")]
    public float maxHP;
    public float currentHP;
    public float damage;
    public bool isDead;

    public BatState currentState;
    public float diveCooldown;
    public float moveSpeed;

    public Rigidbody2D body;
    public Animator animator;
    public PlayerManager player;

    public float diveTimer;
    private void Awake()
    {
        isDead = false;

        // look for player
        player = FindObjectOfType<PlayerManager>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        diveTimer = 0f;
    }
    private void Update()
    {
        if (isDead)
        {
            Destroy(this.gameObject);
            return;
        }

        if(diveTimer > 0f)
        {
            diveTimer -= Time.deltaTime;
        }
        currentState = currentState.Tick(this);  
    }
    public void SetHP(float health)
    {
        currentHP = health;
    }
    public void DamageCharacter(float incomingDamage)
    {
        currentHP = Mathf.Clamp(currentHP - incomingDamage, 0f, maxHP);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            // damage the player
            player.DamageCharacter(this.damage);
            Debug.Log(player.gameObject.name + " took " + this.damage + " damage from " + this.gameObject.transform.parent.name);
            if (player.currentHP <= 0f)
            {
                player.isDead = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        // draw detection range gizmo
        Gizmos.DrawWireSphere(transform.position, normalDetectionRange);
        Gizmos.DrawWireSphere(transform.position, sneakingDetectionRange);
    }
}
