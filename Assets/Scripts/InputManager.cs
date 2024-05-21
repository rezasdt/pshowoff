using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private LayerMask _placeLayer;
    [SerializeField]
    private LayerMask _placeableLayer;
    private const uint RAYCAST_THROTTLE_COUNT = 5;
    private const float MAX_RAYCAST_DISTANCE = 100f;

    public event Action<Vector3> Move = delegate { };
    public event Action<Vector3> Place = delegate { };
    public event Action<GameObject, Vector2> Select = delegate { };
    public event Action Discard = delegate { };

    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;
    private uint _frameCounter = 0;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Move.performed += OnMove;
        _playerActions.Press.started += OnPlaceOrSelect;
        _playerActions.Discard.performed += OnDiscard;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Move.performed -= OnMove;
        _playerActions.Press.started -= OnPlaceOrSelect;
        _playerActions.Discard.performed -= OnDiscard;
    }

    private void OnMove(InputAction.CallbackContext pContext)
    {
        if (_frameCounter++ % RAYCAST_THROTTLE_COUNT != 0)
            return;
        Vector3? selectedPoint =
            GetSelectedPlacePoint(pContext.ReadValue<Vector2>());
        if (selectedPoint == null)
            return;
        Move.Invoke(selectedPoint.Value);
    }

    private void OnPlaceOrSelect(InputAction.CallbackContext pContext)
    {
        if (IsPointerOverUI())
            return;
        Vector2 pointerPos =
            Mouse.current.position.ReadValue();
        GameObject selectedPlaceable =
            GetSelectedPlaceable(pointerPos);

        if (selectedPlaceable != null)
        {
            Select.Invoke(selectedPlaceable, pointerPos);
            return;
        }

        Vector3? placePoitn =
            GetSelectedPlacePoint(pointerPos);
        if (placePoitn != null)
        {
            Place.Invoke(placePoitn.Value);
        }
    }

    private void OnDiscard(InputAction.CallbackContext pContext)
        => Discard.Invoke();

    private bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    private Vector3? GetSelectedPlacePoint(Vector2 pPosition)
    {
        Ray ray = _camera.ScreenPointToRay(pPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAX_RAYCAST_DISTANCE, _placeLayer))
        {
            return hit.point;
        }
        return null;
    }

    private GameObject GetSelectedPlaceable(Vector2 pPosition)
    {
        Ray ray = _camera.ScreenPointToRay(pPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAX_RAYCAST_DISTANCE, _placeableLayer))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
