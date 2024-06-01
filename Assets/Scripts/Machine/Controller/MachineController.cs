using System.Collections;
using UnityEngine;

public abstract class MachineController : MonoBehaviour
{
    [field: SerializeField]
    public MachineBase Machine { get; protected set; }
    public GameObject Platform { get; set; }

    public static event System.Action<int, Vector3> OnEarn = delegate { };

    protected IEnumerator EarnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Machine.EarnIntervalSec);
            OnEarn.Invoke(Machine.EarnAmount, transform.position);
        }
    }

    public abstract int ResaleValue { get; }
}
