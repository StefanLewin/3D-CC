using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine pContext, PlayerStateFactory pFactory, string name)
        : base(pContext, pFactory)
    {
        Name = name;
    }

    public override void CheckSwitchStates()
    {
        if (Context.IsMovementPressed)
        {
            SwitchState(Factory.Move());
        }
    }

    public override void EnterState() {}

    public override void ExitState() {}

    public override void FixedUpdateState() {}

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
