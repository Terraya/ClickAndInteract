using UnityEngine;

[RequireComponent(typeof(ActionScheduler))]
public class PlayerInteraction : MonoBehaviour, IAction
{
    private IRaycastable _target;
    private ActionScheduler _actionScheduler;
    
    private void Awake()
        => _actionScheduler = GetComponent<ActionScheduler>();

    public void InteractWithTarget(IRaycastable target)
    {
        _target = target;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _actionScheduler.StartAction(this);
            target.HandleRaycast(gameObject);
        }
    }

    public void Cancel()
    {
        if(_target == null) return;
        _target.CancelRaycast();
        _target = null;
    }
}