using System.Collections;
using TMPro;
using UnityEngine;

public class ChallengeUIController : MonoBehaviour
{
    [SerializeField] private int durationSec;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI details;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;

    private Challenge _challenge;
    public void Init(Challenge pChallenge)
    {
        _challenge = pChallenge;
        title.text = _challenge.Title;
        description.text = _challenge.Description;
        details.text = $"{_challenge.Cost.ToString()}$\n{_challenge.RewardAmount.ToString()}$\n{_challenge.SuccessChance.ToString()}%";
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
        moneyVariable.Value -= _challenge.Cost;
        moneyVariable.Value += Random.Range(0, 100) < _challenge.SuccessChance ? _challenge.RewardAmount : 0;
        Destroy(gameObject);
    }

    public void Decline()
    {
        Destroy(gameObject);
    }
}
