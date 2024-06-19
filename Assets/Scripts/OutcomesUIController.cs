using System;
using TMPro;
using UnityEngine;

public class OutcomesUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable riskVariable;
    [SerializeField] private Int32Variable riskCapacityVariable;
    [SerializeField] private Int32Variable daysTotalVariable;

    private void Start()
    {
        resultText.text = $"The business generated {moneyVariable.Value:N0}$ in {daysTotalVariable.Value} days!\n";
        if (riskCapacityVariable.Value > 0)
        {
            resultText.text += $"You are {Mathf.FloorToInt((float)riskVariable.Value / riskCapacityVariable.Value)}% a risk taker!";
        }
    }
}
