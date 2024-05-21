using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Money;
    [HideInInspector] public int RiskTaken = 0;
    [HideInInspector] public int RiskTotal = 1;
    [field: SerializeField] public int TotalDays { get; private set; }
    [SerializeField] private int _dayLength = 6;

    private string _sceneToLoadWhenDaysEnd = "Results";
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(CountdownDays());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CountdownDays()
    {
        while (TotalDays > 0)
        {
            yield return new WaitForSeconds(_dayLength);
            TotalDays--;

            if (TotalDays == 0)
            {
                LoadEndScene();
            }
        }
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene(_sceneToLoadWhenDaysEnd);
    }
}
