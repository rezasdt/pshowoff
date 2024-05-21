using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Money;
    public int RiskTaken;
    public int RiskTotal;
    [field: SerializeField] public int DaysLeft { get; private set; }

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
        while (DaysLeft > 0)
        {
            yield return new WaitForSeconds(60);
            DaysLeft--;

            if (DaysLeft == 0)
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
