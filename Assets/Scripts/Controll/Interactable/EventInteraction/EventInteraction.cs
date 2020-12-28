using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventInteraction : Interactable
{
    public UnityEvent onInteract;
    public UnityEvent onCancelInteract;

    public override IEnumerator StartInteractWithPlayer()
    {
        //Need to think about making the event time after and before available
        //and flexible as player movement and animation
        yield return new WaitForSeconds(1f);
        onInteract.Invoke();
    }

    public override IEnumerator StartInteractWithAI()
    {
        //No implementation yet 
        throw new NotImplementedException();
    }

    public override IEnumerator StopInteractWithPlayer()
    {
        //Need to remove animation in case its setup
        onCancelInteract.Invoke();
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator StopInteractWithAI()
    {
        //No implementation yet
        throw new NotImplementedException();
    }
}