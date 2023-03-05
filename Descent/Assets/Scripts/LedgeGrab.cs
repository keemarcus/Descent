using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LedgeGrab : MonoBehaviour
{
    public List<Transform> ledgeGrabPoints;
    private PlayerManager player;

    private void Start()
    {
        foreach(Transform t in this.GetComponentsInChildren<Transform>())
        {
            if (t.position != this.transform.position)
            {
                ledgeGrabPoints.Add(t);
            }
        }

        //Debug.Log(this.transform.position);
        //Debug.Log(this.name);

        foreach(Transform ledgeGrabPoint in ledgeGrabPoints)
        {
            //Debug.Log("Ledge at :" + ledgeGrabPoint.localPosition);
        }

        player = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        if(player != null && PlayerInRangeToGrab(player.hangingTransform) && !player.isGrounded && !player.isInteracting)
        {
            //player.HandleLedgeGrab(this.transform.position);
        }
    }

    private bool PlayerInRangeToGrab(Transform playerTransform)
    {
        bool inRange = false;

        foreach(Transform ledgeGrabPoint in ledgeGrabPoints)
        {
            //Debug.Log(Vector2.Distance(ledgeGrabPoint.position, playerTransform.position));
            if(Vector2.Distance(ledgeGrabPoint.position, playerTransform.position) <= 1f)
            {
                inRange = true;
                break;
            }
        }

        return inRange;
    }
}
