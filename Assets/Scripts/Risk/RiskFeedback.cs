using GD.MinMaxSlider;
using UnityEngine;

[System.Serializable]
public class RiskFeedback
{
    [field: MinMaxSlider(0f, 100f)]
    [field: SerializeField]
    public Vector2 RistRange { get; private set; } 
    [field: TextArea]
    [field: SerializeField]
    public string Feedback { get; private set; }
}
