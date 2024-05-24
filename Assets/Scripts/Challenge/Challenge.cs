using UnityEngine;

[CreateAssetMenu]
public class Challenge : ScriptableObject
{
    [field: SerializeField]
    [field: Range(0, 100)]
    public int SuccessChance { get; private set; }
    [field: SerializeField]
    [field: TextArea]
    public string Description { get; private set; }

    public void Accept()
    {
        GameManager.Instance.RiskTaken += 100 - SuccessChance;
        GameManager.Instance.RiskTotal += 100 - SuccessChance;
    }
    public void Decline()
    {
        GameManager.Instance.RiskTotal += 100 - SuccessChance;
    }
}
