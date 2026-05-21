using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Transform attachmentPlace;
    public LineRenderer lineRenderer;

    void Update()
    {
        lineRenderer.SetPosition(1, new Vector2(attachmentPlace.position.x, attachmentPlace.position.y));
    }
}
