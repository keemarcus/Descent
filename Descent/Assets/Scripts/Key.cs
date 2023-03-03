using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public string doorCode;
    public override void Use()
    {
        // find any nearby doors
        Door [] doors = FindObjectsOfType<Door>();
        foreach(Door door in doors)
        {
            if(Vector2.Distance(this.transform.position, door.transform.position) <= door.openDistance && door.keyCode.Equals(doorCode))
            {
                door.Open();
                Destroy(this.gameObject);
            }
        }
    }
}
