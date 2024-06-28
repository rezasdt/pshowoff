using UnityEngine;
using UnityEngine.InputSystem;

public class MachineCreationHandler : MonoBehaviour
{
    [SerializeField] private Tag freePlatformTag;
    [SerializeField] private Tag reservedPlatformTag;
    [SerializeField] private GameObject platformsParent;
    [SerializeField] private PointInputHandler inputHandler;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private MachineControllerRuntimeSet mControllerRuntimeSet;
    [SerializeField] private GameLogger gameLogger;
    
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
        inputHandler.OnCreate += OnCreate;
        _playerActions.Enable();
        _playerActions.DiscardSelection.performed += OnDiscardSelection;
    }

    private void OnDisable()
    {
        MachineButtonUIController.OnMachineClicked -= OnMachineClicked;
        inputHandler.OnCreate -= OnCreate;
        _playerActions.Disable();
        _playerActions.DiscardSelection.performed -= OnDiscardSelection;
    }

    private void OnDiscardSelection(InputAction.CallbackContext pContext)
    {
        _selectedMachine = null;
        platformsParent.SetActive(false);
    }

    private void OnMachineClicked(MachineBase pMachine)
    {
        _selectedMachine = pMachine;
        platformsParent.SetActive(true);
    }

    private void OnCreate(GameObject pPlatform)
    {
        if (!pPlatform.CompareTag(freePlatformTag)) return;
        if (moneyVariable.Value < _selectedMachine.Cost) return;
        
        var newMachine = Instantiate(_selectedMachine.Prefab, pPlatform.transform.position, Quaternion.identity)
            .GetComponent<MachineController>();
        gameLogger.BuyMachine(newMachine.Machine);
        mControllerRuntimeSet.Add(newMachine);
        newMachine.Platform = pPlatform;
        pPlatform.tag = reservedPlatformTag;
        pPlatform.gameObject.SetActive(false);
        moneyVariable.Value -= _selectedMachine.Cost;
        _selectedMachine = null;
        platformsParent.SetActive(false);
    }
}
