using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteManager : MonoBehaviour
{
    public float damage;
    public float fallRange;

    public Rigidbody2D body;
    public Animator animator;
    public PlayerManager player;

    private void Awake()
    {
        // look for player
        player = FindObjectOfType<PlayerManager>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        #region Player Detection
        if (player == null)
        {
            // look for player
            player = FindObjectOfType<PlayerManager>();
        }
        else
        {
            if(Mathf.Abs(this.transform.position.x - player.transform.position.x) <= fallRange)
            {
                animator.SetBool("Falling", true);
                body.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            animator.SetBool("Hit Ground", true);
            body.bodyType = RigidbodyType2D.Static;

            // damage the player
            player.DamageCharacter(this.damage);
            Debug.Log(player.gameObject.name + " took " + this.damage + " damage from " + this.gameObject.name);
            if (player.currentHP <= 0f)
            {
                player.isDead = true;
            }
        }else if (collision.gameObject.tag.Equals("Ground"))
        {
            animator.SetBool("Hit Ground", true);
            body.bodyType = RigidbodyType2D.Static;
        }
    }
    public virtual void DestroyAnimationEvent()
    {
        Destroy(this.gameObject);
    }
}
