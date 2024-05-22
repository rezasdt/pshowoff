using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI daysLeftText;

    private void FixedUpdate()
    {
        moneyText.text = $"Money: ${GameManager.Instance.Money}";
        daysLeftText.text = $"Days Left: {GameManager.Instance.TotalDays}";
    }
}
