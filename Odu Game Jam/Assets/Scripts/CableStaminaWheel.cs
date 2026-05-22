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
    [SerializeField] Color colorFull  = Color.white;
    [SerializeField] Color colorEmpty = Color.black;

    public Vector3 AnchorPosition => cableLineRenderer.GetPosition(0);
    public float MaxCableLength   => maxCableLength;
    public bool  IsAtLimit        { get; private set; }

    void Update()
    {
        if (cableLineRenderer == null || fillImage == null) return;

        // Use first and LAST position — works correctly with any number of segments
        Vector3 anchor  = cableLineRenderer.GetPosition(0);
        Vector3 plugTip = cableLineRenderer.GetPosition(cableLineRenderer.positionCount - 1);

        float currentLength = Vector3.Distance(anchor, plugTip);
        float fill = Mathf.Clamp01(1f - (currentLength / maxCableLength));

        IsAtLimit = fill <= 0f;

        fillImage.fillAmount = IsAtLimit ? 0f : fill;
        fillImage.color = IsAtLimit ? colorEmpty : colorFull;
    }
}