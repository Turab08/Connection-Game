using UnityEngine;
using UnityEngine.UI;

public class CableStaminaWheel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LineRenderer cableLineRenderer;
    public BezierSpineMulti bezierSpineMulti;
    [SerializeField] Image fillImage;

    [Header("Cable Settings")]
    [SerializeField] float maxCableLength;
    [SerializeField] float lengthMagnifier = 1f;

    [Header("Colors")]
    [SerializeField] Color colorFull  = Color.white;
    [SerializeField] Color colorEmpty = Color.black;

    public Vector3 AnchorPosition => cableLineRenderer.GetPosition(0);
    public float MaxCableLength   => maxCableLength;
    public bool  IsAtLimit        { get; private set; }

    void Start()
    {
        if (bezierSpineMulti != null)
            maxCableLength = bezierSpineMulti.segmentCount * bezierSpineMulti.segmentLength * lengthMagnifier;

        if (fillImage != null)
            fillImage.fillAmount = 1f;
    }

    void Update()
    {
        if (cableLineRenderer == null || fillImage == null || bezierSpineMulti == null) return;

        // Recalculate each frame so the max stays correct after a rope cut
        maxCableLength = bezierSpineMulti.segmentCount * bezierSpineMulti.segmentLength * lengthMagnifier;

        int count = cableLineRenderer.positionCount;
        if (count < 2) return;

        // Direct anchor-to-endpoint distance is the correct tension metric.
        // Arc length is always ≈ maxCableLength due to Verlet segment constraints,
        // so it would pin the bar at 0 permanently.
        float directDistance = Vector3.Distance(
            cableLineRenderer.GetPosition(0),
            cableLineRenderer.GetPosition(count - 1)
        );

        float fill = Mathf.Clamp01(1f - (directDistance / maxCableLength));
        IsAtLimit = fill <= 0f;


        fillImage.fillAmount = fill;
        fillImage.color = IsAtLimit ? colorEmpty : colorFull;
    }
}
