using UnityEngine;
using System.Collections.Generic;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] private int challengesPerStage;
    [SerializeField] private int challengesIntervalSec;
    [SerializeField] private ChallengeUIController challengePrefab;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private RectTransform challengeCanvas;
    [Header("SO")]
    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private Int32Variable riskCapacityVariable;

    private int _highestReachedStage = -1;
    private bool _isCoroutineRunning = false;
    private readonly Queue<Challenge> _challenges = new();

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
            List<int> selectedIndices = new List<int>();

            int challengesToAdd = Mathf.Min(challengesPerStage, stage.Challenges.Length);

            while (selectedIndices.Count < challengesToAdd)
            {
                int randomIndex = Random.Range(0, stage.Challenges.Length);

                if (!selectedIndices.Contains(randomIndex))
                {
                    selectedIndices.Add(randomIndex);
                    _challenges.Enqueue(stage.Challenges[randomIndex]);
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

        while (_challenges.Count > 0)
        {
            yield return new WaitForSeconds(challengesIntervalSec);
            Challenge challenge = _challenges.Dequeue();
            ChallengeUIController challengeUI = Instantiate(challengePrefab, challengeCanvas.transform);
            challengeUI.Init(challenge);
            riskCapacityVariable.Value += 100 - challenge.SuccessChance;
        }

        _isCoroutineRunning = false;
    }
}
