using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachineTooltipUI : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TextMeshProUGUI _infoText;
    [HideInInspector] public MachineController MachineController;
    public event System.Action UpgradeFailed = delegate { };

    private void OnEnable()
    {
        SetCurrentMachineInfo();
        if (MachineController.Machine.Upgrade != null)
        {
            _upgradeButton.gameObject.SetActive(true);
        }
        else
        {
            _upgradeButton.gameObject.SetActive(false);
        }
    }

    public void Sell()
    {
        GameManager.Instance.Money += MachineController.Machine.ResaleValue;
        Destroy(MachineController.gameObject);
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        if (GameManager.Instance.Money < MachineController.Machine.Upgrade.Cost) return;

        // Successful upgrade
        if (Random.Range(0f, 100f) < MachineController.Machine.UpgradeSuccessChance)
        {
            Instantiate(MachineController.Machine.Upgrade.Prefab, MachineController.transform.position, Quaternion.identity);
            _popup.Create("Upgrade successful");
        }
        else // Failed upgrade
        {
            UpgradeFailed.Invoke();
            _popup.Create("Upgrade failed. You lost the machine.");
        }
        Destroy(MachineController.gameObject);
        gameObject.SetActive(false);
        GameManager.Instance.Money -= MachineController.Machine.Upgrade.Cost;
    }

    public void SetCurrentMachineInfo()
    {
        _infoText.text = MachineController.Machine.ToString();
    }

    public void SetUpgradeMachineInfo()
    {
        _infoText.text = MachineController.Machine.Upgrade.ToString();
    }
}
