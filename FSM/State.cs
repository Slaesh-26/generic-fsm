using UnityEngine;

public abstract class State
{
    private IStateSwitcher _stateSwitcher;

    public State(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();
}