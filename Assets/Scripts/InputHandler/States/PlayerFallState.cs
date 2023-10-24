using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine pContext, PlayerStateFactory pFactory, string name)
        : base(pContext, pFactory)
    {
        IsRootState = true;
        Name = name;
        
    }

    public override void CheckSwitchStates()
    {
        if (IsGrounded())
        {
            SwitchState(Factory.Ground());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void ExitState() {}

    public override void FixedUpdateState() {}

    public override void InitializeSubState()
    {
        if (!Context.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            SetSubState(Factory.Move());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
