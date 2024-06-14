using UnityEngine;
using System.Collections.Generic;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] private int challengesIntervalSec;
    [SerializeField] private ChallengeUIController challengePrefab;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private RectTransform challengeCanvas;
    [Header("SO")]
    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private Int32Variable riskCapacityVariable;

    private int _highestReachedStage = -1;
    private bool _isCoroutineRunning = false;
    private Queue<Challenge> _challengeQueue = new();

    private void OnEnable()
    {
        stageManager.OnStageChange += CheckAddChallenges;
    }

    private void OnDisable()
    {
        stageManager.OnStageChange -= CheckAddChallenges;
    }

    private void CheckAddChallenges(int pNewStage)
    {
        if (pNewStage <= _highestReachedStage)
            return;

        AddChallenges(pNewStage);
    }

    private void AddChallenges(int newStage)
    {
        for (int stageIndex = _highestReachedStage + 1; stageIndex <= newStage; stageIndex++)
        {
            Stage stage = stageDatabase.Stages[stageIndex];
            for (int i = 0; i < 2; i++)
            {
                if (stage.Challenges.Length > 0)
                {
                    int randomIndex = Random.Range(0, stage.Challenges.Length);
                    _challengeQueue.Enqueue(stage.Challenges[randomIndex]);
                }
            }
        }

        if (!_isCoroutineRunning)
        {
            StartCoroutine(ScheduleChallenges());
        }

        _highestReachedStage = newStage;
    }

    private System.Collections.IEnumerator ScheduleChallenges()
    {
        _isCoroutineRunning = true;

        while (_challengeQueue.Count > 0)
        {
            yield return new WaitForSeconds(challengesIntervalSec);
            Challenge challenge = _challengeQueue.Dequeue();
            ChallengeUIController challengeUI = Instantiate(challengePrefab, challengeCanvas.transform);
            challengeUI.Init(challenge);
            riskCapacityVariable.Value += 100 - challenge.SuccessChance;
        }

        _isCoroutineRunning = false;
    }
}
