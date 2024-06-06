using System;
using TMPro;
using UnityEngine;

public class PrintResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI result;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;

    private void Start()
    {
        result.text = $"You have earned {moneyVariable.Value.ToString()}$";
    }
}
