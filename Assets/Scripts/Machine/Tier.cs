using System.Collections.Generic;

[System.Serializable]
public class Tier
{
    public int stageAmount;
    public List<Machine> machinesUnlocked = new List<Machine>();
}
