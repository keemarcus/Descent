using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDiveState : BatState
{
    public BatState idleState;
    public override BatState Tick(BatManager batManager)
    {
        if(batManager.body.velocity == Vector2.zero)
        {
            batManager.diveTimer = batManager.diveCooldown;
            return idleState;
        }
        else
        {
            return this;
        }
    }

}
