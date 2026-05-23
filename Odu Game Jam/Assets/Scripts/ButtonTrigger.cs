using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{

    public DoorOpening door;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);

        if (door.isOpen == false) {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    door.isOpen = true;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.button);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.door);
                }
            }
            else {
                if (collision.gameObject.CompareTag("Box"))
                {
                    door.isOpen = true;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.button);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.door);
                }
            }
        }

    }
}
