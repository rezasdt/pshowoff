using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;

[CreateAssetMenu]
public class GameLogger : ScriptableObject
{
    [System.Serializable]
    public class LogEntry
    {
        public string action;
        public string objectName;
        public float time;
    }

    public string BuildVersion { get; private set;}
    private float _startTime;
    private readonly List<LogEntry> _logs = new();

    public void Reset()
    {
        _logs.Clear();
        _startTime = Time.time;
    }
    
    private void OnEnable()
    {
        BuildVersion = Application.version;
        Reset();
    }

    private void LogAction(string action, string objectName)
    {
        LogEntry logEntry = new LogEntry
        {
            action = action,
            objectName = objectName,
            time = Time.time - _startTime
        };
        _logs.Add(logEntry);
    }

    public void BuyMachine(MachineBase pMachine)
    {
        LogAction("BuyMachine", pMachine.name);
    }

    public void SellMachine(MachineBase pMachine)
    {
        LogAction("SellMachine", pMachine.name);
    }

    public void UpgradeMachine(MachineBase pMachine)
    {
        LogAction("UpgradeMachine", pMachine.name);
    }
    
    public void MachineUpgradeSuccess(MachineBase pMachine)
    {
        LogAction("MachineUpgradeSuccess", pMachine.name);
    }
    
    public void MachineUpgradeFail(MachineBase pMachine)
    {
        LogAction("MachineUpgradeFail", pMachine.name);
    }

    public void AcceptChallenge(Challenge pChallenge)
    {
        LogAction("AcceptChallenge", pChallenge.name);
    }

    public void DeclineChallenge(Challenge pChallenge)
    {
        LogAction("DeclineChallenge", pChallenge.name);
    }
    
    public void ChallengeSuccess(Challenge pChallenge)
    {
        LogAction("ChallengeSuccess", pChallenge.name);
    }
    
    public void ChallengeFail(Challenge pChallenge)
    {
        LogAction("ChallengeFail", pChallenge.name);
    }
    
    public void RepairMachine(MachineBase pMachine)
    {
        LogAction("RepairMachine", pMachine.name);
    }
    
    public void StagePromote(int pNewStage)
    {
        LogAction("StagePromote", pNewStage.ToString());
    }
    public void StageDemote(int pNewStage)
    {
        LogAction("StageDemote", pNewStage.ToString());
    }

    private string ToJson()
    {
        var data = new
        {
            BuildVersion = this.BuildVersion,
            Logs = _logs
        };
        return JsonConvert.SerializeObject(data);
    }

    public async void CloudSaveLog()
    {
        var playerData = new Dictionary<string, object>{
          {$"Log_{System.DateTime.Now:yyMMdd_HHmm}", ToJson()}
        };
        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        }
        catch { }
    }
}
