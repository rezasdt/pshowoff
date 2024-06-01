using UnityEngine;

public class StarterMachineController : MachineController
{
    public override int ResaleValue
    {
        get => Mathf.FloorToInt(Machine.Value * MachineBase.ResaleFactor);
    }

    private void Start()
    {
        StartCoroutine(EarnCoroutine());
    }
}
