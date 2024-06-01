using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineCreationHandler : MonoBehaviour
{
    [SerializeField]
    private Tag _freePlatfromTag;
    [SerializeField]
    private Tag _reservedPlatfromTag;
    [SerializeField]
    private GameObject _platformsParent;
    [SerializeField]
    private PointInputHandler _inputHandler;
    [SerializeField]
    private Int64Variable _moneyVariable;

    private MachineBase _selectedMachine;
    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
    }

    private void OnEnable()
    {
        MachineButtonUIController.OnMachineClicked += OnMachineClicked;
        _inputHandler.OnCreate += OnCreate;
        _playerActions.Enable();
        _playerActions.DiscardSelection.performed += OnDiscardSelection;
    }

    private void OnDisable()
    {
        MachineButtonUIController.OnMachineClicked -= OnMachineClicked;
        _inputHandler.OnCreate -= OnCreate;
        _playerActions.Disable();
        _playerActions.DiscardSelection.performed -= OnDiscardSelection;
    }

    private void OnDiscardSelection(InputAction.CallbackContext pContext)
    {
        _selectedMachine = null;
        _platformsParent.SetActive(false);
    }

    private void OnMachineClicked(MachineBase pMachine)
    {
        _selectedMachine = pMachine;
        _platformsParent.SetActive(true);
    }

    private void OnCreate(GameObject pPlatfrom)
    {
        if (!pPlatfrom.CompareTag(_freePlatfromTag)) return;

        var newMachine = Instantiate(_selectedMachine.Prefab, pPlatfrom.transform.position, Quaternion.identity)
            .GetComponent<MachineController>();
        newMachine.Platform = pPlatfrom;
        pPlatfrom.tag = _reservedPlatfromTag;
        pPlatfrom.gameObject.SetActive(false);
        _moneyVariable.Value -= _selectedMachine.Cost;
        _selectedMachine = null;
        _platformsParent.SetActive(false);
    }
}
