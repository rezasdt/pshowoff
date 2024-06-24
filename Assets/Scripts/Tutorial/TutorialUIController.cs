using RedBlueGames.Tools.TextTyper;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TutorialUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextTyper textTyper;
    [SerializeField] private VideoPlayer videoPlayer;
    [Header("SO")]
    [SerializeField] private Tutorial tutorial;

    private int _index = -1;

    private void Awake()
    {
        if (tutorial.tutorialList.Count > 0)
        {
            _index = 0;
        }
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
