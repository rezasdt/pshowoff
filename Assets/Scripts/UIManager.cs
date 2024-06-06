using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [Header("Canvas")]
    [SerializeField] private TextMeshProUGUI daysText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private Slider thresholdSlider;
    [Header("SO")]
    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable daysTotalVariable;
    [SerializeField] private Int32Variable dayLengthSecVariable;
    [SerializeField] private Int64Variable timerVariable;

    private readonly StringBuilder _sb = new();

    private void Start()
    {
        StartCoroutine(UpdateUICoroutine());
    }

    private void OnEnable()
    {
        stageManager.OnStageChange += OnStageChange;
    }

    private void OnDisable()
    {
        stageManager.OnStageChange -= OnStageChange;
    }

    private IEnumerator UpdateUICoroutine()
    {
        while (true)
        {
            _sb.Clear();
            _sb.Append(Mathf.CeilToInt((float)timerVariable.Value / dayLengthSecVariable.Value).ToString());
            _sb.Append(" / ");
            _sb.Append(daysTotalVariable.Value.ToString());
            daysText.text = _sb.ToString();

            _sb.Clear();
            _sb.Append(moneyVariable.Value.ToString("N0"));
            _sb.Append("$");
            moneyText.text = _sb.ToString();

            thresholdSlider.value = moneyVariable.Value;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnStageChange(int pNewStage)
    {
        if (pNewStage == -1)
        {
            stageNameText.text = "-";
            thresholdSlider.minValue = 0;
            thresholdSlider.maxValue = 1;
            thresholdSlider.value = 0;
            return;
        }

        if (pNewStage >= stageDatabase.Stages.Length) return;
        
        stageNameText.text = stageDatabase.Stages[pNewStage].Name;
        thresholdSlider.minValue = pNewStage == 0 ? 0 : stageDatabase.Stages[pNewStage - 1].MaxThreshold;
        thresholdSlider.maxValue = stageDatabase.Stages[pNewStage].MaxThreshold;
    }
}
