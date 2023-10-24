using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerStateMachine : MonoBehaviour
{
    [field: SerializeField, Range(0.0f, 5.0f)] public float WalkSpeed { get; set; } = 1.0f;
    [field: SerializeField, Range(0.0f, 5.0f)] public float MaxWalkSpeed { get; set; } = 5.0f;
    [field: SerializeField, Range(0.0f, 50.0f)] public float JumpForce { get; set; } = 2.0f;
    
    [field: SerializeField] private Camera _playerCamera;
    [field: SerializeField] private Transform _rotationAnchor;
    public Rigidbody PlayerRigidbody { get; private set; }
    public bool IsMovementPressed { get; private set; }
    public bool IsJumpPressed { get; set; }
    public bool IsGrounded { get; private set; }
    public Vector2 CurrentMovementInput { get; set; }
    public PlayerBaseState CurrentState { get; set; }
    public PlayerStateFactory States { get; private set; }
    public Camera PlayerCamera { get; private set; }
    public Transform RotationAnchor { get; private set; }

    private ThirdPersonActionAsset PlayerActionsAsset { get; set; }
    private InputAction MoveAction { get; set; }
    private InputAction JumpAction { get; set; }
    
    [field:Header ("Debug")]
    [field: SerializeField] private DebugScriptableObject _debugScriptableObject;


    private void Awake()
    {
        PlayerActionsAsset = new ThirdPersonActionAsset();
        States = new PlayerStateFactory(this);

        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerCamera = _playerCamera;

        CurrentState = States.Ground();
        CurrentState.EnterState();
    }

    private void Update()
    {        
        CurrentState.UpdateStates();
        UpdateDebug();
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    private void OnEnable()
    {
        MoveAction = PlayerActionsAsset.Player.Move;
        JumpAction = PlayerActionsAsset.Player.Jump;

        JumpAction.started += OnJumpInput;
        JumpAction.canceled += OnJumpInput;
        MoveAction.performed+= OnMoveInput;
        MoveAction.canceled+= OnMoveInput;

        PlayerActionsAsset.Player.Enable();

    }

    private void OnDisable()
    {
        PlayerActionsAsset.Player.Disable();
    }

    void OnMoveInput(InputAction.CallbackContext pCallbackContext) 
    { 
        CurrentMovementInput = pCallbackContext.ReadValue<Vector2>();
        IsMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
    }
        
    void OnJumpInput(InputAction.CallbackContext pCallbackContext)
    {
        IsJumpPressed = pCallbackContext.ReadValueAsButton();
    }

    void UpdateDebug()
    {
        _debugScriptableObject.rootState = "Root State: " + CurrentState.Name;
        _debugScriptableObject.substate = "Sub State: " + CurrentState.SubName;
    }
}