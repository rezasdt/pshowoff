using System.Collections;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    [field: SerializeField] public Machine Machine { get; private set; }

    private void Start()
    {
        if (Machine.Upgrade != null)
            GameManager.Instance.RiskTotal += 100 - Machine.Upgrade.UpgradeSuccessChance;
        StartCoroutine(EarnCoroutine());
    }

    private IEnumerator EarnCoroutine()
    {
        while (true)
        {
            GameManager.Instance.Money += Machine.Earn;
            yield return new WaitForSeconds(Machine.EarningInterval);
        }
    }
}
