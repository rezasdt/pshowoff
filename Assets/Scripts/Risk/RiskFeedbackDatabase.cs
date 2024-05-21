using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class RiskFeedbackDatabase : ScriptableObject
{
    [field: SerializeField]
    public List<RiskFeedback> riskFeedbacks { get; private set; } = new();
}
