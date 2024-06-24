using UnityEngine;

[CreateAssetMenu(menuName = "Machine/Improved")]
public class ImprovedMachine : MachineBase
{
    public static float RepairFactor { get; } = 1f;
    public static float SalvageFactor { get; } = 0.25f;

    [field: SerializeField]
    [field: Range(0, 100)]
    public int HealthySpawnChance { get; private set; }
    [field: SerializeField]
    public MachineBase Downgrade { get; private set; }

    private int? _cachedValue = null;

    public override int Value
    {
        get
        {
            if (_cachedValue == null)
                _cachedValue = CalcValue();

            return _cachedValue.Value;
        }
    }

    private int CalcValue()
    {
        int totalValue = Cost;
        MachineBase current = Downgrade;

        while (current is ImprovedMachine improvedMachine)
        {
            totalValue += improvedMachine.Cost;
            current = improvedMachine.Downgrade;
        }

        totalValue += current.Cost;

        return totalValue;
    }
}
