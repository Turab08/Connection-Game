using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{

    public DoorOpening door;
    Vector2 targetPosition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);

        if (door.isOpen == false) {
            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    door.isOpen = true;
                    targetPosition = transform.position + new Vector3(0.15f, 0);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.button);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.door);
                }
            }
            else {
                if (collision.gameObject.CompareTag("Box"))
                {
                    door.isOpen = true;
                    targetPosition = transform.position + new Vector3(0, -0.1f);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.button);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.door);
                }
            }
        }

    }

    void Update()
    {
        if (door.isOpen && SceneManager.GetActiveScene().name == "Level 3")
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 7 * Time.deltaTime);

        }
        else if (door.isOpen && SceneManager.GetActiveScene().name == "Level 4")
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 7 * Time.deltaTime);
        }
    }
}
