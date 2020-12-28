using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IRaycastable
{
    //Delegate Event which get called after start Interacting
    public delegate void
        Interact(GameObject interactingEntity); //InteractingEntity is always the Entity which try to interact with this Interactable

    public Interact OnInteract;

    //Delegate Event which get called after stopping the Interaction
    public delegate void StopInteract();

    public StopInteract OnStopInteract;

    //Running Coroutine
    private Coroutine runningCoroutine;

    //This Entity is the one Interacting with this Interactable
    private GameObject interactingEntity;
    public GameObject InteractingEntity => interactingEntity;
    public void SetInteractingEntity(GameObject entity) => interactingEntity = entity;

    public void OnStartInteract(GameObject entity)
        => OnInteract?.Invoke(entity);

    public void OnCancelInteract()
        => OnStopInteract?.Invoke();

    private void Awake()
    {
        OnInteract += StartInteraction;
    }

    public virtual void StartInteraction(GameObject entity)
    {
        SetInteractingEntity(entity);
        if (entity.CompareTag("Player"))
        {
            runningCoroutine = StartCoroutine(StartInteractWithPlayer());
            return;
        }

        runningCoroutine = StartCoroutine(StartInteractWithAI());
    }

    public abstract IEnumerator StartInteractWithPlayer();
    public abstract IEnumerator StartInteractWithAI();
    public abstract IEnumerator StopInteractWithPlayer();
    public abstract IEnumerator StopInteractWithAI();

    public virtual void SetupAnimator()
    {
        Animator anim = interactingEntity.GetComponent<Animator>();
        //Need a more flexible way to setup as many parameters as wanted
    }

    public virtual void SetAnimatorBack()
    {
        Animator anim = interactingEntity.GetComponent<Animator>();
        //Need a more flexible way to setup as many parameters as wanted
    }

    public CursorType GetCursorType()
        => CursorType.Interact;

    public bool HandleRaycast(GameObject entity)
    {
        OnStartInteract(entity);
        return true;
    }

    public void CancelRaycast()
        => OnCancelInteract();
}