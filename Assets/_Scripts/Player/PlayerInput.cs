using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions inputActions;
    public PlayerInputActions.PlayerActions Actions { get; private set; }
    public bool Shifting { get; private set; }
    public Vector2 Rotate { get; private set; }
    public Vector2 Pointer {  get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        Actions = inputActions.Player;

        Actions.Rotate.performed += ctx => Rotate = ctx.ReadValue<Vector2>();
        Actions.Rotate.canceled += _ => Rotate = Vector2.zero;

        Actions.Pointer.performed += ctx => Pointer = ctx.ReadValue<Vector2>();
        Actions.Pointer.canceled += _ => Pointer = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
