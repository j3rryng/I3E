using UnityEngine;
using UnityEngine.AI;

// Drives a NavMeshAgent. It stays at its start position until its EnemyVision
// spots the Player. While the Player is in sight it chases; once the Player
// leaves its vision it walks back to where it started.
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyVision))]
public class Chaser : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyVision vision;

    private Vector3 startPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();

        // Remember where we began so we can return here later.
        startPosition = transform.position;
    }

    void Update()
    {
        if (vision.PlayerInSight)
        {
            // Player is visible -> chase its current position.
            agent.SetDestination(vision.LastKnownPosition);
        }
        else
        {
            // Player is not visible -> head back to the original position.
            agent.SetDestination(startPosition);
        }
    }
}
