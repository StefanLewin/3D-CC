using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine pCurrentContext, PlayerStateFactory pFactory, string name) 
        : base(pCurrentContext, pFactory)
    {
        IsRootState = true;
        Name = name;
        
    }

    public override void CheckSwitchStates()
    {
        if (Context.IsJumpPressed)
        {
            SwitchState(Factory.Jump());
        } 
        else if (!IsGrounded())
        {
            SwitchState(Factory.Fall());
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
        } else
        {
            SetSubState(Factory.Move());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }


}
