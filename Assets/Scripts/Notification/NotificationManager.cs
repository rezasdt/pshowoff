using System;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private NotificationUIController notificationPrefab;
    [SerializeField] private StageManager stageManager;

    private int? _formerStage;
    
    private void OnEnable()
    {
        ImprovedMachineController.OnDefectedUpgrade += OnUpgradeFail;
        stageManager.OnStageChange += OnStageChange;
    }

    private void OnDisable()
    {
        ImprovedMachineController.OnDefectedUpgrade -= OnUpgradeFail;
        stageManager.OnStageChange -= OnStageChange;
    }

    public void Create(string pTitle, string pDescription, float pDuration = 8f)
    {
        var newNotification = Instantiate(notificationPrefab, container);
        newNotification.Init(pTitle, pDescription, pDuration);
    }

    private void OnUpgradeFail()
    {
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
        }
        else
        {
            Create("Stage Demotion!", "You have been demoted to a lower stage. Some machines are locked again.");
        }

        _formerStage = pNewStage;
    }
}
