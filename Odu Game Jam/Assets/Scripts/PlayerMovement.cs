using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FollowMouse followMouse;
    public Transform target;
    public float smoothTime;

    [Header("Rope Limit")]
    public CableStaminaWheel cableStamina;
    [SerializeField] float tensionSnapForce = 3f;

    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;
    private bool _wasAtLimit;

    public bool isGrabbed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (followMouse.canGrab && Input.GetMouseButtonDown(0))
        {
            isGrabbed = true;
            rb.gravityScale = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
            rb.gravityScale = 1;
        }
    }

    void FixedUpdate()
    {
        if (!isGrabbed || target == null) return;

        Vector2 nextPosition = Vector2.SmoothDamp(rb.position, target.position, ref velocity, smoothTime);

        if (cableStamina != null)
        {
            Vector2 anchor = cableStamina.AnchorPosition;
            float   maxLen = cableStamina.MaxCableLength;
            Vector2 toNext = nextPosition - anchor;

            if (toNext.magnitude > maxLen)
            {
                Vector2 radial = toNext.normalized;

                // Hard stop: clamp position to rope radius
                nextPosition = anchor + radial * maxLen;

                // Kill outward SmoothDamp velocity so it doesn't fight the clamp
                float outwardSpeed = Vector2.Dot(velocity, radial);
                if (outwardSpeed > 0f)
                    velocity -= radial * outwardSpeed;

                // One-shot snap impulse for the "tense" jolt feel
                if (!_wasAtLimit)
                    rb.AddForce(-radial * tensionSnapForce, ForceMode2D.Impulse);

                _wasAtLimit = true;
            }
            else
            {
                _wasAtLimit = false;
            }
        }

        rb.MovePosition(nextPosition);
    }
}