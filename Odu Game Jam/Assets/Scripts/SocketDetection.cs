using UnityEngine;

public class SocketDetection : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Socket"))
            {
                Debug.Log("Player detected in socket!");
                
            }
        }
}

