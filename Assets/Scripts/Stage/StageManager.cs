using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Int64Variable _moneyVariable;
    [SerializeField] private StageDatabase _stageDatabase;
    [SerializeField] private MachineControllerRuntimeSet mControllerRuntimeSet;

    public event System.Action<int> OnStageChange = delegate { };

    private List<int> _stageThresholds = new();
    private int _formerStage = -1;

    private void Awake()
    {
        CacheStageThresholds();
    }

    private void Start()
    {
        StartCoroutine(CheckStageCoroutine());
    }

    private void CacheStageThresholds()
    {
        _stageThresholds.Add(0);
        foreach (var stage in _stageDatabase.Stages)
        {
            _stageThresholds.Add(stage.MaxThreshold);
        }
    }

    public int GetStage()
    {
        int currentStage = -1;
        foreach (int threshold in _stageThresholds)
        {
            if (_moneyVariable.Value + mControllerRuntimeSet.TotalValue > threshold) currentStage++;
        }
        return Mathf.Min(currentStage, _stageThresholds.Count - 2);
    }

    private IEnumerator CheckStageCoroutine()
    {
        while (true)
        {
            int currentStage = GetStage();
            if (currentStage != _formerStage)
            {
                OnStageChange.Invoke(currentStage);
                _formerStage = currentStage;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
