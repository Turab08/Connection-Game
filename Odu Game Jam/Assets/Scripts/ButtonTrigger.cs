using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{

    public DoorOpening door;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("Button Triggered!");
            door.isOpen = true;
        }
    }
}
