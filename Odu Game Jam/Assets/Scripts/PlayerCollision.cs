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
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.plugIn);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (collision.gameObject.CompareTag("Exit"))
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.plugIn);
                }
                Application.Quit();
            }
        }
}

