using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraPanHandler : MonoBehaviour
{
    [SerializeField] private float panSpeed = 0.01f;

    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Pan.performed += OnPan;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Pan.performed -= OnPan;
    }

    private void OnPan(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        Vector3 pan = new Vector3(mouseDelta.x, 0f, mouseDelta.y) * panSpeed;
        transform.Translate(-pan, Space.World);
    }
}