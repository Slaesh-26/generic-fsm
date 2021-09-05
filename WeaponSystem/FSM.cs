using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FiniteStateMachine : IStateSwitcher
    {
        private State _currentState;
        private State _previousState;
        private List<State> _availableStates;

        public void AddStates(State[] states)
        {
            this._availableStates.AddRange(states);
        }
        
        public void Update() 
        {
            _currentState.UpdateState(); 
        }
        
        public void SwitchState<T>() where T : State
        {
            State nextState = GetState<T>();
            MoveToState(nextState);
        }

        public void SwitchToPreviousState()
        {
            MoveToState(_previousState);
        }
        
        private State GetState<T>() where T : State
        {
            State state = _availableStates.Find(s => s is T);
    
            if (state == null)
            {
                Debug.Log("[FSM] State not found");
            }
            
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
    
    public abstract class State
    {
        protected IStateSwitcher _stateSwitcher;

        private float _exitTime;
        private Timer _timer;
        private bool _hasExitTime;

        public State(IStateSwitcher stateSwitcher)
        {
            this._stateSwitcher = stateSwitcher;
        }

        public State(IStateSwitcher stateSwitcher, float exitTime)
        {
            this._stateSwitcher = stateSwitcher;
            this._exitTime = exitTime;
            
            if (exitTime > 0) this._hasExitTime = true;
        }

        public virtual void EnterState()
        {
            if (_hasExitTime)
            {
                _timer = new Timer(_exitTime);
                _timer.TimerElapsed += _stateSwitcher.SwitchToPreviousState;
            }
        }

        public virtual void UpdateState()
        {
            _timer?.Tick(Time.deltaTime);
        }

        public virtual void ExitState()
        {
            _timer.TimerElapsed -= _stateSwitcher.SwitchToPreviousState;
            _timer = null;
        }
    }
}

