using System;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    private IAction currentAction;

    private string currentAction_debug;

    public void StartAction(IAction action)
    {
        if (currentAction == action) return;
        if (currentAction != null)
            currentAction.Cancel();

        currentAction_debug = action.ToString();
        currentAction = action;
    }

    public void CancelCurrentAction()
        => StartAction(null);
}