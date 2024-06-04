using UnityEngine;
using UnityEngine.InputSystem;

public class UIPanelPanHandler : MonoBehaviour
{
    [SerializeField] private float _panSpeed = 100f;

    private RectTransform _panel;
    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;

    private void Awake()
    {
        _panel = GetComponent<RectTransform>();
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
        Vector2 delta = context.ReadValue<Vector2>();
        _panel.anchoredPosition += delta * _panSpeed * Time.deltaTime;
    }
}