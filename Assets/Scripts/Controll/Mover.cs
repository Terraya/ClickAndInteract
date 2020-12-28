using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ActionScheduler))]
public class Mover : MonoBehaviour, IAction
{
    [SerializeField] private float maxSpeed = 6f;

    private NavMeshAgent agent;
    private ActionScheduler actionScheduler;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        actionScheduler = GetComponent<ActionScheduler>();
    }

    private void Update()
        => UpdateAnimator();

    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
        actionScheduler.StartAction(this);
        MoveTo(destination, speedFraction);
    }

    public bool CanMoveTo(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
        if (!hasPath) return false;
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        agent.destination = destination;
        agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        agent.isStopped = false;
    }

    private void UpdateAnimator()
    {
    }

    public void Cancel()
        => agent.isStopped = true;
    
}