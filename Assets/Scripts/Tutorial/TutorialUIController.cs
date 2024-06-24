using RedBlueGames.Tools.TextTyper;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class TutorialUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextTyper textTyper;
    [SerializeField] private VideoPlayer videoPlayer;
    [Header("SO")]
    [SerializeField] private Tutorial tutorial;

    private int _index = -1;
    private PlayerControls _playerControls;
    private PlayerControls.PlayerActions _playerActions;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerActions = _playerControls.Player;
        if (tutorial.tutorialList.Count > 0)
        {
            _index = 0;
        }
    }
    
    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Point.performed += OnPoint;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Point.performed -= OnPoint;
    }

    private void OnPoint(InputAction.CallbackContext context)
    {
        Skip();
    }

    private void Start()
    {
        if (_index == 0) LoadTutorial(0);
    }

    private void LoadTutorial(int pIndex)
    {
        description.text = tutorial.tutorialList[pIndex].Description;
        textTyper.TypeText(description.text);
        if (tutorial.tutorialList[pIndex].VideoFileName == "")
        {
            videoPlayer.gameObject.SetActive(false);
            return;
        }
        videoPlayer.gameObject.SetActive(true);
        string videoPath =
            System.IO.Path.Combine(Application.streamingAssetsPath, tutorial.tutorialList[pIndex].VideoFileName);
        print(videoPath);
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }

    public void LoadNext()
    {
        if (_index + 1 >= tutorial.tutorialList.Count) return;
        LoadTutorial(++_index);
    }
    
    public void LoadPrevious()
    {
        if (_index - 1 < 0) return;
        LoadTutorial(--_index);
    }

    public void Skip()
    {
        textTyper.Skip();
    }
}
