using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Int32Variable daysTotalVariable;
    [SerializeField] private Int32Variable dayLengthSecVariable;
    [SerializeField] private Int64Variable timerVariable;
    
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
        
        LoadNextScene(sceneToLoad);
    }
    
    private void LoadNextScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
