using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BatState : MonoBehaviour
{
    public abstract BatState Tick(BatManager batManager);
}
