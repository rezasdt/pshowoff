using System;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private NotificationUIController notificationPrefab;
    [SerializeField] private StageManager stageManager;
    [Header("SO")]
    [SerializeField] private GameLogger gameLogger;
    private int? _formerStage;
    
    private void OnEnable()
    {
        ImprovedMachineController.UpgradeSuccess += OnUpgradeSuccessFail;
        stageManager.OnStageChange += OnStageChange;
    }

    private void OnDisable()
    {
        ImprovedMachineController.UpgradeSuccess -= OnUpgradeSuccessFail;
        stageManager.OnStageChange -= OnStageChange;
    }

    public void Create(string pTitle, string pDescription, float pDuration = 8f)
    {
        var newNotification = Instantiate(notificationPrefab, container);
        newNotification.Init(pTitle, pDescription, pDuration);
    }

    private void OnUpgradeSuccessFail(ImprovedMachineController pImprovedMachineController)
    {
        if (pImprovedMachineController.IsHealthy) return;
        Create("Upgrade Failed!", "You can repair the machine or sell it.");
    }

    private void OnStageChange(int pNewStage)
    {
        if (_formerStage == null)
        {
            _formerStage = pNewStage;
            return;
        }

        if (pNewStage > _formerStage.Value)
        {
            Create("Stage Promotion!", "You have advanced to a new stage and unlocked new machines.");
            gameLogger.StagePromote(pNewStage);
        }
        else
        {
            Create("Stage Demotion!", "You have been demoted to a lower stage. Some machines are locked again.");
            gameLogger.StageDemote(pNewStage);
        }

        _formerStage = pNewStage;
    }
}
