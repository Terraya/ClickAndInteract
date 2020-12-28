using System.Collections;
using UnityEngine;

public class SimpleInteraction : Interactable
{
    [Header("Animation Time Setup")]
    [SerializeField] private float waitTimeBeforeAnim = 1f;
    [SerializeField] private float waitTimeAfterAnim = 1f;
    
    public override IEnumerator StartInteractWithPlayer()
    {
        yield return new WaitForSeconds(waitTimeBeforeAnim);
        //Do anything here, like walk to the Object and play Animation
        SetupAnimator();
        yield return new WaitForSeconds(waitTimeAfterAnim);
    }

    public override IEnumerator StartInteractWithAI()
    {
        //No implementation yet
        throw new System.NotImplementedException();
    }

    public override IEnumerator StopInteractWithPlayer()
    {
        SetAnimatorBack();
        throw new System.NotImplementedException();
    }

    public override IEnumerator StopInteractWithAI()
    {
        //No implementation yet
        throw new System.NotImplementedException();
    }
}