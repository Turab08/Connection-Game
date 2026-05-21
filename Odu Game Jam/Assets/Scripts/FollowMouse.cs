using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool canGrab;
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 10f; 

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            canGrab = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            canGrab = false;
        }
    }

}
