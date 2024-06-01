using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class StageDatabase : ScriptableObject
{
    [field: SerializeField]
    public List<Stage> Stages { get; private set; } = new();
}

[System.Serializable]
public class Stage
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: Min(0)]
    [field: SerializeField]
    public int RequiredThreshold { get; private set; }
    [field: SerializeField]
    public List<StarterMachine> StarterMachines { get; private set; } = new();
    [field: SerializeField]
    public List<Challenge> Challenges { get; private set; } = new();
}
