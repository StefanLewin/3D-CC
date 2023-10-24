using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    Vector3 _forceDirection = Vector3.zero;

    public PlayerMoveState(PlayerStateMachine pContext, PlayerStateFactory pFactory, string name)
        :base(pContext, pFactory)
    {
        Name = name;
    }

    public override void CheckSwitchStates()
    {
        if (!Context.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void EnterState() {}

    public override void ExitState() {}

    public override void FixedUpdateState()
    {
        _forceDirection += Context.CurrentMovementInput.x * Context.WalkSpeed * GetCameraRight(Context.PlayerCamera);
        _forceDirection += Context.CurrentMovementInput.y * Context.WalkSpeed * GetCameraForward(Context.PlayerCamera);
        Context.PlayerRigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;

        //Make sure, that the maxSpeed is not exceeded
        Vector3 horizontalVelocity = Context.PlayerRigidbody.velocity;
        horizontalVelocity.y = 0;

        if (horizontalVelocity.sqrMagnitude > Context.MaxWalkSpeed * Context.MaxWalkSpeed)
            Context.PlayerRigidbody.velocity = horizontalVelocity.normalized * Context.MaxWalkSpeed + Vector3.up * Context.PlayerRigidbody.velocity.y;

        AdjustPlayerRotation();
    }

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    /// <summary>
    /// Helper method for Ground Movement.
    /// Gets the forward Vector of the camera, so the player will move in the direction where the camera is pointing to.
    /// </summary>
    /// <param name="p_playerCamera"></param>
    /// <returns></returns>
    private Vector3 GetCameraForward(Camera pPlayerCamera)
    {
        Vector3 forward = pPlayerCamera.transform.forward;

        forward.y = 0;
        return forward.normalized;
    }

    /// <summary>
    /// Helper method for Grond Movement.
    /// Gets the Right Vector of the camera, so the player will move in the direction where the camera is pointing to.
    /// </summary>
    /// <param name="p_playerCamera"></param>
    /// <returns></returns>
    private Vector3 GetCameraRight(Camera pPlayerCamera)
    {
        Vector3 right = pPlayerCamera.transform.right;

        right.y = 0;
        return right.normalized;
    }

    private void AdjustPlayerRotation()
    {
        Vector3 direction = Context.PlayerRigidbody.velocity;

        direction.y = 0f;

        if (Context.CurrentMovementInput.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            Context.PlayerRigidbody.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            Context.PlayerRigidbody.angularVelocity = Vector3.zero;
    }
}