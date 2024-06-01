using UnityEngine;

public abstract class MachineBase : ScriptableObject
{
    public static float ResaleFactor { get; } = 0.5f;

    [field: Min(0)]
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: Min(0)]
    [field: SerializeField]
    public virtual int EarnAmount { get; private set; }
    [field: Min(0)]
    [field: SerializeField]
    public float EarnIntervalSec { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public ImprovedMachine Upgrade { get; private set; }
    public virtual int Value { get; }
}
