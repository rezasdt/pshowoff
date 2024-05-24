using TMPro;
using UnityEngine;

public class PrintResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _risk;

    private void Start()
    {
        _money.text = $"You earned ${GameManager.Instance.Money}";
        _risk.text = $"You are {(GameManager.Instance.RiskTaken / (float)GameManager.Instance.RiskTotal * 100):0.0}% risk taking";
    }
}
