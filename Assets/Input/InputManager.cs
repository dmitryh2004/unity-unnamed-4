using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;
    public Vector2 Movement { get; private set; }
    private bool dash = false;
    public bool Dash => dash && (!(dash = false));

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnMovePressed(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
        Debug.Log($"Current input: {Movement}");
    }

    public void OnDashPressed(InputAction.CallbackContext context)
    {
        dash = true;
    }
}
