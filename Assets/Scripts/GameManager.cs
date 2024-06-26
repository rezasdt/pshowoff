using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject outcome;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable daysTotalVariable;
    [SerializeField] private Int32Variable dayLengthSecVariable;
    [SerializeField] private Int64Variable timerVariable;
    [SerializeField] private Int32Variable riskVariable;
    [SerializeField] private Int32Variable riskCapacityVariable;
    [SerializeField] private MachineControllerRuntimeSet mControllerRuntimeSet;

    void Start()
    {
        moneyVariable.Reset();
        riskVariable.Reset();
        riskCapacityVariable.Reset();
        mControllerRuntimeSet.Clear();
        
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

        moneyVariable.Value += mControllerRuntimeSet.Value;
        Time.timeScale = 0f;
        outcome.gameObject.SetActive(true);
    }

}
