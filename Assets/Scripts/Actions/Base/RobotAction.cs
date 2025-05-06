using System;
using UnityEngine;

public abstract class RobotAction
{
    public RobotAction() { }
    public RobotAction(string msg)
    {
        _actionName = msg;
    }
    protected string _actionName;

    public bool IsReady { get; private set; }
    public bool IsFinished { get; private set; }

    public IRobotActionable RobotActionable
    {
        get
        {
            return _robotActionable;
        }
        set
        {
            if(_robotActionable == null)
            {
                if (value is IRobotActionable)
                {
                    _robotActionable = value;
                }
            }
        }
    }
    private IRobotActionable _robotActionable;

    protected Action OnReadyAction;
    protected Action OnFinishAction;

    public virtual void ReadyAction() 
    { 
        OnReadyAction?.Invoke(); 
        IsReady = true; 
    }
    public abstract void PlayAction();
    public void FinishAction() { OnFinishAction?.Invoke(); IsFinished = true; }
}
