using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class Pistol : Weapon
{
    protected override void InitFSM()
    {
        _fsm = new FiniteStateMachine();

        var idleState = new PistolIdleState(this, _fsm);
        var reloadState = new PistolReloadState(this, _fsm);
        var shootState = new PistolShootState(this, _fsm);

        _fsm.AddStates(new State[]
        {
            idleState, reloadState, shootState
        });
        
        _fsm.SwitchState<PistolIdleState>();
    }
}

public class PistolIdleState : State
{
    private Pistol _pistol;

    public PistolIdleState(Pistol pistol, IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        this._pistol = pistol;
    }

    public override void EnterState()
    {
        //anim starts playing idle
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _stateSwitcher.SwitchState<PistolShootState>();
        }
    }

    public override void ExitState()
    {
        //anim ends playing idle
    }
}

public class PistolReloadState : State
{
    private Pistol _pistol;

    public PistolReloadState(Pistol pistol, IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        this._pistol = pistol;
    }
}

class PistolShootState : State
{
    private Pistol _pistol;
    private Timer _timer;

    public PistolShootState(Pistol pistol, IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        this._pistol = pistol;
    }

    public override void EnterState()
    {
        _pistol.ShootRay(_pistol.firePoint.position, _pistol.firePoint.forward, _pistol.targetMask);

        _timer = new Timer(0.1f);
        _timer.TimerElapsed += _stateSwitcher.SwitchToPreviousState;
    }

    public override void UpdateState()
    {
        _timer.Tick(Time.deltaTime);
    }

    public override void ExitState()
    {
        _timer.TimerElapsed -= _stateSwitcher.SwitchToPreviousState;
        _timer = null;
    }
}