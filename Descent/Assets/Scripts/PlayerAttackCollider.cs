using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            // damage the enemy
            BatManager bat = collision.GetComponent<BatManager>();
            if(bat != null)
            {
                bat.DamageCharacter(this.damage);
                Debug.Log(bat.gameObject.transform.parent.name + " took " + this.damage + " damage from " + this.gameObject.transform.parent.name);
                if (bat.currentHP <= 0f)
                {
                    bat.isDead = true;
                }
            }
        }
    }
}
