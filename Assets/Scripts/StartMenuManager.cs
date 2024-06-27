using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private BoolVariable isPlaying;
    private void Start()
    {
        isPlaying.Value = false;
        AudioManager.Instance.PlaySoundtrack(AudioManager.Instance.Sounds.StartMenu);
    }
}
