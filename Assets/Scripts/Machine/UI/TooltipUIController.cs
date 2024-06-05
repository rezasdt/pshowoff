using TMPro;
using UnityEngine;

public class TooltipUIController : MonoBehaviour
{
    [Header("Selection Tooltip")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI earn;
    [SerializeField] private TextMeshProUGUI resale;
    [SerializeField] private RectTransform repairPanel;
    [SerializeField] private TextMeshProUGUI repair;
    [SerializeField] private RectTransform repairButtonPanel;
    [SerializeField] private RectTransform upgradeButtonPanel;

    [Header("Upgrade Tooltip")]
    [SerializeField] private TextMeshProUGUI healthyChance;
    [SerializeField] private TextMeshProUGUI upgradeTitle;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private TextMeshProUGUI upgradeEarn;

    public void Init(MachineController pMachineController)
    {
        title.text = pMachineController.Machine.name;
        cost.text = $"{pMachineController.Machine.Cost.ToString()}$";
        earn.text = $"{pMachineController.Machine.EarnAmount.ToString()}$ / {pMachineController.Machine.EarnIntervalSec.ToString()}s";
        resale.text = $"{pMachineController.ResaleValue.ToString()}$";
        
        if (pMachineController is ImprovedMachineController { IsHealthy: false } improvedMachineController)
        {
            repair.text = $"{improvedMachineController.ResaleValue.ToString()}$";
            repairPanel.gameObject.SetActive(true);
            repairButtonPanel.gameObject.SetActive(true);
        }
        else
        {
            repairPanel.gameObject.SetActive(false);
            repairButtonPanel.gameObject.SetActive(false);
        }
        
        if (pMachineController.Machine.Upgrade is { } improvedMachine)
        {
            healthyChance.text = $"{improvedMachine.HealthySpawnChance.ToString()}%";
            upgradeTitle.text = improvedMachine.name;
            upgradeCost.text = $"{improvedMachine.Cost.ToString()}$";
            upgradeEarn.text = $"{improvedMachine.EarnAmount.ToString()}$ / {improvedMachine.EarnIntervalSec.ToString()}s";
            upgradeButtonPanel.gameObject.SetActive(true);
        }
        else
        {
            upgradeButtonPanel.gameObject.SetActive(false);
        }
    }
}
