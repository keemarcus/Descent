using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public CharacterManager holder;
    public BoxCollider2D physicsCollider;
    public BoxCollider2D triggerCollider;
    private void Awake()
    {
        BoxCollider2D [] colliders = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                physicsCollider = collider;
            }
            else
            {
                triggerCollider = collider;
            }
        }
    }
    public abstract void Use();

    private void Update()
    {
        if(holder != null)
        {
            this.transform.position = holder.heldItemTransform.position;

            if (this.GetComponent<SpriteRenderer>().enabled == true && holder.GetComponent<EnemyManager>())
            {
                
            }
        }
        else if(this.GetComponent<SpriteRenderer>().enabled == false)
        {
            
        }
    }

    public void Pickup(Collider2D character)
    {
        PlayerManager player = character.GetComponent<PlayerManager>();
        if (player != null && player.heldItem == null)
        {
            physicsCollider.enabled = false;
            this.holder = player;
            player.heldItem = this;
        }
        else
        {
            EnemyManager enemy = character.GetComponent<EnemyManager>();
            if (enemy != null && enemy.heldItem == null)
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                physicsCollider.enabled = false;
                this.holder = enemy;
                enemy.heldItem = this;
            }
        }
    }

    public void Drop()
    {
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        physicsCollider.enabled = true;
        
        if(this.holder != null)
        {
            holder.heldItem = null;
            this.holder = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup(collision);
    }
}
