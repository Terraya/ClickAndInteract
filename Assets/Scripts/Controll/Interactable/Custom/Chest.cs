using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Chest : Interactable
{
    [SerializeField] private GameObject movePoint = default;

    private Animation anim;

    public UnityEvent onChestOpen;
    public UnityEvent onChestClose;

    private void Start()
        => movePoint = !movePoint ? gameObject : movePoint;

    public override IEnumerator StartInteractWithPlayer()
    {
        var agent = InteractingEntity.GetComponent<NavMeshAgent>();
        if (!agent) yield break;

        agent.isStopped = false;
        agent.SetDestination(movePoint.transform.position);
        yield return new WaitUntil(() => !agent.pathPending);
        yield return new WaitUntil(() => agent.remainingDistance < 0.5);

        onChestOpen.Invoke();
    }

    public override IEnumerator StartInteractWithAI()
    {
        //No implementation yet
        throw new NotImplementedException();
    }

    public override IEnumerator StopInteractWithPlayer()
    {
        //Here we can close the chest in case we open it before
        print("Closing Chest - Player Stop interacting with Chest");
        yield break;
    }

    public override IEnumerator StopInteractWithAI()
    {
        throw new NotImplementedException();
    }
}