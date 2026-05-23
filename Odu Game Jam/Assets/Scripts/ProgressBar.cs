using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image[] bars;

    [SerializeField] private Color completedColor = Color.black;
    [SerializeField] private Color currentColor   = Color.white;
    [SerializeField] private Color upcomingColor  = new(1f, 1f, 1f, 0.15f);

    void Start()
    {
        // buildIndex 0 is assumed to be the main menu, so levels start at 1.
        // Subtract 1 so level 1 → bar[0], level 2 → bar[1], etc.
        int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;

        for (int i = 0; i < bars.Length; i++)
        {
            if (bars[i] == null) continue;

            if (i < levelIndex)
                bars[i].color = completedColor;
            else if (i == levelIndex)
                bars[i].color = currentColor;
            else
                bars[i].color = upcomingColor;
        }
    }
}
