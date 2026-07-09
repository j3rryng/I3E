using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    private readonly HashSet<Rigidbody> countedBodies = new HashSet<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || countedBodies.Contains(rb)) return;

        countedBodies.Add(rb);
        ScoreManager.Instance.AddScore(scoreValue);
        Debug.Log("Goal scored! Score: " + ScoreManager.Instance.Score);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
            countedBodies.Remove(rb);
    }
}