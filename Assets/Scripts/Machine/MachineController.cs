using System.Collections;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private Machine _machine;

    private void Start()
    {
        StartCoroutine(EarnCoroutine());
    }

    private IEnumerator EarnCoroutine()
    {
        while (true)
        {
            GameManager.Instance.Money += _machine.Earn;
            yield return new WaitForSeconds(_machine.EarningInterval);
        }
    }
}
