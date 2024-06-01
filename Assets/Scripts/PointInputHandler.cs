using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PointInputHandler : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private LayerMask _machinesLayer;
    [SerializeField]
    private LayerMask _platfromsLayer;

    public event System.Action<GameObject, Vector2> OnSelect = delegate { };
    public event System.Action<GameObject> OnCreate = delegate { };

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
        _playerActions.Point.performed += OnPoint;
    }
    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Point.performed -= OnPoint;
    }

    private void OnPoint(InputAction.CallbackContext pContext)
    {
        if (IsPointerOverUI()) return;

        Vector2 point = pContext.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;
        float maxDistance = 100f;

        if (Physics.Raycast(ray, out hit, maxDistance, _machinesLayer))
        {
            OnSelect.Invoke(hit.collider.gameObject, point);
            return;
        }
        if (Physics.Raycast(ray, out hit, maxDistance, _platfromsLayer))
        {
            OnCreate.Invoke(hit.collider.gameObject);
        }
    }

    private bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
}
