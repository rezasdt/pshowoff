using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlaySoundtrack(AudioManager.Instance.Sounds.StartMenu);
    }
}
