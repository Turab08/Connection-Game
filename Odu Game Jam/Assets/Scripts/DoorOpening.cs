using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    

    public void OpenDoor()
    {
        Debug.Log("Door Opened!");
        // Add your door opening logic here, such as playing an animation or disabling a collider
        // For example, you could disable the door's collider to allow passage:
        Collider2D doorCollider = GetComponent<Collider2D>();
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}