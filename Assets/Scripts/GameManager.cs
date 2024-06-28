using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject outcome;
    [SerializeField] private ChallengeManager challengeManger;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable daysTotalVariable;
    [SerializeField] private Int32Variable dayLengthSecVariable;
    [SerializeField] private Int64Variable timerVariable;
    [SerializeField] private Int32Variable riskVariable;
    [SerializeField] private Int32Variable riskCapacityVariable;
    [SerializeField] private MachineControllerRuntimeSet mControllerRuntimeSet;
    [SerializeField] private BoolVariable isPlaying;
    [SerializeField] private GameLogger gameLogger;

    void Start()
    {
        isPlaying.Value = true;
        moneyVariable.Reset();
        riskVariable.Reset();
        riskCapacityVariable.Reset();
        mControllerRuntimeSet.Clear();
        gameLogger.Reset();
        
        timerVariable.Value = daysTotalVariable.Value * dayLengthSecVariable.Value;
        StartCoroutine(TimerCoroutine());
        AudioManager.Instance.PlaySoundtrack(AudioManager.Instance.Sounds.Game);
    }

    private IEnumerator TimerCoroutine()
    {
        while (timerVariable.Value > 0)
        {
            yield return new WaitForSeconds(1f);
            timerVariable.Value -= 1;
        }

        moneyVariable.Value += mControllerRuntimeSet.TotalValue;
        foreach (var machine in mControllerRuntimeSet.Items)
        {
            machine.StopAllCoroutines();
        }
        challengeManger.StopAllCoroutines();
        outcome.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        gameLogger.CloudSaveLog();
    }
}
