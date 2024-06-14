using UnityEngine;

public class ImprovedMachineController : MachineController
{
    public static event System.Action OnDefectedUpgrade = delegate { };
    public bool IsHealthy { get; private set; }
    public override int ResaleValue
    {
        get => Mathf.FloorToInt(Machine.Value * (IsHealthy ? MachineBase.ResaleFactor : ImprovedMachine.SalvageFactor));
    }

    private void Awake()
    {
        IsHealthy =
            Random.Range(0, 100) < ((ImprovedMachine)Machine).HealthySpawnChance ? true : false;   
    }

    private void Start()
    {
        if (IsHealthy) StartCoroutine(EarnCoroutine());
        else OnDefectedUpgrade.Invoke();
    }

    public int RepairCost
        => Mathf.FloorToInt(Machine.Value * ImprovedMachine.RepairFactor);

    public void Repair()
    {
        IsHealthy = true;
        StartCoroutine(EarnCoroutine());
    }
}
