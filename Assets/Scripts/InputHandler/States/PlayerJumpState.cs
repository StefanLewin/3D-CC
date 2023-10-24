using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    bool isFalling = false;
    private float  _movementMultiplier = 5;
    public PlayerJumpState(PlayerStateMachine pContext, PlayerStateFactory pFactory, string name)
        : base(pContext, pFactory)
    {
        IsRootState = true;
        Name = name;        
    }

    public override void CheckSwitchStates()
    {
        if (IsGrounded() && isFalling)
        {
            SwitchState(Factory.Ground());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
        Context.PlayerRigidbody.AddForce(Vector3.up * Context.JumpForce, ForceMode.Impulse);
        Context.WalkSpeed += _movementMultiplier;
        Context.IsJumpPressed = false;
    }

    public override void ExitState()
    {
        isFalling = false;
        Context.WalkSpeed -= _movementMultiplier;
    }

    public override void FixedUpdateState()
    {
        if(Context.PlayerRigidbody.velocity.y < 0f)
        {
            isFalling = true;
            Context.PlayerRigidbody.velocity -= 8 * Physics.gravity.y * Time.fixedDeltaTime * Vector3.down;
        }
    }

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
