using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RiskAdvice : ScriptableObject
{
    [SerializeField] private List<RiskStage> riskStages = new();

    public RiskStage GetRiskStage(int pRiskPercentage)
    {
        if (riskStages == null || riskStages.Count == 0)
        {
            return null;
        }

        for (int i = riskStages.Count - 1; i >= 0; i--)
        {
            if (pRiskPercentage >= riskStages[i].MinRisk)
            {
                return riskStages[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class RiskStage
{
    [field: SerializeField][field: Range(0, 100)] public int MinRisk { get; private set; }
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField][field: TextArea] public string Advice { get; private set; }
}