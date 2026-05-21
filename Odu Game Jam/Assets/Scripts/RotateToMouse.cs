using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public PlayerMovement playerMovement;
    void Update()
    {
        if (playerMovement.isGrabbed) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
