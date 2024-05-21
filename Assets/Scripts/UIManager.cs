using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI daysLeftText;
    [SerializeField] private TextMeshProUGUI riskTakenText;

    private void Start()
    {
        InvokeRepeating("UpdateUI", 0, 1);
    }

    private void UpdateUI()
    {
        if (GameManager.Instance != null)
        {
            moneyText.text = $"Money: ${GameManager.Instance.Money}";
            daysLeftText.text = $"Days Left: {GameManager.Instance.DaysLeft}";

            if (GameManager.Instance.RiskTotal > 0)
            {
                float riskPercentage = (float)GameManager.Instance.RiskTaken / GameManager.Instance.RiskTotal * 100;
            }
            else
            {
                riskTakenText.text = "Risk Taken: 0%";
            }
        }
    }
}
