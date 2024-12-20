using UnityEngine;

[CreateAssetMenu(menuName = "RuntimeSets/MachineControllerSet")]
public class MachineControllerRuntimeSet : RuntimeSet<MachineController>
{
    public int TotalValue { get; private set; }
    
    public override void Add(MachineController item)
    {
        base.Add(item);
        CalculateMachinesValue();
    }

    public override void Remove(MachineController item)
    {
        base.Remove(item);
        CalculateMachinesValue();
    }

    public override void Clear()
    {
        base.Clear();
        CalculateMachinesValue();
    }

    private void CalculateMachinesValue()
    {
        TotalValue = 0;
        foreach (var item in Items)
        {
            TotalValue += item.ResaleValue;
        }
    }
}