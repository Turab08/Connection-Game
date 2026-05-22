using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public bool isOpen = false;
    public Vector3 targetPosition;
    public float lerpSpeed = 5f;

    void Start()
    {
        targetPosition = transform.position + new Vector3(0, 4f, 0); // Example target position above the door
    }
    void Update()
    {
        if (isOpen)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }

    public void ResetDoor()
    {
        isOpen = false;
        transform.position = targetPosition - new Vector3(0, 4f, 0); // Reset to original position
    }
}