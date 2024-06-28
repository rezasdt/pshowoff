using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineSelectionHandler : MonoBehaviour
{
    [SerializeField] private PointInputHandler pointInputHandler;
    [SerializeField] private Tag freePlatformTag;
    [SerializeField] private TooltipUIController tooltipUIController;
    [SerializeField] private RectTransform tooltip;
    [SerializeField] private RectTransform upgradeTooltip;
    [SerializeField] private GameObject defectedIndicatorPrefab;
    [SerializeField] private GameObject mainCamera;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable riskVariable;
    [SerializeField] private Int32Variable riskCapacityVariable;
    [SerializeField] private MachineControllerRuntimeSet mControllerRuntimeSet;
    [SerializeField] private GameLogger gameLogger;
    
    private MachineController _selectedMachine;
    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;
    private GameObject _lastHoveredMachine;
    private Dictionary<MachineController, GameObject> _defectedMachinesByIndicator = new();

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
        ImprovedMachineController.UpgradeSuccess += LogMachineUpgradeResult;
        ImprovedMachineController.UpgradeSuccess += CreateIndicatorOnDefected;
        _playerActions.Enable();
        _playerActions.Pan.started += OnPan;
    }

    private void OnDisable()
    {
        pointInputHandler.OnSelect -= OnSelect;
        pointInputHandler.OnMachineHover -= OnMachineHover;
        pointInputHandler.OnClickAway -= HideTooltips;
        ImprovedMachineController.UpgradeSuccess -= LogMachineUpgradeResult;
        ImprovedMachineController.UpgradeSuccess -= CreateIndicatorOnDefected;
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
        gameLogger.RepairMachine(_selectedMachine.Machine);
        DestroyIndicatorIfDefected(_selectedMachine);
        selected.Repair();
        moneyVariable.Value -= selected.RepairCost;
        HideTooltips();
    }
    
    public void Upgrade()
    {
        if (_selectedMachine.Machine.Upgrade.Cost > moneyVariable.Value) return;
        gameLogger.UpgradeMachine(_selectedMachine.Machine);
        if (_selectedMachine.Machine is ImprovedMachine)
        {
            riskVariable.Value += 100 - ((ImprovedMachine)_selectedMachine.Machine).HealthySpawnChance;
        }
        if (_selectedMachine.Machine.Upgrade != null &&
            _selectedMachine.Machine.Upgrade.Upgrade != null)
        {
            var amount = 100 - _selectedMachine.Machine.Upgrade.HealthySpawnChance;
            riskCapacityVariable.Value += amount;
        }
        mControllerRuntimeSet.Remove(_selectedMachine);
        Destroy(_selectedMachine.gameObject);
        var newMachineController =
            Instantiate(_selectedMachine.Machine.Upgrade.Prefab, _selectedMachine.gameObject.transform.position,
                    Quaternion.identity)
                .GetComponent<MachineController>();
        mControllerRuntimeSet.Add(newMachineController);
        newMachineController.Platform = _selectedMachine.Platform;
        moneyVariable.Value -= _selectedMachine.Machine.Upgrade.Cost;
        HideTooltips();
    }
    
    public void Sell()
    {
        _selectedMachine.Platform.tag = freePlatformTag.tag;
        _selectedMachine.Platform.SetActive(true);
        moneyVariable.Value += _selectedMachine.ResaleValue;
        mControllerRuntimeSet.Remove(_selectedMachine);
        DestroyIndicatorIfDefected(_selectedMachine);
        Destroy(_selectedMachine.gameObject);
        HideTooltips();
        gameLogger.SellMachine(_selectedMachine.Machine);
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

    private void LogMachineUpgradeResult(ImprovedMachineController pImprovedMachineController)
    {
        if (pImprovedMachineController.IsHealthy) gameLogger.MachineUpgradeSuccess(_selectedMachine.Machine);
        else gameLogger.MachineUpgradeFail(_selectedMachine.Machine);
    }
    
    private void CreateIndicatorOnDefected(ImprovedMachineController pImprovedMachineController)
    {
        if (pImprovedMachineController.IsHealthy) return;
        GameObject indicator = Instantiate(defectedIndicatorPrefab,
            pImprovedMachineController.gameObject.transform.position + new Vector3(0, 1.75f, -0.95f),
            mainCamera.transform.rotation);
        var tween = Tween.ShakeLocalPosition(indicator.transform, strength: new Vector3(0, 0.5f), duration: 2, frequency: 1);
        tween.SetRemainingCycles(-1);
        _defectedMachinesByIndicator.Add(pImprovedMachineController, indicator);
    }

    private void DestroyIndicatorIfDefected(MachineController pMachineController)
    {
        if (_selectedMachine is ImprovedMachineController improvedMachineController)
        {
            if (improvedMachineController.IsHealthy) return;
            Destroy(_defectedMachinesByIndicator[pMachineController]);
            _defectedMachinesByIndicator.Remove(_selectedMachine);
        }
    }
}
