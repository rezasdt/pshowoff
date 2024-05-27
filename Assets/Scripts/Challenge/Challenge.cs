using UnityEngine;

[CreateAssetMenu]
public class Challenge : ScriptableObject
{
    [field: SerializeField]
    [field: TextArea]
    public string Description { get; private set; }
    [field: SerializeField]
    [field: Range(0, 100)]
    public int SuccessChance { get; private set; }
    [field: SerializeField]
    public int RiskRewardAmount { get; private set; }
    [field: SerializeField]
    [field: TextArea]
    public string SuccessMessage { get; private set; }
    [field: SerializeField]
    [field: TextArea]
    public string FailMessage { get; private set; }
}
