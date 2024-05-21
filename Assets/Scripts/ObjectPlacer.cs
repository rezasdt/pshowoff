using UnityEngine;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour
{
    public static Machine SelectedMachine = null;

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject _cellIndicator;
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _gridShader;

    [SerializeField] private MachinesDatabase _machinesDatabase;
    [SerializeField] private GameObject _machineItemUIPrefab;
    [SerializeField] private RectTransform _uiPanel;

    private List<(MachineItemUI machineItemUI, Tier tier)> _machineItemUIList = new List<(MachineItemUI machineItemUI, Tier tier)>();

    private void OnEnable()
    {
        _inputManager.Move += OnMove;
        _inputManager.Discard += OnDiscard;
        _inputManager.Place += OnPlace;
    }

    private void OnDisable()
    {
        _inputManager.Move -= OnMove;
        _inputManager.Discard -= OnDiscard;
        _inputManager.Place -= OnPlace;
    }

    private void Start()
    {
        PopulateUIPanel();
    }

    private void Update()
    {
        UpdateMachineItemUIStates();
    }

    private void PopulateUIPanel()
    {
        if (_machinesDatabase == null || _machineItemUIPrefab == null || _uiPanel == null)
        {
            Debug.LogError("MachinesDatabase, MachineItemUIPrefab, or UIPanel references are not set in ObjectPlacer.");
            return;
        }

        foreach (Tier tier in _machinesDatabase.tiers)
        {
            foreach (Machine machine in tier.machinesUnlocked)
            {
                // Instantiate a MachineItemUI prefab
                GameObject machineItemUIObj = Instantiate(_machineItemUIPrefab, _uiPanel);
                MachineItemUI machineItemUI = machineItemUIObj.GetComponent<MachineItemUI>();

                if (machineItemUI != null)
                {
                    // Set Machine scriptable object
                    machineItemUI.machine = machine;
                    _machineItemUIList.Add((machineItemUI, tier));
                }
                else
                {
                    Debug.LogError("MachineItemUI prefab does not have MachineItemUI component attached.");
                    return;
                }
            }
        }
    }

    private void UpdateMachineItemUIStates()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is not set.");
            return;
        }

        float currentMoney = GameManager.Instance.Money;

        foreach (var item in _machineItemUIList)
        {
            if (item.tier.stageAmount <= currentMoney)
            {
                item.machineItemUI.gameObject.SetActive(true);
            }
            else
            {
                item.machineItemUI.gameObject.SetActive(false);
            }
        }
    }

    private void OnDiscard()
    {
        SelectedMachine = null;
        _cellIndicator.SetActive(false);
        _gridShader.SetActive(false);
    }

    private void OnMove(Vector3 pPosition)
    {
        Vector3Int gridPos = _grid.WorldToCell(pPosition);
        _cellIndicator.transform.position = _grid.CellToWorld(gridPos);
    }

    private void OnPlace(Vector3 pPosition)
    {
        if (SelectedMachine == null) return;
        Vector3Int gridPos = _grid.WorldToCell(pPosition);
        Instantiate(SelectedMachine.Prefab, _grid.CellToWorld(gridPos), Quaternion.identity);
    }
}
