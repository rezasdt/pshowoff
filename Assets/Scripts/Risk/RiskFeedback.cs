using UnityEngine;

[System.Serializable]
public class RiskFeedback
{
    [field: Range(0f, 100f)]
    [field: SerializeField]
    public float MinRisk { get; private set; } 
    [field: TextArea]
    [field: SerializeField]
    public string Feedback { get; private set; }
}
