using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class CameraPanHandler : MonoBehaviour
{
    [SerializeField] private float panSpeed = 0.01f;
    [SerializeField] private float zoomSpeed = 0.01f;
    [SerializeField] private float zoomRange = 8f;

    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;
    private float _zoom = 0f;
    private Vector2 _lastPinchDistance;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
        if (!EnhancedTouchSupport.enabled)
        {
            EnhancedTouchSupport.Enable();
        }
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Pan.performed += OnPan;
        _playerActions.Zoom.performed += OnZoom;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Pan.performed -= OnPan;
        _playerActions.Zoom.performed -= OnZoom;
    }

    private void OnPan(InputAction.CallbackContext context)
    {
        if (context.control.device is Touchscreen && Touch.activeTouches.Count > 1)
            return;
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        Vector3 pan = new Vector3(mouseDelta.x, 0f, mouseDelta.y) * panSpeed;
        transform.Translate(-pan, Space.World);
    }

    private void OnZoom(InputAction.CallbackContext context)
    {
        if (context.control.device is Touchscreen && Touch.activeTouches.Count >= 2)
        {
            Touch primary = Touch.activeTouches[0];
            Touch secondary = Touch.activeTouches[1];
            if (primary.phase == TouchPhase.Moved || secondary.phase == TouchPhase.Moved)
            {
                if (primary.history.Count < 1 || secondary.history.Count < 1)
                    return;
                float current = Vector2.Distance(primary.screenPosition, secondary.screenPosition);
                float previosu =
                    Vector2.Distance(primary.history[0].screenPosition, secondary.history[0].screenPosition);
                float diff = current - previosu;
                Zoom(diff);
            }
        }
        else
        {
            if (context.phase != InputActionPhase.Performed)
                return;
            var scrollValue = context.ReadValue<Vector2>().y;
            Zoom(scrollValue);
        }
    }

    private void Zoom(float scrollValue)
    {
        float zoomAmount = scrollValue * zoomSpeed;
        _zoom += zoomAmount;
        if (Mathf.Abs(_zoom) > zoomRange) return;
        transform.localPosition += transform.forward * zoomAmount;
    }
}
