using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private float forceAmount = 8f;
    [SerializeField] private float upwardBoost = 0.3f; // adds a bit of lift to the kick
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;

    private Rigidbody rb;
    private Transform playerTransform;
    private bool playerInRange = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            KickBall();
        }
    }

    private void KickBall()
    {
        Vector3 direction = (transform.position - playerTransform.position);
        direction.y = 0f; // ignore height difference, kick horizontally
        direction.Normalize();
        direction += Vector3.up * upwardBoost;

        rb.linearVelocity = Vector3.zero;      // reset current motion so each kick feels consistent
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(direction * forceAmount, forceMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;
        }
    }
}