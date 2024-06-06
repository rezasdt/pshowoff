using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }
}