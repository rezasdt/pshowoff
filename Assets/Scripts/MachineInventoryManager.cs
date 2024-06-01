using System.Collections.Generic;
using UnityEngine;

public class MachineInventoryManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _container;
    [SerializeField]
    private StageManager _stageManager;
    [SerializeField]
    private MachineButtonUIController _machineButtonPrefab;
    [SerializeField]
    private StageDatabase _stageDatabase;

    private Dictionary<int, List<MachineButtonUIController>> _machineButtonsByStage = new();

    private void OnEnable()
    {
        _stageManager.OnStageChange += UpdateButtonsState;
    }
    private void OnDisable()
    {
        _stageManager.OnStageChange -= UpdateButtonsState;
    }
    private void Start()
    {
        InitMachineButtons();
        UpdateButtonsState(_stageManager.GetStage());
    }

    private void InitMachineButtons()
    {
        for (int stageIndex = 0; stageIndex < _stageDatabase.Stages.Count; stageIndex++)
        {
            Stage stage = _stageDatabase.Stages[stageIndex];
            List<MachineButtonUIController> machineButtons = new();

            foreach (var starterMachine in stage.StarterMachines)
            {
                MachineButtonUIController buttonInstance = Instantiate(_machineButtonPrefab, _container);
                buttonInstance.Init(starterMachine, stageIndex + 1);
                machineButtons.Add(buttonInstance);
            }

            _machineButtonsByStage.Add(stageIndex, machineButtons);
        }
    }

    private void UpdateButtonsState(int pCurrentStageIndex)
    {
        foreach (var stageEntry in _machineButtonsByStage)
        {
            int stageIndex = stageEntry.Key;
            List<MachineButtonUIController> stageMachineButtons = stageEntry.Value;

            foreach (var button in stageMachineButtons)
            {
                if (stageIndex <= pCurrentStageIndex)
                    button.Unlock();
                else
                    button.Lock();
            }
        }
    }
}
