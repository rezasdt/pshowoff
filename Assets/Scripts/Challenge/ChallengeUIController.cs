using System.Collections;
using TMPro;
using UnityEngine;

public class ChallengeUIController : MonoBehaviour
{
    public event System.Action<bool, string> OnChallengeSuccess = delegate { };
    
    [SerializeField] private int durationSec;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI details;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable riskVariable;
    [SerializeField] private GameLogger gameLogger;

    private Challenge _challenge;
    public void Init(Challenge pChallenge)
    {
        _challenge = pChallenge;
        title.text = _challenge.Title;
        description.text = _challenge.Description;
        details.text = $"{_challenge.Cost:N0}$\n{_challenge.RewardAmount:N0}$\n{_challenge.SuccessChance.ToString()}%";
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (durationSec > 0)
        {
            timer.text = $"{durationSec}s";
            yield return new WaitForSeconds(1f);
            durationSec--;
        }

        Decline();
    }
    
    public void Accept()
    {
        if (moneyVariable.Value < _challenge.Cost) return;
        gameLogger.AcceptChallenge(_challenge);
        moneyVariable.Value -= _challenge.Cost;
        if (Random.Range(0, 100) < _challenge.SuccessChance)
        {
            moneyVariable.Value += _challenge.RewardAmount;
            OnChallengeSuccess.Invoke(true, _challenge.SuccessMessage);
            gameLogger.ChallengeSuccess(_challenge);
        }
        else
        {
            OnChallengeSuccess.Invoke(false, _challenge.FailMessage);
            gameLogger.ChallengeFail(_challenge);
        }
        riskVariable.Value += 100 - _challenge.SuccessChance;
        Destroy(gameObject);
    }

    public void Decline()
    {
        gameLogger.DeclineChallenge(_challenge);
        Destroy(gameObject);
    }
}
