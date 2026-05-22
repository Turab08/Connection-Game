using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Socket"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (collision.gameObject.CompareTag("Start"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (collision.gameObject.CompareTag("Exit"))
            {
                Application.Quit();
            }
        }
}

