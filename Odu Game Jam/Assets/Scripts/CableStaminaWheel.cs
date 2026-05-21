using UnityEngine;
using UnityEngine.UI;

public class CableStaminaWheel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LineRenderer cableLineRenderer;
    [SerializeField] Image fillImage;

    [Header("Cable Settings")]
    [SerializeField] float maxCableLength = 16f; 

    [Header("Colors")]
    [SerializeField] Color colorFull     = new Color(0.20f, 0.78f, 0.45f);
    [SerializeField] Color colorWarning  = new Color(0.94f, 0.62f, 0.15f);
    [SerializeField] Color colorCritical = new Color(0.88f, 0.29f, 0.29f);

    void Update()
    {
        if (cableLineRenderer == null || fillImage == null) return;


        Vector3 anchor  = cableLineRenderer.GetPosition(0);
        Vector3 plugTip = cableLineRenderer.GetPosition(1);

        float currentLength = Vector3.Distance(anchor, plugTip);
        float fill = Mathf.Clamp01(1f - (currentLength / maxCableLength));

        fillImage.fillAmount = fill;

        if (fill > 0.5f)
            fillImage.color = Color.Lerp(colorWarning, colorFull, (fill - 0.5f) * 2f);
        else
            fillImage.color = Color.Lerp(colorCritical, colorWarning, fill * 2f);
    }
}