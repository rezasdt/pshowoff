using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private Machine _machine;

    private void Start()
    {
        InvokeRepeating("Earn", 0, _machine.EarningInterval);
    }

    private void Earn()
    {
        GameManager.Instance.Money += _machine.Earn;
    }
}
