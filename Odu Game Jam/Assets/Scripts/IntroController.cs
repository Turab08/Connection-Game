using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    void Start()
    {
        // Wait 7 seconds (5 for fade-in + 2 to let them read), then load Level 1
        Invoke("LoadFirstLevel", 7f);
    }

    void LoadFirstLevel()
    {
        // Replace "Level1" with the exact name of your first level scene
        SceneManager.LoadScene("Level1");
    }
}