using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Key Attributes")]
    public string keyCode;
    public float openDistance;

    Collider2D lockedCollider;
    private void Awake()
    {
        lockedCollider = GetComponent<Collider2D>();
    }
    public void Open()
    {
        Debug.Log("Door is unlocked");
        lockedCollider.enabled = false;
    }
}
