using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerStateMachine Context { get; private set; }
    protected PlayerStateFactory Factory { get; private set; }
    protected ThirdPersonActionAsset _action;
    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;
    protected bool IsRootState { get; set; } = false;

    public string Name { get; protected set; }
    public string SubName { get; private set; } = "isSub";

    public PlayerBaseState(PlayerStateMachine pCurrentContext, PlayerStateFactory pFactory) {
        Context = pCurrentContext;
        Factory = pFactory;
    }

    public abstract void EnterState();

    public abstract void FixedUpdateState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        _currentSubState?.UpdateState();
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
        _currentSubState?.FixedUpdateState();
    }

    protected void SwitchState(PlayerBaseState pNewState) 
    {
        ExitState();
        pNewState.EnterState();

        if (IsRootState)
        {
            Context.CurrentState = pNewState;
        }
        else if(_currentSuperState != null)
        {
            _currentSuperState.SetSubState(pNewState);
        } else
        {
            Debug.Log("Komische Fehler");
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperSate) 
    {
        _currentSuperState = newSuperSate;
    }

    protected void SetSubState(PlayerBaseState newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
        SubName = newSubState.Name;
    }

    protected bool IsGrounded()
    {
        Ray ray = new(Context.PlayerRigidbody.transform.position, Vector3.down);
        return Physics.Raycast(ray, out _, 1);
    }
}
