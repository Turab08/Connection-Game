using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public FollowMouse followMouse;
    public Transform target;
    public float smoothTime;

    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;

    public bool isGrabbed;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if (followMouse.canGrab && Input.GetMouseButtonDown(0))
        {
            isGrabbed = true;
            rb.gravityScale = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
            rb.gravityScale = 1;
        }
    }

    // Changed from LateUpdate to FixedUpdate so it syncs with the physics engine
    void FixedUpdate() 
    {
        if (isGrabbed && target != null)
        {
            // Calculate where we WANT to go
            Vector2 nextPosition = Vector2.SmoothDamp(rb.position, target.position, ref velocity, smoothTime);
            
            // Tell the Rigidbody to move there (This will stop if it hits a wall!)
            rb.MovePosition(nextPosition);
        }    
    }
}