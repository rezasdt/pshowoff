using TMPro;
using UnityEngine;
using System.Text;

public class TooltipUIController : MonoBehaviour
{
    [Header("Selection Tooltip")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI earn;
    [SerializeField] private TextMeshProUGUI resale;
    [SerializeField] private TextMeshProUGUI repair;
    [SerializeField] private RectTransform repairPanel;
    [SerializeField] private RectTransform repairButtonPanel;
    [SerializeField] private RectTransform upgradeButtonPanel;

    [Header("Upgrade Tooltip")]
    [SerializeField] private TextMeshProUGUI healthyChance;
    [SerializeField] private TextMeshProUGUI upgradeTitle;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private TextMeshProUGUI upgradeEarn;

    private StringBuilder stringBuilder = new StringBuilder();

    public void Init(MachineController pMachineController)
    {
        repairPanel.gameObject.SetActive(false);
        repairButtonPanel.gameObject.SetActive(false);
        upgradeButtonPanel.gameObject.SetActive(false);

        title.text = pMachineController.Machine.name;

        stringBuilder.Clear();
        stringBuilder.Append(pMachineController.Machine.Cost.ToString());
        stringBuilder.Append("$");
        cost.text = stringBuilder.ToString();

        stringBuilder.Clear();
        stringBuilder.Append(pMachineController.Machine.EarnAmount.ToString());
        stringBuilder.Append("$ / ");
        stringBuilder.Append(pMachineController.Machine.EarnIntervalSec.ToString());
        stringBuilder.Append("s");
        earn.text = stringBuilder.ToString();

        stringBuilder.Clear();
        stringBuilder.Append(pMachineController.ResaleValue.ToString());
        stringBuilder.Append("$");
        resale.text = stringBuilder.ToString();

        if (pMachineController.Machine.Upgrade is { } improvedMachine)
        {
            stringBuilder.Clear();
            stringBuilder.Append(improvedMachine.HealthySpawnChance.ToString());
            stringBuilder.Append("%");
            healthyChance.text = stringBuilder.ToString();

            upgradeTitle.text = improvedMachine.name;

            stringBuilder.Clear();
            stringBuilder.Append(improvedMachine.Cost.ToString());
            stringBuilder.Append("$");
            upgradeCost.text = stringBuilder.ToString();

            stringBuilder.Clear();
            stringBuilder.Append(improvedMachine.EarnAmount.ToString());
            stringBuilder.Append("$ / ");
            stringBuilder.Append(improvedMachine.EarnIntervalSec.ToString());
            stringBuilder.Append("s");
            upgradeEarn.text = stringBuilder.ToString();

            upgradeButtonPanel.gameObject.SetActive(true);
        }

        if (pMachineController is ImprovedMachineController { IsHealthy: false } improvedMachineController)
        {
            stringBuilder.Clear();
            stringBuilder.Append(improvedMachineController.RepairCost.ToString());
            stringBuilder.Append("$");
            repair.text = stringBuilder.ToString();

            repairPanel.gameObject.SetActive(true);
            repairButtonPanel.gameObject.SetActive(true);
            upgradeButtonPanel.gameObject.SetActive(false);
        }
    }
}
