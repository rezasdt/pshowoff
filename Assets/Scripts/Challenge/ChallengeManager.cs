using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField]
    private ChallengeWrapper _challengeWrapperPrefab;
    [SerializeField]
    private Popup _popup;
    [SerializeField]
    private List<TimedChallenge> _timedChallenges;

    private ChallengeWrapper _currentChallenge;
    private void Start()
    {
        StartCoroutine(ExecuteChallenges());
    }

    private IEnumerator ExecuteChallenges()
    {
        float previousTime = 0f;

        foreach (TimedChallenge timedChallenge in _timedChallenges)
        {
            float waitTime = timedChallenge.executionTime - previousTime;
            yield return new WaitForSeconds(waitTime);

            previousTime = timedChallenge.executionTime;

            _currentChallenge = Instantiate(_challengeWrapperPrefab, transform);
            _currentChallenge.SetChallenge(timedChallenge.challenge);
            _currentChallenge.Result += OnComplete;
            _currentChallenge.gameObject.SetActive(true);
        }
    }

    private void OnComplete(bool result)
    {
        if (result) Accept();
        else Decline();
        Destroy(_currentChallenge.gameObject);
    }

    private void Accept()
    {
        int chance = 100 - _currentChallenge.Challenge.SuccessChance;
        int money = _currentChallenge.Challenge.RiskRewardAmount;
        if (Random.Range(0f, 100f) < _currentChallenge.Challenge.SuccessChance)
        {
            GameManager.Instance.Money += money;
            _popup.Create(_currentChallenge.Challenge.SuccessMessage);
        }
        else
        {
            GameManager.Instance.Money -= money;
            _popup.Create(_currentChallenge.Challenge.FailMessage);
        }
        GameManager.Instance.RiskTaken += chance;
        GameManager.Instance.RiskTotal += chance;
    }
    private void Decline()
    {
        int chance = 100 - _currentChallenge.Challenge.SuccessChance;
        GameManager.Instance.RiskTotal += chance;
    }
}


[System.Serializable]
public class TimedChallenge
{
    public Challenge challenge;
    public float executionTime; // Absolute time in seconds since the start of execution
}
