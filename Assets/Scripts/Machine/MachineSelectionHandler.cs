using UnityEngine;
using UnityEngine.InputSystem;

public class MachineSelectionHandler : MonoBehaviour
{
    [SerializeField] private PointInputHandler pointInputHandler;
    [SerializeField] private Tag freePlatformTag;
    [SerializeField] private TooltipUIController tooltipUIController;
    [SerializeField] private RectTransform tooltip;
    [SerializeField] private RectTransform upgradeTooltip;

    [SerializeField] private Int64Variable moneyVariable;
    
    private MachineController _selectedMachine;
    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;
    private GameObject _lastHoveredMachine;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
    }
    private void OnEnable()
    {
        pointInputHandler.OnSelect += OnSelect;
        pointInputHandler.OnMachineHover += OnMachineHover;
        pointInputHandler.OnClickAway += HideTooltips;
        _playerActions.Enable();
        _playerActions.Pan.started += OnPan;
    }

    private void OnDisable()
    {
        pointInputHandler.OnSelect -= OnSelect;
        pointInputHandler.OnMachineHover -= OnMachineHover;
        pointInputHandler.OnClickAway -= HideTooltips;
        _playerActions.Disable();
        _playerActions.Pan.started -= OnPan;
    }
    
    private void OnPan(InputAction.CallbackContext pContext)
    {
        HideTooltips();
    }

    private void OnMachineHover(GameObject pGameObject)
    {
        if (_lastHoveredMachine != null)
            Destroy(_lastHoveredMachine.GetComponent<Outline>());
        
        if (pGameObject == null) return;
        _lastHoveredMachine = pGameObject;
        var outline = _lastHoveredMachine.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 10f;
    }

    public void ShowUpgradeTooltip()
    {
        upgradeTooltip.transform.position = Mouse.current.position.ReadValue() + Vector2.one * 5;
        upgradeTooltip.gameObject.SetActive(true);
        _playerActions.PointerMove.performed += UpdateUpgradeTooltipPos;
    }

    public void HideUpgradeTooltip()
    {
        upgradeTooltip.gameObject.SetActive(false);
        _playerActions.PointerMove.performed -= UpdateUpgradeTooltipPos;
    }

    private void UpdateUpgradeTooltipPos(InputAction.CallbackContext pContext)
    {
        upgradeTooltip.transform.position = pContext.ReadValue<Vector2>() + Vector2.one * 5;
    }

    public void Repair()
    {
        var selected = ((ImprovedMachineController)_selectedMachine);
        if (selected.RepairCost > moneyVariable.Value) return;
        selected.Repair();
        moneyVariable.Value -= selected.RepairCost;
        HideTooltips();
    }
    
    public void Upgrade()
    {
        if (_selectedMachine.Machine.Upgrade.Cost > moneyVariable.Value) return;
        var newMachineController =
            Instantiate(_selectedMachine.Machine.Upgrade.Prefab, _selectedMachine.gameObject.transform.position,
                    Quaternion.identity)
                .GetComponent<MachineController>();
        newMachineController.Platform = _selectedMachine.Platform;
        Destroy(_selectedMachine.gameObject);
        moneyVariable.Value -= _selectedMachine.Machine.Upgrade.Cost;
        HideTooltips();
    }
    
    public void Sell()
    {
        _selectedMachine.Platform.tag = freePlatformTag.tag;
        _selectedMachine.Platform.SetActive(true);
        moneyVariable.Value += _selectedMachine.ResaleValue;
        Destroy(_selectedMachine.gameObject);
        HideTooltips();
    }

    private void HideTooltips()
    {
        tooltip.gameObject.SetActive(false);
        upgradeTooltip.gameObject.SetActive(false);
    }

    private void OnSelect(GameObject pSelectedObject, Vector2 pTooltipPosition)
    {
        var selectedMachine = pSelectedObject.GetComponentInParent<MachineController>();
        if (selectedMachine == null) return;

        _selectedMachine = selectedMachine;
        tooltipUIController.Init(selectedMachine);
        tooltip.transform.position = pTooltipPosition;
        tooltip.gameObject.SetActive(true);
    }
}
