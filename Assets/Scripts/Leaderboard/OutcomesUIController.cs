using System.Text;
using TMPro;
using UnityEngine;

public class OutcomesUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject leaderboard;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable riskVariable;
    [SerializeField] private Int32Variable riskCapacityVariable;
    [SerializeField] private RiskAdvice riskAdvice;
    [SerializeField] private Int32Variable daysTotalVariable;

    private void Start()
    {
        StringBuilder sb = new();
        
        sb.AppendFormat("The business generated {0:N0}$ in {1} days!\n", moneyVariable.Value, daysTotalVariable.Value);
        
        if (riskCapacityVariable.Value > 0)
        {
            int riskPercentage = Mathf.FloorToInt((float)riskVariable.Value / riskCapacityVariable.Value * 100);
            RiskStage riskStage = riskAdvice.GetRiskStage(riskPercentage);
            sb.AppendFormat("You are {0}% a risk taker!", riskPercentage);
            sb.AppendFormat(" Which means you are {0}.\nHere is our advice for you:\n{1}", riskStage.Title, riskStage.Advice);
        }
        
        resultText.text = sb.ToString();
        leaderboard.SetActive(true);
    }
}