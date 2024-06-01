using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameLogger : ScriptableObject
{
    [System.Serializable]
    private class LogEntry
    {
        public string action;
        public string objectName;
        public float time;
    }

    public string BuildVersion { get; private set;}
    private float _startTime;
    private List<LogEntry> _logs = new();

    private void OnEnable()
    {
        _startTime = Time.time;
        BuildVersion = Application.version;
    }

    public void LogAction(string action, string objectName)
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

    public void AcceptChallenge(Challenge pChallenge)
    {
        LogAction("AcceptChallenge", pChallenge.name);
    }

    public void DeclineChallenge(Challenge pChallenge)
    {
        LogAction("DeclineChallenge", pChallenge.name);
    }

    public void ClearLogs()
    {
        _logs.Clear();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(_logs, false);
    }
}
