using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeWrapper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _successChanceText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;
    public Challenge Challenge { get; private set; }

    public event Action<bool> Result = delegate { };

    public void SetChallenge(Challenge challenge)
        => Challenge = challenge;

    private void OnEnable()
    {
        _successChanceText.text = Challenge.SuccessChance.ToString() + "%";
        _rewardText.text = "$" + Challenge.RiskRewardAmount.ToString();
        _descriptionText.text = Challenge.Description.ToString();
    }

    public void Accept()
        => Result.Invoke(true);
    public void Decline()
        => Result.Invoke(false);
}
