using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleState : BatState
{
    public bool isSleeping;

    public BatDiveState diveState;
    public override BatState Tick(BatManager batManager)
    {
        #region Player Detection
        if(batManager.player == null)
        {
            // look for player
            batManager.player = FindObjectOfType<PlayerManager>();
        }
        else
        {
            if(batManager.diveTimer <= 0f && (!batManager.player.isSneaking && Vector2.Distance(batManager.transform.position, batManager.player.transform.position) <= batManager.normalDetectionRange) || (batManager.player.isSneaking && Vector2.Distance(batManager.transform.position, batManager.player.transform.position) <= batManager.sneakingDetectionRange))
            {
                batManager.animator.SetFloat("X", batManager.player.transform.position.x - batManager.transform.position.x);
                batManager.animator.SetTrigger("Dive");
                return diveState;
            }
        }
        #endregion

        return this;
    }
}
