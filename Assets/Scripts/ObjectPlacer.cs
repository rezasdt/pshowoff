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

    private void OnEnable()
    {
        _inputManager.Move += OnMove;
        _inputManager.Discard += OnDiscard;
    }

    private void OnDisable()
    {
        _inputManager.Move -= OnMove;
        _inputManager.Discard -= OnDiscard;
    }

    private void Start()
    {
        PopulateUIPanel();
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
                GameObject machineItemUIObj = Instantiate(_machineItemUIPrefab, _uiPanel);
                MachineItemUI machineItemUI = machineItemUIObj.GetComponent<MachineItemUI>();

                if (machineItemUI != null)
                {
                    machineItemUI.machine = machine;
                }
                else
                {
                    Debug.LogError("MachineItemUI prefab does not have MachineItemUI component attached.");
                    return;
                }
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
}
