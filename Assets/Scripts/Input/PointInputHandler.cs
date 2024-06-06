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

    private ushort _raycastThrottle = 0;
    private const float RaycastDistance = 100f;
    
    public event System.Action<GameObject, Vector2> OnSelect = delegate { };
    public event System.Action<GameObject> OnCreate = delegate { };
    public event System.Action<GameObject> OnMachineHover = delegate { };

    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;
    private GameObject _lastHoveringMachine;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
    }
    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Point.performed += OnPoint;
        _playerActions.PointerMove.performed += OnMove;
    }
    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Point.performed -= OnPoint;
        _playerActions.PointerMove.performed -= OnMove;
    }
    
    private void OnMove(InputAction.CallbackContext pContext)
    {
        if (_raycastThrottle++ % 5 > 0) return;
        if (IsPointerOverUI()) return;
        
        var point = pContext.ReadValue<Vector2>();
        var ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RaycastDistance, _machinesLayer))
        {
            var currentMachine = hit.collider.gameObject;
            if (currentMachine == _lastHoveringMachine) return;
            _lastHoveringMachine = currentMachine;
            OnMachineHover.Invoke(currentMachine);
        }
        else
        {
            if (_lastHoveringMachine == null) return;
            _lastHoveringMachine = null;
            OnMachineHover.Invoke(null);
        }
    }

    private void OnPoint(InputAction.CallbackContext pContext)
    {
        if (IsPointerOverUI()) return;

        var point = pContext.ReadValue<Vector2>();
        var ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RaycastDistance, _machinesLayer))
        {
            OnSelect.Invoke(hit.collider.gameObject, point);
            return;
        }
        if (Physics.Raycast(ray, out hit, RaycastDistance, _platfromsLayer))
        {
            OnCreate.Invoke(hit.collider.gameObject);
        }
    }

    private bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
}
