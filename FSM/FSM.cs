using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : IStateSwitcher
{
    private List<State> _availableStates;
    private State _currentState;
    private State _previousState;

    public void SwitchState<T>() where T : State
    {
        State nextState = GetState<T>();
        MoveToState(nextState);
    }

    public void SwitchToPreviousState()
    {
        MoveToState(_previousState);
    }

    public void AddStates(State[] states)
    {
        _availableStates.AddRange(states);
    }

    public void UpdateFSM()
    {
        _currentState.UpdateState();
    }

    private State GetState<T>() where T : State
    {
        State state = _availableStates.Find(s => s is T);

        if (state == null) Debug.Log("[FSM] State not found");

        return state;
    }

    private void MoveToState(State nextState)
    {
        _previousState = _currentState;

        _currentState?.ExitState();
        _currentState = nextState;
        _currentState.EnterState();
    }
}