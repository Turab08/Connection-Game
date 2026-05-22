using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{   
    public AudioManager audioManager;

    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Socket"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (collision.gameObject.CompareTag("Start"))
            {
                audioManager.PlugIn();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (collision.gameObject.CompareTag("Exit"))
            {
                audioManager.PlugIn();
                Application.Quit();
            }
        }
}

