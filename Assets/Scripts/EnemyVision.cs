using UnityEngine;

// Gives an enemy a cone of vision. Detects the Player when it is inside the
// view radius, within the view angle, and not hidden behind an obstacle.
public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField]
    private float viewRadius = 10f;

    [SerializeField]
    [Range(0f, 360f)]
    private float viewAngle = 90f;

    [SerializeField]
    private float eyeHeight = 1.5f;

    [Header("Layers")]
    [SerializeField]
    private LayerMask targetMask;    // set to the Player's layer

    [SerializeField]
    private LayerMask obstacleMask;  // walls / props that block sight

    // True while the player is currently visible.
    public bool PlayerInSight { get; private set; }

    // Where the player was last seen (useful for a chaser to move toward).
    public Vector3 LastKnownPosition { get; private set; }

    void Update()
    {
        LookForPlayer();
    }

    private void LookForPlayer()
    {
        PlayerInSight = false;

        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;

        // 1) Anything on the target layer within the view radius?
        Collider[] targets = Physics.OverlapSphere(eyePosition, viewRadius, targetMask);

        foreach (Collider target in targets)
        {
            if (!target.CompareTag("Player"))
                continue;

            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

            // 2) Is it inside the view cone?
            if (Vector3.Angle(transform.forward, dirToTarget) > viewAngle / 2f)
                continue;

            // 3) Is the line of sight clear (not blocked by a wall/prop)?
            Vector3 targetEye = target.transform.position + Vector3.up * eyeHeight;
            float distToTarget = Vector3.Distance(eyePosition, targetEye);

            if (Physics.Raycast(eyePosition, (targetEye - eyePosition).normalized,
                                 distToTarget, obstacleMask))
                continue;

            // Player spotted!
            PlayerInSight = true;
            LastKnownPosition = target.transform.position;
            OnPlayerSpotted(target.transform);
            break;
        }
    }

    private void OnPlayerSpotted(Transform player)
    {
        Debug.Log($"{name} spotted the Player at {player.position}!");
    }

    // Draws the vision cone in the Scene view so you can see the range.
    void OnDrawGizmosSelected()
    {
        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;

        Gizmos.color = PlayerInSight ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(eyePosition, viewRadius);

        Vector3 leftEdge = DirFromAngle(-viewAngle / 2f);
        Vector3 rightEdge = DirFromAngle(viewAngle / 2f);

        Gizmos.DrawLine(eyePosition, eyePosition + leftEdge * viewRadius);
        Gizmos.DrawLine(eyePosition, eyePosition + rightEdge * viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees)
    {
        // Angle relative to the enemy's facing direction, on the XZ plane.
        float angle = angleInDegrees + transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f,
                           Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
