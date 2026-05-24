using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{   
    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Socket"))
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.plugIn);
                LevelLoader.Instance.LoadNextLevel();
            }

            if (collision.gameObject.CompareTag("Start"))
            {   
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.plugIn);
                }

                LevelLoader.Instance.LoadNextLevel();
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

