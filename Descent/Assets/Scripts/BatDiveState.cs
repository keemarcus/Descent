using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDiveState : BatState
{
    public BatState idleState;
    public Vector2 targetPosition;
    public Vector2 endPosition;
    public override BatState Tick(BatManager batManager)
    {
        //if(targetPosition != null && batManager.diveTimer <= 0f)
        //{
        //    batManager.body.MovePosition(Vector2.Lerp(batManager.body.position, targetPosition, batManager.moveSpeed * Time.deltaTime));
            

        //    if(Vector2.Distance(batManager.body.position,targetPosition) <= 0.1f)
        //    {
        //        if(endPosition != Vector2.zero)
        //        {
        //            targetPosition = endPosition;
        //            endPosition = Vector2.zero;
        //            return this;
        //        }
        //        else
        //        {
        //            batManager.diveTimer = batManager.diveCooldown;
        //            return idleState;
        //        }
        //    }
        //    else
        //    {
        //        return this;
        //    }
            
        //}

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

    public void SetTargetPosition(Vector2 targetPos)
    {
        Debug.Log(this.transform.position);
        targetPosition = targetPos;
        endPosition = targetPos + new Vector2(-1f, 1f)*((Vector2)this.transform.position - targetPos);
    }
}
