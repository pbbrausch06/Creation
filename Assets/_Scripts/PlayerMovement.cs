using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void FixedUpdate()
    {
        UpdateRotation();
        Move();
    }

    private void UpdateRotation()
    {
        Quaternion cameraRotation = PlayerCamera.Instance.playerCamera.transform.rotation;
        Vector3 eulerRotation = cameraRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
    }

    private void Move()
    {
        Vector3 moveDirection = new(PlayerManager.Instance.playerInput.Pan.x, 0, PlayerManager.Instance.playerInput.Pan.y);
        transform.Translate(speed * Time.deltaTime * moveDirection);
    }
}
