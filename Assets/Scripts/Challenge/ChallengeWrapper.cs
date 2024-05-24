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

    public void SetChallenge(Challenge challenge)
        => Challenge = challenge;

    private void OnEnable()
    {
        _successChanceText.text = Challenge.SuccessChance.ToString();
        _rewardText.text = Challenge.Reward.ToString();
        _descriptionText.text = Challenge.Description.ToString();

    }

    public void Accept()
    {
        if (Random.Range(0f, 100f) < Challenge.SuccessChance)
        {
            GameManager.Instance.RiskTaken += 100 - Challenge.SuccessChance;
        }
        else
        {

        }
        GameManager.Instance.RiskTotal += 100 - Challenge.SuccessChance;
    }
    public void Decline()
    {
        GameManager.Instance.RiskTotal += 100 - Challenge.SuccessChance;
    }
}
