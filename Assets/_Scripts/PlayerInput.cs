using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    private PlayerInputActions inputActions;

    public bool LeftClick   { get; private set; }
    public bool RightClick  { get; private set; }
    public Vector2 Pan   { get; private set; }
    public Vector2 Rotate   { get; private set; }
    public float Zoom   { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        inputActions = new PlayerInputActions();

        PlayerInputActions.PlayerActions player = inputActions.Player;
        
        player.LeftClick.performed += _ => LeftClick = true;
        player.LeftClick.canceled  += _ => LeftClick = false;

        player.RightClick.performed += _ => RightClick = true;
        player.RightClick.canceled  += _ => RightClick = false;

        player.Pan.performed += ctx => Pan = ctx.ReadValue<Vector2>();
        player.Pan.canceled  += ctx => Pan = Vector2.zero;

        player.Rotate.performed += ctx => Rotate = CheckRotate(ctx.ReadValue<Vector2>());
        player.Rotate.canceled  += ctx => Rotate = Vector2.zero;

        player.Zoom.performed += ctx => Zoom = ctx.ReadValue<float>();
        player.Zoom.canceled  += ctx => Zoom = 0;
    }

    private Vector2 CheckRotate(Vector2 ctx)
    {
        if (RightClick)
        {
            return ctx;
        }
        else
        {
            return Vector2.zero;
        }
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
