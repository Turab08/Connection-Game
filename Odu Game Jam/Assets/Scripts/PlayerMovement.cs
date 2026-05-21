using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FollowMouse followMouse;
    public Transform target;
    public float smoothTime;

    private Vector2 velocity = Vector2.zero;

    public bool isGrabbed;


    void Update()
    {
        if (followMouse.canGrab && Input.GetMouseButtonDown(0))
        {
            isGrabbed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
        }
    }

    void LateUpdate() {
        if (isGrabbed && target != null)
        {
            Vector2 targetPos = new Vector2(target.position.x, target.position.y);

            transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        }    
    }
}
