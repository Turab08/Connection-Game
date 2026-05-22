using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierSpineMulti : MonoBehaviour
{
    [Header("Attachments")]
    public Transform anchor;
    public Transform endTarget;

    [Header("Rope Settings (Verlet)")]
    public int segmentCount = 72;
    public float segmentLength = 0.25f;
    public Vector2 gravity = new Vector2(0, -9.81f);
    
    [Tooltip("Higher number = stiffer rope. 50 is good.")]
    public int constraintIterations = 50;
    
    [Header("Collision")]
    [Tooltip("Keep this small! (e.g. 0.1). This is the physical radius, not visual width.")]
    public float ropeThickness = 0.1f;
    public LayerMask colliderMask = ~0;
    public Transform[] ignoreObjects;

    [Header("Cutting Logic")]
    public LayerMask scissorsMask;
    public bool isCut = false;
    private bool _isBroken = false;

    private LineRenderer _lr;
    private List<RopeSegment> _segments = new List<RopeSegment>();

    public class RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;
        
        public RopeSegment(Vector2 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }

    void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.useWorldSpace = true;
        
        Vector2 startPos = anchor.position;
        for (int i = 0; i < segmentCount; i++)
        {
            _segments.Add(new RopeSegment(startPos));
            startPos.y -= segmentLength;
        }
    }

    void FixedUpdate()
    {
        if (anchor == null || endTarget == null || isCut) return;

        
        SimulatePhysics();

        if (!_isBroken)
        {
            CheckForCutting();
        }
        
        ApplyConstraints();
    }

    void Update()
    {
        DrawRope();
    }

    private void SimulatePhysics()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            RopeSegment seg = _segments[i];
            Vector2 velocity = seg.posNow - seg.posOld;
            seg.posOld = seg.posNow;
            seg.posNow += velocity;
            seg.posNow += gravity * Time.fixedDeltaTime;
        }
    }

    private void CheckForCutting()
    {
        // Loop through all segments to see if any are overlapping a scissor collider
        for (int j = 0; j < segmentCount; j++)
        {
            // Use the same ropeThickness you used for walls
            Collider2D scissorHit = Physics2D.OverlapCircle(_segments[j].posNow, ropeThickness, scissorsMask);
            
            if (scissorHit != null)
            {
                HandleRopeCut(j);
                break; 
            }
        }
    }   

    private void HandleRopeCut(int cutIndex)
    {
        if (_isBroken) return; // Prevent multiple cuts in one frame
        _isBroken = true;

        // 1. Remove all segments from the cut point to the end
        // This makes the rope visually "end" where the scissors hit
        if (cutIndex < _segments.Count - 1)
        {
            _segments.RemoveRange(cutIndex + 1, _segments.Count - (cutIndex + 1));
        }

        // 2. Update the segment count for the LineRenderer
        segmentCount = _segments.Count;
    }

    private void ApplyConstraints()
    {
        for (int i = 0; i < constraintIterations; i++)
        {
            // 1. Distance Constraint
            for (int j = 0; j < segmentCount - 1; j++)
            {
                RopeSegment seg1 = _segments[j];
                RopeSegment seg2 = _segments[j + 1];

                float currentDistance = Vector2.Distance(seg1.posNow, seg2.posNow);
                float error = Mathf.Abs(currentDistance - segmentLength);
                
                Vector2 changeDir = Vector2.zero;
                if (currentDistance > segmentLength)
                    changeDir = (seg1.posNow - seg2.posNow).normalized;
                else if (currentDistance < segmentLength)
                    changeDir = (seg2.posNow - seg1.posNow).normalized;

                Vector2 changeAmount = changeDir * error;

                if (j != 0) 
                {
                    seg1.posNow -= changeAmount * 0.5f;
                    seg2.posNow += changeAmount * 0.5f;
                }
                else
                {
                    seg2.posNow += changeAmount; 
                }
            }

            // 2. Anchor Constraint
            _segments[0].posNow = anchor.position;

            // ONLY pin the endTarget if the rope is NOT broken
            if (!_isBroken)
            {
                _segments[segmentCount - 1].posNow = endTarget.position;
            }

            // 3. Collision Constraint (UPDATED to prevent fast-pull clipping)
            for (int j = 1; j < segmentCount - 1; j++)
            {
                RopeSegment seg = _segments[j];
                
                // STEP A: Anti-Tunneling (Continuous Collision Detection)
                // We check how far it moved. If it moved really fast this frame, we sweep the area to catch walls it passed through.
                Vector2 moveDelta = seg.posNow - seg.posOld;
                float moveDist = moveDelta.magnitude;

                if (moveDist > ropeThickness * 0.5f) 
                {
                    RaycastHit2D sweepHit = Physics2D.CircleCast(seg.posOld, ropeThickness, moveDelta.normalized, moveDist, colliderMask);
                    if (sweepHit.collider != null && !IsIgnored(sweepHit.collider))
                    {
                        // Stop it from passing through! Snap it to the surface it hit.
                        seg.posNow = sweepHit.point + (sweepHit.normal * ropeThickness);
                    }
                }

                // STEP B: Static Collision (Resting Push-out)
                // Standard check to gently push it out if it's resting against a wall
                Collider2D overlapHit = Physics2D.OverlapCircle(seg.posNow, ropeThickness, colliderMask);

                
                if (overlapHit != null && !IsIgnored(overlapHit))
                {
                    Vector2 closestPoint = overlapHit.ClosestPoint(seg.posNow);
                    
                    if (Vector2.Distance(seg.posNow, closestPoint) > 0.001f)
                    {
                        Vector2 pushDirection = (seg.posNow - closestPoint).normalized;
                        seg.posNow = closestPoint + (pushDirection * ropeThickness);
                    }
                    else
                    {
                        // Failsafe: if it gets perfectly trapped inside an object, nudge it slightly
                        seg.posNow += (Vector2)Random.insideUnitCircle.normalized * 0.05f; 
                    }
                }
            }
        }
    }

    private void DrawRope()
    {
        _lr.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            _lr.SetPosition(i, _segments[i].posNow);
        }
    }

    private bool IsIgnored(Collider2D col)
    {
        if (col.transform == anchor || col.transform == endTarget || col.transform.IsChildOf(anchor) || col.transform.IsChildOf(endTarget))
            return true;

        if (ignoreObjects != null)
        {
            foreach (var ignoredTransform in ignoreObjects)
            {
                if (ignoredTransform != null && col.transform.IsChildOf(ignoredTransform))
                    return true;
            }
        }
        return false;
    }
}