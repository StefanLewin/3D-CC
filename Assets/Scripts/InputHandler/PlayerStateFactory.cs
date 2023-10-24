using System.Collections.Generic;

public class PlayerStateFactory
{
    enum PlayerStates
    {
        idle,
        move,
        jump,
        fall,
        ground
    }

    private PlayerStateMachine _context;
    private Dictionary<PlayerStates, PlayerBaseState> _states = new();

    public PlayerStateFactory(PlayerStateMachine pCurrentContext)
    {
        _context = pCurrentContext;
        _states[PlayerStates.idle] = new PlayerIdleState(_context, this, "Idle");
        _states[PlayerStates.move] = new PlayerMoveState(_context, this, "Move");
        _states[PlayerStates.jump] = new PlayerJumpState(_context, this, "Jump");
        _states[PlayerStates.fall] = new PlayerFallState(_context, this, "Fall");
        _states[PlayerStates.ground] = new PlayerGroundState(_context, this, "Ground");
    }

    public PlayerBaseState Idle() { return _states[PlayerStates.idle]; }
    public PlayerBaseState Move() { return _states[PlayerStates.move]; }
    public PlayerBaseState Jump() { return _states[PlayerStates.jump]; }
    public PlayerBaseState Fall() { return _states[PlayerStates.fall]; }
    public PlayerBaseState Ground() { return _states[PlayerStates.ground]; }

}
