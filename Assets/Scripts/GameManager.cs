using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private Int32Variable daysTotalVariable;
    [SerializeField] private Int32Variable dayLengthSecVariable;
    [SerializeField] private Int64Variable timerVariable;

    private void Awake()
    {
        moneyVariable.Reset();
    }

    void Start()
    {
        timerVariable.Value = daysTotalVariable.Value * dayLengthSecVariable.Value;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (timerVariable.Value > 0)
        {
            yield return new WaitForSeconds(1f);
            timerVariable.Value -= 1;
        }

        var existingMachines = GameObject.FindObjectsOfType<MachineController>();
        foreach (var machine in existingMachines)
        {
            moneyVariable.Value += machine.ResaleValue;
        }
        LoadNextScene(sceneToLoad);
    }
    
    private void LoadNextScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
