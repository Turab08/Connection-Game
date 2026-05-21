using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketDetection : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Socket"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
}

