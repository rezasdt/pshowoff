using System;
using UnityEngine;

[CreateAssetMenu]
public class StageDatabase : ScriptableObject
{
    [field: SerializeField]
    public Stage[] Stages { get; private set; } = Array.Empty<Stage>();
}

[Serializable]
public class Stage
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: Min(0)]
    [field: SerializeField]
    public int MaxThreshold { get; private set; }
    [field: SerializeField]
    public StarterMachine[] StarterMachines { get; private set; } = Array.Empty<StarterMachine>();
    [field: SerializeField]
    public Challenge[] Challenges { get; private set; } = Array.Empty<Challenge>();
}
