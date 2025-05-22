using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]


    [Header("Settings")]
    [SerializeField] private float defaultPanSpeed = 20f;
    [SerializeField] private float shiftPanSpeed = 50f;
    [SerializeField] private float cameraRotationSpeed = 5f;
    [SerializeField] private float cameraRotationLerpSpeed = 5f;
    [SerializeField] private int minCameraPitch = 1;
    [SerializeField] private int maxCameraPitch = 89;
    [SerializeField] private float cameraZoomSpeed = 375f;
    [SerializeField] private float cameraZoomLerpSpeed = 10f;
    [SerializeField] private int cameraMinZoom = 2;
    [SerializeField] private int cameraMaxZoom = 100;

    [HideInInspector] public Camera playerCamera;

    private const float cameraPositionLerpSpeed = 10f;

    private GameObject playerTarget;
    private GameObject cameraTarget;

    private float cameraPanSpeed;
    private float yaw;
    private float pitch;
    private float smoothYaw;
    private float smoothPitch;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerTarget = new GameObject("Player Target");
        cameraTarget = new GameObject("Camera Target");
        cameraTarget.transform.parent = transform;

        PlayerManager.Instance.playerInput.Actions.RightClick.performed += UpdatePlayerTargetRotation;
        PlayerManager.Instance.playerInput.Actions.Pan.performed += UpdatePlayerTargetPosition;
        PlayerManager.Instance.playerInput.Actions.Shift.started += ShiftPanSpeed;
        PlayerManager.Instance.playerInput.Actions.Shift.canceled += DefaultPanSpeed;
        PlayerManager.Instance.playerInput.Actions.RightClick.started += LockCursor;
        PlayerManager.Instance.playerInput.Actions.RightClick.canceled += UnlockCursor;
        PlayerManager.Instance.playerInput.Actions.Zoom.performed += UpdateCameraTarget;
    }

    private void Update()
    {
        UpdateCamera();
        UpdatePlayer();
    }

    private void LockCursor(InputAction.CallbackContext _)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor(InputAction.CallbackContext _)
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void ShiftPanSpeed(InputAction.CallbackContext _)
    {
        cameraPanSpeed = shiftPanSpeed;
    }

    private void DefaultPanSpeed(InputAction.CallbackContext _)
    {
        cameraPanSpeed = defaultPanSpeed;
    }

    private void UpdatePlayer()
    {
        UpdatePlayerRotation();
        UpdatePlayerPosition();
    }

    private void UpdateCameraTarget(InputAction.CallbackContext ctx)
    {
        Vector3 moveDirection = new(0, 0, ctx.ReadValue<float>());
        cameraTarget.transform.Translate(Time.deltaTime * cameraZoomSpeed * moveDirection, Space.Self);
        cameraTarget.transform.localPosition = new(0, 0, Mathf.Clamp(cameraTarget.transform.localPosition.z, cameraMinZoom, cameraMaxZoom));
    }

    private void UpdateCamera()
    {
        playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraTarget.transform.localPosition, cameraZoomLerpSpeed * Time.deltaTime);
    }

    private void UpdatePlayerTargetRotation(InputAction.CallbackContext _)
    {
        Vector2 rotationDirection = PlayerManager.Instance.playerInput.Rotate;

        yaw += rotationDirection.x * cameraRotationSpeed * Time.deltaTime;
        pitch += rotationDirection.y * cameraRotationSpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -maxCameraPitch, -minCameraPitch);
    }

    private void UpdatePlayerRotation()
    {
        smoothYaw = Mathf.LerpAngle(smoothYaw, yaw, cameraRotationLerpSpeed * Time.deltaTime);
        smoothPitch = Mathf.LerpAngle(smoothPitch, pitch, cameraRotationLerpSpeed * Time.deltaTime);

        playerTarget.transform.rotation = Quaternion.Euler(0, smoothYaw, 0);

        transform.rotation = Quaternion.Euler(smoothPitch, smoothYaw, 0);
    }

    private void UpdatePlayerTargetPosition(InputAction.CallbackContext ctx)
    {
        Vector2 pan = ctx.ReadValue<Vector2>();
        Vector3 moveDirection = new(pan.x, 0, pan.y);
        playerTarget.transform.Translate(cameraPanSpeed * Time.deltaTime * moveDirection);
    }

    private void UpdatePlayerPosition()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.transform.position, cameraPositionLerpSpeed * Time.deltaTime);
    }
}
