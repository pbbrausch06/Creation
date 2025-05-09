using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    [HideInInspector] public Camera playerCamera;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
