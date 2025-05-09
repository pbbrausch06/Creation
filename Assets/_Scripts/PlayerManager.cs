using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [HideInInspector] public PlayerInput playerInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        playerInput = GetComponent<PlayerInput>();
    }
}
